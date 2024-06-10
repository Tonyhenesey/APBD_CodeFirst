using Microsoft.AspNetCore.Mvc;
using WebApplicationCodeFirst.ModelsDTO;
using WebApplicationCodeFirst.Repository;
using WebApplicationCodeFirst.Services;

namespace WebApplicationCodeFirst.Controlers;


[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionsController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost("CreatePrescription")]
    public async Task<IActionResult> CreatePrescription(PrescriptionRequest request)
    {
        try
        {
            var prescription = await _prescriptionService.CreatePrescriptionAsync(request);
            return Ok(prescription);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}