using System.ComponentModel.DataAnnotations;

namespace TEST_DEV_DAAR_03062025.Dto;

public class PersonaFisicaCreateDto
{
    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

    [Required]
    public required string Nombre { get; set; }

    [Required]
    public required string ApellidoPaterno { get; set; }

    [Required]
    public required string ApellidoMaterno { get; set; }

    [Required]
    public required string RFC { get; set; }

    [Required]
    public required DateTime FechaNacimiento { get; set; }

    [Required]
    public required int UsuarioAgrega { get; set; }

    public bool Activo { get; set; } = true;
}

public class PersonaFisicaUpdateDto
{
    [Required]
    public int IdPersonaFisica { get; set; }

    [Required]
    public required string Nombre { get; set; }

    [Required]
    public required string ApellidoPaterno { get; set; }

    [Required]
    public required string ApellidoMaterno { get; set; }

    [Required]
    public required DateTime FechaNacimiento { get; set; }

    [Required]
    public required string RFC { get; set; }

    public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;
}