using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplicationCodeFirst.ModelsDTO;
using WebApplicationCodeFirst.Services;

namespace WebApplicationCodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
        }

        [HttpPost("get-patient-data")]
        public async Task<ActionResult<PatientDto>> GetPatientData(PatientRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request cannot be null");
            }

            try
            {
                var patientData = await _patientService.GetPatientDataAsync(request);
                return Ok(patientData);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }
    }
}