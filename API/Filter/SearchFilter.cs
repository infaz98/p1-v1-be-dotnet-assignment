namespace API.Filter
{
    public class SearchFilter
    {
        public string? Pharam { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public SearchFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public SearchFilter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
