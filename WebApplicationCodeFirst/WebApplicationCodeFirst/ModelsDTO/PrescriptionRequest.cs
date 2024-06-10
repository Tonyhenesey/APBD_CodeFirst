using WebApplicationCodeFirst.Models;

namespace WebApplicationCodeFirst.ModelsDTO;

public class PrescriptionRequest
{
    public int? PatientId { get; set; }
    public string PatientFirstName { get; set; }
    public string PatientLastName { get; set; }
    public DateTime Birthdate { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int DoctorId { get; set; }
    public List<PrescriptionMedicamentRequest> Medicaments { get; set; }
    public Patient Patient { get; }
}