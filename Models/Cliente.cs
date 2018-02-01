using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaWebEF.Models
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCliente { get; set; }
        [Required]
        [StringLength(100, MinimumLength=2)]
        public string Nome { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Range(1,100)]
        public int Idade { get; set; }

        public ICollection<Pedido> Pedido{get;set;}
        

        
    }
}