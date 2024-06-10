using WebApplicationCodeFirst.ModelsDTO;

namespace WebApplicationCodeFirst.Repository
{
    public interface IPatientRepository
    {
        Task<PatientDto> ShowPatientData(PatientRequest request);
    }
}