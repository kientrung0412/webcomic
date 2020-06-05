using System;

namespace Model.Models
{
    public class Messenge
    {
        public int Code
        {
            get => code;
            set => code = value;
        }

        public string Mss
        {
            get => mss;
            set => mss = value;
        }

        private int code;
        private String mss;
    }
}