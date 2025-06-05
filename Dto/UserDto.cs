using System.ComponentModel.DataAnnotations;

namespace TEST_DEV_DAAR_03062025.Dto;

public class UserCreateDto
{
    [Required]
    public required string Nombre { get; set; }

    [Required]
    public required string Apellidos { get; set; }

    [Required]
    public required string Correo { get; set; }

    [Required]
    public required string Password { get; set; }

    public bool Activo { get; set; } = true;

    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
}

public class LoginDto
{
    [Required]
    public required string Correo { get; set; }

    [Required]
    public required string Password { get; set; }
}