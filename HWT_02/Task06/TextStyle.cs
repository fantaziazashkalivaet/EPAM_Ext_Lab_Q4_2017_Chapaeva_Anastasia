namespace Task06
{
    using System;

    public class TextStyle
    {
        private bool none;
        private bool bold;
        private bool italic;
        private bool underline;

        public TextStyle()
        {
            this.bold = false;
            this.italic = false;
            this.underline = false;
            this.none = true;
        }

        public bool Bold
        {
            get
            {
                return this.bold;
            }
        }

        public bool Italic
        {
            get
            {
                return this.italic;
            }
        }

        public bool Underline
        {
            get
            {
                return this.underline;
            }
        }

        public void StyleInfo()
        {
            Console.WriteLine("Параметры надписи: {0}{1}{2}{3}", this.BoldInfo(), this.ItalicInfo(), this.UnderlineInfo(), this.NoneInfo());
        }

        public string NoneInfo()
        {
            if (this.none)
            {
                return "None ";
            }
            else
            {
                return null;
            }
        }

        public string BoldInfo()
        {
            if (this.bold)
            {
                return "Bold ";
            }
            else
            {
                return null;
            }
        }

        public string ItalicInfo()
        {
            if (this.italic)
            {
                return "Italic ";
            }
            else
            {
                return null;
            }
        }

        public string UnderlineInfo()
        {
            if (this.underline)
            {
                return "Underline ";
            }
            else
            {
                return null;
            }
        }

        public void ChangeBold()
        {
            if (this.bold)
            {
                this.bold = false;
            }
            else
            {
                this.bold = true;
                this.none = false;
            }
        }

        public void ChangeItalic()
        {
            if (this.italic)
            {
                this.italic = false;
            }
            else
            {
                this.italic = true;
                this.none = false;
            }
        }

        public void ChangeUnderline()
        {
            if (this.underline)
            {
                this.underline = false;
            }
            else
            {
                this.underline = true;
                this.none = false;
            }
        }

        public void StyleNone()
        {
            this.none = true;
        }
    }
}
