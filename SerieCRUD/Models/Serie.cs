using SerieCRUD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SerieCRUD.Models
{

    public class Serie
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Gênero")]
        [Required]
        public Genero Genero { get; set; }
        [DisplayName("Título")]
        [Required(ErrorMessage="O campo título é obrigatorio.")]
        public string Titulo { get; set; }
        [DisplayName("Descrição")]
        [StringLength(250)]
        public string Descricao { get; set; }
        [Range(1900, 2100)]
        [Required(ErrorMessage="Ano inválido.")]
        public int Ano { get; set; }
        public bool Excluido { get; set; }
    }
}
