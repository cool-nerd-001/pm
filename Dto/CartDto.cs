using System.ComponentModel.DataAnnotations;

namespace ProductMicroservice.Dto
{
    public class ProductIdList
    {
        [Required]
        public List<Guid>? array { get; set; }
  
    }

    public class ProductListForCartDto
    {
        public Guid PId { get; set; }

        public string? Name { get; set; }

        public int Price { get; set; }

        public string? Image { get; set; }   
    }

}
