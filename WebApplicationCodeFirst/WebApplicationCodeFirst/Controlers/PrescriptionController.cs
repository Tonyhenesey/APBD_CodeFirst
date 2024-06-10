using Microsoft.AspNetCore.Mvc;
using WebApplicationCodeFirst.ModelsDTO;
using WebApplicationCodeFirst.Repository;

namespace WebApplicationCodeFirst.Controlers;


[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public PrescriptionsController(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrescription(PrescriptionRequest request)
    {
        try
        {
            var prescription = await _prescriptionRepository.CreatePrescriptionAsync(request);
            return Ok(prescription);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}