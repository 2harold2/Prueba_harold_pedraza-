using System.ComponentModel.DataAnnotations;

namespace prueba_harold_pedraza.Domain;

public class Patient
{
    public int? IdPatient { get; set; } = 0;

    [Required]
    [StringLength(15, ErrorMessage = "{0} puede cantidad de caracteres entre {2} y {1}.", MinimumLength = 2)]
    public string DocumentNumber { get; set; }

    [Required]
    [StringLength(120, ErrorMessage = "{0} puede cantidad de caracteres entre {2} y {1}.", MinimumLength = 2)]
    public string Names { get; set; }

    [StringLength(120, ErrorMessage = "{0} puede cantidad de caracteres entre {2} y {1}.", MinimumLength = 2)]
    public string LastNames { get; set; }

    [Required]
    [StringLength(150, ErrorMessage = "{0} puede cantidad de caracteres entre {2} y {1}.", MinimumLength = 2)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [StringLength(15, ErrorMessage = "{0} puede cantidad de caracteres entre {2} y {1}.", MinimumLength = 1)]
    public string Phone { get; set; } = string.Empty;

    public string BirthDate { get; set; } = string.Empty;
}

public class PatientRelations : Patient
{
    [Required]
    public int IdTypeDocument { get; set; }

    [Required]
    public int IdAffiliationType { get; set; }
}

public class PatientFormatted : Patient
{
    public string DocumentType { get; set; }

    public string Afiliation { get; set; }
}
