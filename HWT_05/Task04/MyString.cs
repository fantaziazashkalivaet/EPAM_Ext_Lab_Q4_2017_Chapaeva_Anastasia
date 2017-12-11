namespace Task04
{
    using System.Linq;
    using System.Text;

    public class MyString
    {
        private char[] myString;

        public MyString()
        {
            myString = new char[256];
        }

        public MyString(int size)
        {
            if (size > 0)
            {
                myString = new char[size];
            }
        }

        public MyString(char[] arr)
        {
            myString = arr;
        }

        public MyString(char symbol, int length)
        {
            myString = new char[length];
            for (var i = 0; i < length; i++)
            {
                myString[i] = symbol;
            }
        }

        public char[] MyStr
        {
            get
            {
                return myString;
            }
        }

        public int Length
        {
            get
            {
                try
                {
                    return myString.Count();
                }
                catch
                {
                    return 0;
                }
            }
        }

        public char this[int i]
        {
            get
            {
                return myString[i];
            }

            set
            {
                if (i >= 0 && i < myString.Count())
                {
                    myString[i] = value;
                }
            }
        }

        public static MyString operator +(MyString first, MyString second)
        {
            var length = first.Length + second.Length;
            var newMyString = new MyString(length);

            for (var i = 0; i < first.Length; i++)
            {
                newMyString[i] = first[i];
            }

            for (var i = first.Length; i < length; i++)
            {
                newMyString[i] = second[i - first.Length];
            }

            return newMyString;
        }

        public static bool operator ==(MyString first, MyString second)
        {
            if (first.Length != second.Length)
            {
                return false;
            }

            for (var i = 0; i < first.Length; i++)
            {
                if (first[i] != second[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator !=(MyString first, MyString second)
        {
            return !(first == second);
        }

        public override string ToString()
        {
            var s = new StringBuilder();
            try
            {
                for (var i = 0; i < myString.Count(); i++)
                {
                    s.Append(myString[i]);
                }
            }
            catch
            {
                return string.Empty;
            }

            return s.ToString();
        }

        public MyString TrimStart()
        {
            int i;
            if (myString != null)
            {
                for (i = 0; i < myString.Length; i++)
                {
                    if (!char.IsSeparator(myString[i]))
                    {
                        break;
                    }
                }

                var newStr = new MyString(myString.Length - i);
                for (var j = 0; j < newStr.Length; j++)
                {
                    newStr[j] = myString[i + j];
                }

                return newStr;
            }

            return this;
        }

        public MyString TrimEnd()
        {
            int i;
            if (myString != null)
            {
                for (i = myString.Length - 1; i >= 0; i--)
                {
                    if (!char.IsSeparator(myString[i]))
                    {
                        break;
                    }
                }

                var newStr = new MyString(i + 1);
                for (var j = 0; j < newStr.Length; j++)
                {
                    newStr[j] = myString[j];
                }

                return newStr;
            }

            return this;
        }

        public MyString Trim()
        {
            return this.TrimEnd().TrimStart();
        }

        public MyString ToLower()
        {
            if (myString != null)
            {
                var newStr = new MyString(myString.Count());
                for (var i = 0; i < myString.Count(); i++)
                {
                    newStr[i] = char.ToLower(myString[i]);
                }

                return newStr;
            }

            return this;
        }

        public MyString ToUpper()
        {
            if (myString != null)
            {
                var newStr = new MyString(myString.Count());
                for (var i = 0; i < myString.Count(); i++)
                {
                    newStr[i] = char.ToUpper(myString[i]);
                }

                return newStr;
            }

            return this;
        }
    }
}
