using WebApplicationCodeFirst.DBContext;
using WebApplicationCodeFirst.Models;
using WebApplicationCodeFirst.ModelsDTO;

namespace WebApplicationCodeFirst.Repository;

public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public PrescriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

       public async Task<Prescription> CreatePrescriptionAsync(PrescriptionRequest request)
{
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
    Patient patient;
    if (request.Patient.IdPatient.HasValue)
    {
        patient = await _context.Patients.FindAsync(request.Patient.IdPatient.Value);
        if (patient == null)
        {
            throw new ArgumentException("Patient does not exist.");
        }
    }
    else
    {
        patient = new Patient
        {
            FirstName = request.Patient.FirstName,
            LastName = request.Patient.LastName,
            Birthdate = request.Patient.Birthdate.Value
        };
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync(); // Save the new patient to get the ID
    }

    // Check if doctor exists
    var doctor = await _context.Doctors.FindAsync(request);
    if (doctor == null)
    {
        throw new ArgumentException("Doctor does not exist.");
    }

    // Create the prescription
    var prescription = new Prescription
    {
        Date = request.Date,
        DueDate = request.DueDate,
        Patient = patient,
        Doctor = doctor
    };

    // Add medicaments to the prescription
    foreach (var medicamentDto in request.Medicaments)
    {
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