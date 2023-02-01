using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfitsChallange.Models
{
    public enum ReviewOptions
    {
        O, A, B, C, D, E, F, G
    }

    public class Document
    {
        public Document()
        {
            this.RegisterDate = DateTime.Now;
        }

        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [MaxLength(10, ErrorMessage = "Limite máximo de 10 caracteres.")]
        [Display(Name = "Código")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [MaxLength(100, ErrorMessage = "Limite máximo de 100 caracteres.")]
        [Display(Name = "Título")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [Display(Name = "Revisão")]
        public ReviewOptions Review { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [Display(Name = "Data Planejada")]
        [DataType(DataType.DateTime)]
        public DateTime PlannedDate { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [Display(Name = "Valor")]
        [DataType(DataType.Currency)]
        public decimal Value { get; set; }

        public bool Removed { get; set; }

        [Display(Name = "Data de Registro")]
        [DataType(DataType.DateTime)]
        public DateTime RegisterDate { get; set; }

        [NotMapped]
        public int ReviewInt { get => (int)this.Review; }

        [NotMapped]
        public string ReviewLabel { get => this.Review.ToString(); }

        [NotMapped]
        public string PlannedDateLabel { get => this.PlannedDate.ToString("dd/MM/yyyy"); }

    }
}