namespace ProductMicroservice.Dto
{
    public class ProductParamsDto
    {
        public string? OrderBy { get; set; }

        public string? SearchTerm { get; set; }

        public string? Brands { get; set; }

        public string? Types { get; set; }

        private const int maxPageSize = 50;

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 6;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > maxPageSize ? maxPageSize : value;
        }
    }
}
