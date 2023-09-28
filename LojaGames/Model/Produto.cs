using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace LojaGames.Model
{
    public class Produto : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string nome { get; set; } = string.Empty;

        [Column(TypeName = "varchar")]
        [StringLength(1000)]
        public string descricao { get; set; } = string.Empty;

        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string console { get; set; } = string.Empty;

        [Column(TypeName = "decimal(5,2)")]
        public decimal preco { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(1000)]
        public string foto { get; set; } = string.Empty;

        public virtual Categoria? Categoria { get; set; }
    }
}
