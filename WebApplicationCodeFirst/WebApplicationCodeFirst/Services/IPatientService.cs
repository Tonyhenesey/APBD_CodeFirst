using System.Threading.Tasks;
using WebApplicationCodeFirst.ModelsDTO;

namespace WebApplicationCodeFirst.Services
{
    public interface IPatientService
    {
        Task<PatientDto> GetPatientDataAsync(PatientRequest request);
    }
}