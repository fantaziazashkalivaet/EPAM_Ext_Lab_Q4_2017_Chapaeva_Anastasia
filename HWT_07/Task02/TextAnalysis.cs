namespace Task02
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class TextAnalysis
    {
        private Dictionary<string, int> words;
        private string fileName;
        private char[] separators;

        /// <summary>
        /// Инициализирует новый экземпляр класса TextAnalysis
        /// </summary>
        /// <param name="fileName">Имя анализируемого текстового файла</param>
        /// <param name="separators">массив символов-разделителей</param>
        public TextAnalysis(string fileName, char[] separators)
        {
            this.fileName = fileName;
            this.separators = separators;
            words = new Dictionary<string, int>();
            ReadText();
        }

        /// <summary>
        /// Словарь, содержащий слова и соответствующие им числа (количество вхождений данного слова в текст)
        /// </summary>
        public Dictionary<string, int> Words
        {
            get
            {
                return words;
            }
        }

        /// <summary>
        /// Выводит в консоль слова и количество их вхождений в текст
        /// </summary>
        public void PrintDictionary()
        {
            foreach (var word in words)
            {
                ForConsole.Write(string.Format(word.Key + " " + word.Value));
            }
        }

        /// <summary>
        /// Считывает текст из файла, записывает слова в словарь
        /// </summary>
        private void ReadText()
        {
            using (var sr = new StreamReader(fileName))
            {
                const int Step = 1;
                var separatedWords = sr.ReadToEnd().Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in separatedWords)
                {
                    int numberOfWords;
                    if (words.TryGetValue(word.ToLower(), out numberOfWords))
                    {
                        words[word.ToLower()] = numberOfWords + Step;
                    }
                    else
                    {
                        words.Add(word.ToLower(), numberOfWords + Step);
                    }
                }
            }
        }
    }
}
