using WebApplicationCodeFirst.Models;

namespace WebApplicationCodeFirst.ModelsDTO;

public class PrescriptionRequest
{
    // public DoctorDto IdDoctor { get; set; } 
    public PatientDto Patient { get; set; }
    public List<MedicamentDto> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    
}