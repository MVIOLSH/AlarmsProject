namespace Alarms.Logic.Models
{
    public class PageInfo
    {
        public int TotalItemCount { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int PageCount =>
            TotalItemCount % PageSize == 0
                ? TotalItemCount / PageSize
                : TotalItemCount / PageSize + 1;
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < PageCount;
        public bool IsFirstPage => PageNumber == 1;
        public bool IsLastPage => PageNumber == PageCount;
        public int FirstItemOnPage => 1;
        public int LastItemOnPage => 1;
        public int SkipQty => PageSize * (PageNumber - 1);
        public int PagerSize { get; set; } = 7;

        public Dictionary<int, string> PagesDictionary()
        {
            var firstPage = PageNumber - (int)Math.Floor(PagerSize / 2.0);
            var lastPage = PageNumber + (int)Math.Floor(PagerSize / 2.0);

            firstPage = firstPage < 1 ? 1 : firstPage;
            lastPage = lastPage > PageCount ? PageCount : lastPage;

            var pages = new Dictionary<int, string>();
            for (var i = firstPage; i <= lastPage; i++)
            {
                if (i == firstPage && firstPage != 1)
                {
                    pages[i] = "...";
                }
                else if (i == lastPage && lastPage != PageCount)
                {
                    pages[i] = "...";
                }
                else
                {
                    pages[i] = i.ToString();
                }
            }

            return pages;
        }

        public PageInfo Page(int i)
        {
            return new PageInfo
            {
                PageNumber = i,
                PageSize = 20,
                TotalItemCount = TotalItemCount
            };
        }


    }
}

