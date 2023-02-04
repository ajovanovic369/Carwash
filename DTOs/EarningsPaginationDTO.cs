namespace CarWash.DTOs
{
    public class EarningsPaginationDTO
    {
        public int Page { get; set; } = 1;
        public int day { get; set; } = 0;
        public int month { get; set; } = 0;
        public int year { get; set; } = 0;

        public int dayT { get; set; } = 0;
        public int monthT { get; set; } = 0;
        public int yearT { get; set; } = 0;

        private int recordsPerPage = 5;
        private readonly int maxRecordsPerPage = 25;

        public int RecordsPerPage
        {
            get
            {
                return recordsPerPage;
            }
            set
            {
                recordsPerPage = (value > maxRecordsPerPage) ? maxRecordsPerPage : value;
            }
        }
    }
}
