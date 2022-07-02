using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Holmes_Services.Models.DomainModels
{
    public class Decking
    {
        [Required(ErrorMessage = "Railing Id required")]
        [RegularExpression(@"[0-9]+?", ErrorMessage = "Id must be numeric")]
        [Range(0, int.MaxValue, ErrorMessage = "Id must be a positive number")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Product code required")]
        [MaxLength(255, ErrorMessage = "Product code must be 255 characters or less")]
        [RegularExpression(@"[0-9]*?[a-zA-Z]*?[0-9]*?[a-zA-Z]*?", ErrorMessage = "Product code may contain letters and numbers only")]
        public string Product_Code { get; set; }


        [Required(ErrorMessage = "Name is required")]
        [MaxLength(255, ErrorMessage = "Name must be 255 characters or less")]
        [RegularExpression(@"[0-9]*?[a-zA-Z]*?[0-9]*?[a-zA-Z]*?", ErrorMessage = "Name may contain letters and numbers only")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Deck type is required")]
        [MaxLength(10, ErrorMessage = "Type id must be 10 characters or less")]
        [RegularExpression(@"[0-9]*?[a-zA-Z]*?[0-9]*?[a-zA-Z]*?", ErrorMessage = "Deck type may contain letters and numbers only")]
        public string Type_Id { get; set; }
        //nav property 
        [ForeignKey(nameof(Type_Id))]
        public Deck_Type Type { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(255, ErrorMessage = "Description must be 255 characters or less")]
        [RegularExpression(@"[0-9]*?[a-zA-Z]*?[0-9]*?[a-zA-Z]*?", ErrorMessage = "Deck type may contain letters and numbers only")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price per square foot required")]
        public int Group_Id { get; set; }

        [ForeignKey(nameof(Group_Id))]
        public Price_Groups Group { get; set; }

        [Required(ErrorMessage = "Price per square foot required")]
        [RegularExpression(@"\d*?|\D*?", ErrorMessage = "Price must be numeric")]
        public double Price_Per_SqFt { get; set; }


        [Required(ErrorMessage = "Image is required")]
        [MaxLength(255, ErrorMessage = "Image must be 255 characters or less")]
        public string Image { get; set; }

        // nav property
        public ICollection<Design> Designs { get; set; }

        public string Slug() => Product_Code + "-" + Name;

        public string GetFormattedPrice()
        {
            double num;
            try
            {
                num = Price_Per_SqFt;
            }
            catch (Exception ex)
            { throw; }

            return num.ToString("C");
        }
    }
}
