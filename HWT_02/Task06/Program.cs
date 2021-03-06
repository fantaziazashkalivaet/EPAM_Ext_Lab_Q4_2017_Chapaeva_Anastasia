﻿/*
 Для выделения текстовой надписи можно использовать выделение
жирным, курсивом и подчёркиванием. Предложите способ хранения
информации о выделении надписи и напишите программу, которая
позволяет назначать и удалять текстовой надписи выделение
*/

namespace Task06
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var text = new TextStyle();
            bool exit = false;

            while (!exit)
            {
                text.StyleInfo();
                Console.WriteLine("Text style:\n\t1: bold\n\t2: italic\n\t3: underline");
                exit = Logic.ReadAction(text);
            }
        }
    }
}
