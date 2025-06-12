namespace dev_connect.Server.Data.Dto
{
    public class PaginationResponse<T>
    {
        public List<T>? Data { get; set; }
        public long TotalCount { get; set; }
        public int PageSize { get; set; }
    }
}
