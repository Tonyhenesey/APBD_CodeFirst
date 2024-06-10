using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCodeFirst.DBContext;
using WebApplicationCodeFirst.Models;
using WebApplicationCodeFirst.ModelsDTO;

namespace WebApplicationCodeFirst.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PatientDto> ShowPatientData(PatientRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            if (!request.Patient.IdPatient.HasValue)
            {
                throw new ArgumentException("Patient ID is required");
            }

            var patient = await _context.Patients
                .Include(p => p.Prescriptions)
                    .ThenInclude(p => p.Prescription_Medicaments)
                        .ThenInclude(pm => pm.Medicament)
                .Include(p => p.Prescriptions)
                    .ThenInclude(p => p.Doctor)
                .Where(p => p.IdPatient == request.Patient.IdPatient.Value)
                .Select(p => new PatientDto
                {
                    IdPatient = p.IdPatient,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Birthdate = p.Birthdate,
                    Prescriptions = p.Prescriptions
                        .OrderBy(pr => pr.DueDate)
                        .Select(pr => new PrescriptionDto
                        {
                            IdPrescription = pr.IdPrescription,
                            Date = pr.Date,
                            DueDate = pr.DueDate,
                            Medicaments = pr.Prescription_Medicaments
                                .Select(pm => new PrescriptionMedicamentDto
                                {
                                    IdMedicament = pm.Medicament.IdMedicament,
                                    Name = pm.Medicament.Name,
                                    Dose = pm.Dose,
                                    Description = pm.Details
                                }).ToList(),
                            Doctor = new DoctorDto
                            {
                                IdDoctor = pr.Doctor.IdDoctor,
                                FirstName = pr.Doctor.FirstName,
                                LastName = pr.Doctor.LastName
                            }
                        }).ToList()
                }).FirstOrDefaultAsync();

            if (patient == null)
            {
                throw new InvalidOperationException("Patient doesn't exist");
            }

            return patient;
        }
    }
}
