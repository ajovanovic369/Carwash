namespace CarWash.DTOs
{
    public class CarWashFilterDTO
    {
        public int Page { get; set; } = 1;
        public int RecordsPerPage { get; set; } = 10;
        public PaginationDTO Pagination
        {
            get { return new PaginationDTO() { Page = Page, RecordsPerPage = RecordsPerPage }; }
        }
        public string? Name { get; set; }
        public int CarWashServiceId { get; set; }
        public string? ServiceName { get; set; }
        public string? Address { get; set; }
        public bool? CarWashOpen { get; set; }
    }
}
