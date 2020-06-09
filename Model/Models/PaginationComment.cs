using System.Collections.Generic;
using Model.EF;

namespace Model.Models
{
    public class PaginationComment
    {
        private int pageSize;
        private int page;
        private List<comment> _comments;

        public PaginationComment(int pageSize, int page, List<comment> comments)
        {
            this.pageSize = pageSize;
            this.page = page;
            _comments = comments;
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

        public List<comment> Comments
        {
            get => _comments;
            set => _comments = value;
        }
    }
}