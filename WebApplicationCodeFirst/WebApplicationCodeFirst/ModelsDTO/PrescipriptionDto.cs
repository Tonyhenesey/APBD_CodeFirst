using WebApplicationCodeFirst.Models;

namespace WebApplicationCodeFirst.ModelsDTO;
public class PrescriptionDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    // public ICollection<Prescription_Medicament> Medicaments { get; set; }
    public List<PrescriptionMedicamentDto> Medicaments { get; set; }
    public DoctorDto Doctor { get; set; }
}