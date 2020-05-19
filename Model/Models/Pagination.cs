namespace Model.Models
{
    public class Pagination
    {
        private int size;
        private int page;

        public Pagination(int size, int page)
        {
            this.size = size;
            this.page = page;
        }

        public int Size
        {
            get => size;
            set => size = value;
        }

        public int Page
        {
            get => page;
            set => page = value;
        }
    }
}