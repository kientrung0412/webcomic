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
        private String _authorComic;


        public SuperSearch()
        {
            NationId = 0;
            StatusId = 0;
            ListIn = new List<string>();
            ListNotIn = new List<string>();
        }

        public SuperSearch(List<string> listIn, List<string> listNotIn, int nationId, int statusId, string nameComic, string authorComic)
        {
            _listIn = listIn;
            _listNotIn = listNotIn;
            _nationId = nationId;
            _statusId = statusId;
            _nameComic = nameComic;
            _authorComic = authorComic;
        }

        public string AuthorComic
        {
            get => _authorComic;
            set => _authorComic = value;
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