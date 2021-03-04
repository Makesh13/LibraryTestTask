using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Resource.Api.Models
{
    public class Book
    {
        [Required]
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Введите название")]
        public string Title { get; set; }
        public DateTime Year { get; set; }
        public string Gener { get; set; }
    }
}
