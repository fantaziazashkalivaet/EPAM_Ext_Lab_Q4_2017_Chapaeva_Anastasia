namespace Task04
{
    using System.Collections.Generic;
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

        public static MyString ToMyString(char[] arr)
        {
            return new MyString(arr);
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

        public MyString Replace(char newSymbol, char oldSymbol)
        {
            if (myString != null)
            {
                var newStr = myString;
                for (var i = 0; i < myString.Length; i++)
                {
                    if (myString[i] == oldSymbol)
                    {
                        newStr[i] = newSymbol;
                    }
                }
            }

            return this;
        }

        public int IndexOf(char symbol)
        {
            if (myString != null)
            {
                return IndexOf(symbol, 0, myString.Length);
            }

            return -1;
        }

        public int IndexOf(char symbol, int start, int end)
        {
            int count = -1;
            if (myString != null)
            {
                if (!(start < 0 || start > myString.Count() || end < start || end > myString.Count()))
                {
                    for (var i = start; i < end; i++)
                    {
                        if (myString[i] == symbol)
                        {
                            count = i;
                            return count;
                        }
                    }
                }
            }

            return count;
        }

        public int IndexOf(string s)
        {
            int count = -1;
            if (myString != null)
            {
                for (var i = 0; i < myString.Length; i++)
                {
                    if (i + s.Length < myString.Length)
                    {
                        for (var j = 0; j < s.Length; j++)
                        {
                            if (s[j] != myString[i + j])
                            {
                                break;
                            }

                            if (j == s.Length - 1)
                            {
                                return i;
                            }
                        }
                    }
                }
            }

            return count;
        }

        public MyString Insert(string s, int start)
        {
            if (myString != null)
            {
                if (start >= myString.Length || start < 0)
                {
                    return this;
                }

                var size = myString.Length + s.Length;
                var newMyString = new MyString(size);
                for (var i = 0; i < size; i++)
                {
                    if (i < start)
                    {
                        newMyString[i] = myString[i];
                    }
                    else
                    if (i < start + s.Length)
                    {
                        newMyString[i] = s[i - start];
                    }
                    else
                    {
                        newMyString[i] = myString[start + i - s.Length];
                    }
                }

                return newMyString;
            }

            return this;
        }

        public MyString[] Split()
        {
            if (myString == null)
            { 
                return new MyString[1] { this };
            }
            
            var count = new List<MyString>();

            for (var j = 0; j < myString.Length; j++)
            {
                var parts = new List<char>();

                while (!char.IsSeparator(myString[j]))
                {
                    parts.Add(myString[j]);
                    if (j < myString.Length - 1)
                    {
                        j++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (parts != null)
                {
                    count.Add(ToMyString(parts.ToArray()));
                }
            }

            return count.ToArray();
        }
    }
}
