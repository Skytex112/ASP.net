using ProductCQRS.Profiles;

namespace ProductCQRS.Services
{
    public class PaginationService
    {
        private readonly PaginationProfile _pagination;
        public PaginationService(PaginationProfile pagination)
        {
            _pagination = pagination;
        }

        public int GetPageNumber() => _pagination.PageNumber;
        public int GetPageSize() => _pagination.PageSize;

        public (int skip, int take) GetSkipTake()
        {
            int skip = (_pagination.PageNumber - 1) * _pagination.PageSize;
            int take = _pagination.PageSize;
            return (skip, take);
        }
    }
}