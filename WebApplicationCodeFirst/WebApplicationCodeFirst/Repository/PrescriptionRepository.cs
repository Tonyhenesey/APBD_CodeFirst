using Microsoft.EntityFrameworkCore;
using WebApplicationCodeFirst.DBContext;
using WebApplicationCodeFirst.Models;
using WebApplicationCodeFirst.ModelsDTO;
using System;

namespace WebApplicationCodeFirst.Repository;

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly ApplicationDbContext _context;

    public PrescriptionRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Prescription> CreatePrescriptionAsync(PrescriptionRequest request)
    {
        // Validate request
        if (request == null) throw new ArgumentNullException(nameof(request));
        if (request.Patient == null) throw new ArgumentNullException(nameof(request.Patient));
        if (request.Medicaments == null) throw new ArgumentNullException(nameof(request.Medicaments));

        // Check if the number of medicaments exceeds the limit
        if (request.Medicaments.Count > 10)
        {
            throw new ArgumentException("Prescription can include a maximum of 10 medicaments.");
        }

        // Check if DueDate is valid
        if (request.DueDate < request.Date)
        {
            throw new ArgumentException("DueDate must be greater than or equal to Date.");
        }

        // Check if patient exists or create a new one
        Patient patient = null;
        if (request.Patient.IdPatient.HasValue)
        {
            patient = await _context.Patients.FindAsync(request.Patient.IdPatient.Value);
            if (patient == null)
            {
                patient = new Patient
                {
                    FirstName = request.Patient.FirstName,
                    LastName = request.Patient.LastName,
                    Birthdate = request.Patient.Birthdate.Value
                };
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
            }
        }
        else
        {
            if (string.IsNullOrWhiteSpace(request.Patient.FirstName) ||
                string.IsNullOrWhiteSpace(request.Patient.LastName) ||
                !request.Patient.Birthdate.HasValue)
            {
                throw new ArgumentException("Invalid patient information.");
            }

            patient = new Patient
            {
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                Birthdate = request.Patient.Birthdate.Value
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync(); // Save the new patient to get the ID
        }

        // Fetch all doctors from the database
        var doctors = await _context.Doctors.ToListAsync();
        if (doctors == null || doctors.Count == 0)
        {
            throw new InvalidOperationException("No doctors available.");
        }

        // Select a random doctor
        var random = new Random();
        var randomDoctor = doctors[random.Next(doctors.Count)];

        // Create the prescription
        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            Patient = patient,
            Doctor = randomDoctor
        };

        // Add medicaments to the prescription
        foreach (var medicamentDto in request.Medicaments)
        {
            if (medicamentDto == null) throw new ArgumentNullException(nameof(medicamentDto));

            var medicament = await _context.Medicaments.FindAsync(medicamentDto.IdMedicament);
            if (medicament == null)
            {
                throw new ArgumentException($"Medicament with ID {medicamentDto.IdMedicament} does not exist.");
            }

            prescription.Prescription_Medicaments.Add(new Prescription_Medicament
            {
                Medicament = medicament,
                Dose = medicamentDto.Dose,
                Details = medicamentDto.Description
            });
        }

        // Add and save the prescription
        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        return prescription;
    }
}
