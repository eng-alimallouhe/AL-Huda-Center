namespace LMS.Application.DTOs.Common
{
    public class PagedResult <T>
    {
        public ICollection<T> Items { get; set; } 
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        public bool HasNext => CurrentPage < TotalPages;
        public bool HasPrevious => CurrentPage > 1;

        public PagedResult()
        {
            
        }

        public PagedResult(ICollection<T> items, int count, int pageSize, int currentPage)
        {
            Items = items;
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = currentPage;
        }
    }
}
