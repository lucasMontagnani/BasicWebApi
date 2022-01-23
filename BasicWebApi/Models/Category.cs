using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BasicWebApi.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatorio.")]
        [MaxLength(60, ErrorMessage =("Este campo deve conter entre 1 e 60 caracteres."))]
        [MinLength(1, ErrorMessage =("Este campo deve conter entre 1 e 60 caracteres."))]
        public string Title { get; set; }
    }
}
