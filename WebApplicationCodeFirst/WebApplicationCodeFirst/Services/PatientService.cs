using System.Threading.Tasks;
using WebApplicationCodeFirst.ModelsDTO;
using WebApplicationCodeFirst.Repository;

namespace WebApplicationCodeFirst.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
        }

        public async Task<PatientDto> GetPatientDataAsync(PatientRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var patientData = await _patientRepository.ShowPatientData(request);
            return patientData;
        }
    }
}