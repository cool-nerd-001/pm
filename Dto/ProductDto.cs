using System.ComponentModel.DataAnnotations;

namespace ProductMicroservice.Dto
{
    public class ProductDto
    {


            [Required]
            [MaxLength(25)]
            public string? Name { get; set; }



            [Required]
            [MaxLength(15)]
            public string? Brand { get; set; }


            [Required]
            [MaxLength(15)]
            public string? Category { get; set; }



            [Required]
            [Range(1, Int32.MaxValue, ErrorMessage = "Price Field is Required Or Price Should be a Positive Number")]
            public int? Price { get; set; }


            [Required]
            [MaxLength(1000)]
            public string? Description { get; set; }


            [Required]
            [Url]
            public string? Image { get; set; }

    }
}
