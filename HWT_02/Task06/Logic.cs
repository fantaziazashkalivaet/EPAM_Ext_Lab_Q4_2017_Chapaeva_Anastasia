namespace Task06
{
    using System;

    public class Logic
    {
        public static bool ReadAction(TextStyle text)
        {
            Console.WriteLine("Enter a number (1-3) to format the text or \"exit\" to complete:");
            string action = Console.ReadLine();
            return Logic.CheckAction(action, text);
        }

        public static bool CheckAction(string action, TextStyle text)
        {
            bool exit = false;
            action = action.ToLower();

            switch (action)
            {
                case "1":
                    {
                        text.ChangeBold();
                        break;
                    }

                case "2":
                    {
                        text.ChangeItalic();
                        break;
                    }

                case "3":
                    {
                        text.ChangeUnderline();
                        break;
                    }

                case "exit":
                    {
                        exit = true;
                        break;
                    }

                default:
                    {
                        Console.WriteLine("Incorrect data entered. Enter again:");
                        ReadAction(text);
                        break;
                    }
            }
            
            if (!text.Bold && !text.Italic && !text.Underline)
            {
                text.StyleNone();
            }

            return exit;
        }
    }
}
