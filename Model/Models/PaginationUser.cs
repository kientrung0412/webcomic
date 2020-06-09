using System.Collections.Generic;
using Model.EF;

namespace Model.Models
{
    public class PaginationUser
    {
        private int pageSize;
        private int page;
        private List<user> _comics;

        public PaginationUser(int pageSize, int page, List<user> comics)
        {
            this.pageSize = pageSize;
            this.page = page;
            _comics = comics;
        }

        public int PageSize
        {
            get => pageSize;
            set => pageSize = value;
        }

        public int Page
        {
            get => page;
            set => page = value;
        }

        public List<user> Comics
        {
            get => _comics;
            set => _comics = value;
        }
    }
}