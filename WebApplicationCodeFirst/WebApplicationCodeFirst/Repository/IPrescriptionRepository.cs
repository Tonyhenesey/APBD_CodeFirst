using WebApplicationCodeFirst.ModelsDTO;

namespace WebApplicationCodeFirst.Repository;

public interface IPrescriptionRepository
{
    Task<Prescription> CreatePrescriptionAsync(PrescriptionRequest request);
}