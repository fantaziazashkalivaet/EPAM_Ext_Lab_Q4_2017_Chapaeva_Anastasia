namespace Task06
{
    using System;

    public class Logic
    {
        public static void CheckAction(int action, TextStyle text)
        {
            switch (action)
            {
                case 1:
                    {
                        text.ChangeBold();
                        break;
                    }

                case 2:
                    {
                        text.ChangeItalic();
                        break;
                    }

                case 3:
                    {
                        text.ChangeUnderline();
                        break;
                    }
            }
            
            if (!text.Bold && !text.Italic && !text.Underline)
            {
                text.StyleNone();
            }
        }

        public static int CheckValue()
        {
            int value = SetValue();

            if (value > 3 || value < 1)
            {
                Console.WriteLine("Incorrect data entered.");
                value = CheckValue();
            }

            return value;
        }

        public static int SetValue()
        {
            Console.WriteLine("Enter number:");
            double value;

            if (!double.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Incorrect data entered.");
                value = SetValue();
            }

            return (int)value;
        }
    }
}
