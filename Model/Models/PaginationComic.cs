using System.Collections.Generic;
using Model.EF;

namespace Model.Models
{
    public class PaginationComic
    {
        private int pageSize;
        private int page;
        private List<comic> _comics;
        private List<ComicCategoryFilte> _categoryFiltes;

        public PaginationComic(int pageSize, int page, List<ComicCategoryFilte> categoryFiltes)
        {
            this.pageSize = pageSize;
            this.page = page;
            _categoryFiltes = categoryFiltes;
        }


        public PaginationComic(int pageSize, int page, List<comic> comics)
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

        public List<comic> Comics
        {
            get => _comics;
            set => _comics = value;
        }

        public List<ComicCategoryFilte> CategoryFiltes
        {
            get => _categoryFiltes;
            set => _categoryFiltes = value;
        }
    }
}