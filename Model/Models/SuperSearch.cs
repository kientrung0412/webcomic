using System;
using System.Collections.Generic;

namespace Model.Models
{
    public class SuperSearch
    {
        private List<string> _listIn;
        private List<string> _listNotIn;
        private int _nationId;
        private int _statusId;
        private String _nameComic;


        public SuperSearch()
        {
            NationId = 0;
            StatusId = 0;
            ListIn = new List<string>();
            ListNotIn = new List<string>();
        }

        public SuperSearch(List<string> listIn, List<string> listNotIn, int nationId, int statusId, string nameComic)
        {
            _listIn = listIn;
            _listNotIn = listNotIn;
            _nationId = nationId;
            _statusId = statusId;
            _nameComic = nameComic;
        }

        public List<string> ListIn
        {
            get => _listIn;
            set => _listIn = value;
        }

        public List<string> ListNotIn
        {
            get => _listNotIn;
            set => _listNotIn = value;
        }

        public int NationId
        {
            get => _nationId;
            set => _nationId = value;
        }

        public int StatusId
        {
            get => _statusId;
            set => _statusId = value;
        }

        public string NameComic
        {
            get => _nameComic;
            set => _nameComic = value;
        }
    }
}