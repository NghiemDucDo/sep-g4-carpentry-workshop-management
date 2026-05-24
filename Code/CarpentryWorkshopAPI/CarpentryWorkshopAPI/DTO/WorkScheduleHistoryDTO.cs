namespace CarpentryWorkshopAPI.DTO
{
    public class WorkScheduleHistoryDTO
    {
        public int HistoryId { get; set; }
        public int? WorkScheduleId { get; set; }
        public string? Action { get; set; }
        public string? ActionDatestring { get; set; }
        public int? CurrentEmployeeId { get; set; }
    }
}
