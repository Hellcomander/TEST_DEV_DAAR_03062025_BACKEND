using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TEST_DEV_DAAR_03062025.Models
{
    [Table("Users")]

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Nombre { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Apellidos { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Correo { get; set; }

        [Required]
        [MaxLength(256)]
        public string? Password { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        public bool Activo { get; set; } = true;
    }
}