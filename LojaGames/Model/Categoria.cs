﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LojaGames.Model
{
    public class Categoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string tipo { get; set; } = string.Empty;

        [InverseProperty("Categoria")]
        public virtual ICollection<Produto>? Produto { get; set; }
    }
}
