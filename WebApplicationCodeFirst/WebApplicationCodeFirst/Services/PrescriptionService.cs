using WebApplicationCodeFirst.ModelsDTO;
using WebApplicationCodeFirst.Repository;

namespace WebApplicationCodeFirst.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public PrescriptionService(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }

    public async Task<Prescription> CreatePrescriptionAsync(PrescriptionRequest request)
    {
        return await _prescriptionRepository.CreatePrescriptionAsync(request);
    }
    
}