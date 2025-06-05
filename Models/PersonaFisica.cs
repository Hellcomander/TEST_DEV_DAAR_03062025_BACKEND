using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TEST_DEV_DAAR_03062025.Models
{
    [Table("Tb_PersonasFisicas")]
    public class PersonaFisica
    {
        [Key]
        public int IdPersonaFisica { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        public DateTime? FechaActualizacion { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Nombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string? ApellidoPaterno { get; set; }

        [Required]
        [MaxLength(50)]
        public string? ApellidoMaterno { get; set; }

        [Required]
        [MaxLength(50)]
        public string? RFC { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public int UsuarioAgrega { get; set; }

        public bool Activo { get; set; } = true;
    }
}
