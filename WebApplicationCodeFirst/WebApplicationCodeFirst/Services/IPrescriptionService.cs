using WebApplicationCodeFirst.ModelsDTO;

namespace WebApplicationCodeFirst.Services;

public interface IPrescriptionService
{
    Task<Prescription> CreatePrescriptionAsync(PrescriptionRequest request);
}