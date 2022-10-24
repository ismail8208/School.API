using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace School.API.ViewModels
{
    public class PaginationMetaData
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalPageCount { get; set; }
        public PaginationMetaData(int pageSize, int pageNumber, int totalItemCount)
        {
            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
            this.TotalPageCount = totalItemCount;
            this.TotalPageCount = (int)Math.Ceiling(totalItemCount/(double)pageSize);
        }
    }
}
