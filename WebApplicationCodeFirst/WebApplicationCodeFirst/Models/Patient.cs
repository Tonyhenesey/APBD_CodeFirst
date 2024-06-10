namespace WebApplicationCodeFirst.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("patient")]
public class Patient
{
    [Key]
    public int IdPatient { get; set; }

    [Required, MaxLength(100)]
    public string FirstName { get; set; }

    [Required, MaxLength(100)]
    public string LastName { get; set; }

    public DateTime Birthdate { get; set; }

    public ICollection<Prescription> Prescriptions { get; set; }
}
