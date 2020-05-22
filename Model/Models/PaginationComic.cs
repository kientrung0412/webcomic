﻿using System.Collections.Generic;
 using System.Threading.Tasks;
 using Model.EF;

namespace Model.Models
{
    public class PaginationComic
    {
        
        private int pageSize;
        private int page;
        private Task<List<comic>> _comics;

        public PaginationComic(int pageSize, int page, Task<List<comic>> comics)
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

        public Task<List<comic>> Comics
        {
            get => _comics;
            set => _comics = value;
        }
    }
}