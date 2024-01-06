﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace L52_amnesty
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CriminalDatabase database = new CriminalDatabase();

            database.Run();
        }
    }

    class CriminalDatabase
    {
        private FormatOutput _format = new FormatOutput();
        private List<Prisoner> _criminals = new List<Prisoner>();
        private string _amnestiableCrime;

        public CriminalDatabase()
        {
            _amnestiableCrime = "Антиправительственное";
            Fill();
        }

        private enum Menu
        {
            BeforeAmnesty = 1,
            AfterAmnesty = 2,
            Exit = 3,
        }

        public void Run()
        {
            bool isOpen = true;

            while (isOpen)
            {
                Console.Clear();
                Console.WriteLine($"База данных преступников.\n" + new string(_format.DelimiterSymbolString, _format.DelimiterLenght) +
                                  $"\n{(int)Menu.BeforeAmnesty} - Список заключенных до Амнистии.\n{(int)Menu.AfterAmnesty} - Список " +
                                  $"заключенных после Амнистии.\n{(int)Menu.Exit} - Выйти из программы.\n" +
                                  new string(_format.DelimiterSymbolMenu, _format.DelimiterLenght));

                Console.Write("Выберите действие: ");

                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    Console.Clear();

                    switch ((Menu)number)
                    {
                        case Menu.BeforeAmnesty:
                            Show(_criminals);
                            break;

                        case Menu.AfterAmnesty:
                            ShowAfterAmnesty();
                            break;

                        case Menu.Exit:
                            isOpen = false;
                            continue;

                        default:
                            ShowError();
                            break;
                    }
                }
                else
                {
                    ShowError();
                }

                Console.ReadKey(true);
            }
        }

        private bool TryHoldAnAmnesty(out List<Prisoner> filteredPrisoners)
        {
            filteredPrisoners = _criminals.Where(prisoner => prisoner.Crime != _amnestiableCrime).ToList();

            if (filteredPrisoners.Count > 0)
                return true;

            filteredPrisoners = null;
            return false;
        }

        private void ShowAfterAmnesty()
        {
            if (TryHoldAnAmnesty(out List<Prisoner> remainingPrisoners))
                Show(remainingPrisoners);
            else
                Console.WriteLine("Амнистировать некого.");
        }

        private void Show(List<Prisoner> _prisoners)
        {
            Console.WriteLine("Данные преступников.\n" + new string(_format.DelimiterSymbolMenu, _format.DelimiterLenght));

            foreach (var prisoner in _prisoners)
            {
                Console.WriteLine($"ФИО: {prisoner.FullName}\nПреступление: {prisoner.Crime}");
                Console.WriteLine(new string(_format.DelimiterSymbolString, _format.DelimiterLenght));
            }
        }

        private void Fill()
        {
            _criminals.Add(new Prisoner("Раскольников Родион Романович", "Убийство"));
            _criminals.Add(new Prisoner("Гевара Эрнесто Че", "Антиправительственное"));
            _criminals.Add(new Prisoner("Лектер Ганнибал", "Канибализм"));
            _criminals.Add(new Prisoner("Капоне Альфонсе Габриэль", "Терроризм"));
            _criminals.Add(new Prisoner("Чичиков Павел Иванович", "Антиправительственное"));
        }

        private void ShowError()
        {
            Console.WriteLine("\nВы ввели некорректное значение.");
        }

        private class FormatOutput
        {
            public FormatOutput()
            {
                DelimiterSymbolMenu = '=';
                DelimiterSymbolString = '-';
                DelimiterLenght = 75;
            }

            public char DelimiterSymbolMenu { get; private set; }
            public char DelimiterSymbolString { get; private set; }
            public int DelimiterLenght { get; private set; }
        }
    }

    class Prisoner
    {
        public Prisoner(string fullName, string crime)
        {
            FullName = fullName;
            Crime = crime;
        }

        public string FullName { get; private set; }
        public string Crime { get; private set; }
    }
}
