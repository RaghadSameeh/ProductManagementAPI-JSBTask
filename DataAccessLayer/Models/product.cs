using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class product
    {
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Stock number must be a positive number.")]
        public int Stock { get; set; }

        [ForeignKey("order")]
        public string? OrderId { get; set; }
        public virtual order? order { get; set; }
    }
}

