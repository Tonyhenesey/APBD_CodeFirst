namespace WebApplicationCodeFirst.ModelsDTO;

public class PrescriptionMedicamentRequest
{
    public int MedicamentId { get; set; }
    public int Dose { get; set; }
    public string Details { get; set; }
    public object?[]? IdMedicament { get; set; }
}