using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BasicWebApi.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatorio.")]
        [MaxLength(60, ErrorMessage = ("Este campo deve conter entre 1 e 60 caracteres."))]
        [MinLength(1, ErrorMessage = ("Este campo deve conter entre 1 e 60 caracteres."))]
        public string Title { get; set; }

        [MaxLength(1024, ErrorMessage = ("Este campo deve conter no máximo 1024 caracteres."))]
        public string Description { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Categoria inválida.")]
        public int CategoryId { get; set; } // Referencia Category no banco

        public Category Category { get; set; } // Propriedade de navegação, para acessar informações de Category por meio do Product (Ex: Product.Category.Title)
    }
}
