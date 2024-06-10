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
        if (request.Medicaments.Count > 10)
        {
            throw new ArgumentException("Prescription can include a maximum of 10 medicaments.");
        }

        if (request.DueDate < request.Date)
        {
            throw new ArgumentException("DueDate must be greater than or equal to Date.");
        }

        var patient = request.Patient.IdPatient.HasValue 
            ? await _context.Patients.FindAsync(request.Patient.IdPatient.Value) 
            : new Patient
            {
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                Birthdate = request.Patient.Birthdate.Value
            };

        var doctor = await _context.Doctors.FindAsync(request.Patient.IdPatient.Value);
        if (doctor == null)
        {
            throw new ArgumentException("Doctor does not exist.");
        }

        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            Patient = patient,
            Doctor = doctor
        };

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
                // Details = medicamentDto.
            });
        }

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        return prescription;
    }
}