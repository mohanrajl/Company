using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Company.Web.Models
{
    [Bind(Exclude = "VatAmount, GrossAmout, Active")]    
    public class SaleViewModel
    {
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }
        
        [Required(ErrorMessage = "Enter Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Enter Description")]
        [DataType(DataType.MultilineText)]
        [StringLength(250, ErrorMessage = "Description should be maximum 250 characters.")]
        [MinLength(4, ErrorMessage = "Description should be minimum four characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Enter Net amount")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid Net Amount")]
        public decimal NetAmount { get; set; }
        
        public bool VatApplied { get; set; }

        public bool Active { get; set; }

        public decimal VatAmount
        {
            get
            {
                if (VatApplied)
                {
                    return (this.NetAmount / 100) * 20;
                }

                return 0;
            }
        }

        public decimal GrossAmout
        {
            get
            {
                return this.NetAmount + this.VatAmount;
            }
        }
    }
}
