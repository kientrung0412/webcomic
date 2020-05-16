namespace Model.Models
{
    public class Rating
    {
        private int comicId;
        private int point;

        public Rating(int comicId, int point)
        {
            this.comicId = comicId;
            this.point = point;
        }

        public int ComicId
        {
            get => comicId;
            set => comicId = value;
        }

        public int Point
        {
            get => point;
            set => point = value;
        }
    }
}