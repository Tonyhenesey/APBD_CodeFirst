namespace WebApplicationCodeFirst.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Prescription_Medicament")]
public class Prescription_Medicament
{
    private Prescription _prescription;

    [Key, Column(Order = 0)]
    public int IdMedicament { get; set; }

    [Key, Column(Order = 1)]
    public int IdPrescription { get; set; }

    public int Dose { get; set; }

    [MaxLength(100)]
    public string Details { get; set; }

    [ForeignKey("IdMedicament")]
    public Medicament Medicament { get; set; }

    [ForeignKey("IdPrescription")]
    public Prescription Prescription
    {
        get => _prescription;
        set => _prescription = value;
    }
}
