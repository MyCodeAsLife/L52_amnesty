using System;
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
        private List<Prisoner> _criminals = new List<Prisoner>();
        private string _amnestiableCrime;

        public CriminalDatabase()
        {
            _amnestiableCrime = "Антиправительственное";
            Fill();
        }

        enum Menu
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
                Console.WriteLine($"База данных преступников.\n" + new string(FormatOutput.DelimiterSymbolString, FormatOutput.DelimiterLenght) +
                                  $"\n{(int)Menu.BeforeAmnesty} - Список заключенных до Амнистии.\n{(int)Menu.AfterAmnesty} - Список " +
                                  $"заключенных после Амнистии.\n{(int)Menu.Exit} - Выйти из программы.\n" +
                                  new string(FormatOutput.DelimiterSymbolMenu, FormatOutput.DelimiterLenght));

                Console.Write("Выберите действие: ");

                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    Console.Clear();

                    switch ((Menu)number)
                    {
                        case Menu.BeforeAmnesty:
                            ShowBeforeAmnesty();
                            break;

                        case Menu.AfterAmnesty:
                            ShowAfterAmnesty();
                            break;

                        case Menu.Exit:
                            isOpen = false;
                            continue;

                        default:
                            Error.Show();
                            break;
                    }
                }
                else
                {
                    Error.Show();
                }

                Console.ReadKey(true);
            }
        }

        private void ShowBeforeAmnesty()
        {
            Show(_criminals);
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
            Console.WriteLine("Данные преступников.\n" + new string(FormatOutput.DelimiterSymbolMenu, FormatOutput.DelimiterLenght));

            foreach (var prisoner in _prisoners)
            {
                Console.WriteLine($"ФИО: {prisoner.FullName}\nПреступление: {prisoner.Crime}");
                Console.WriteLine(new string(FormatOutput.DelimiterSymbolString, FormatOutput.DelimiterLenght));
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

        internal class FormatOutput
        {
            static FormatOutput()
            {
                DelimiterSymbolMenu = '=';
                DelimiterSymbolString = '-';
                DelimiterLenght = 75;
            }

            public static char DelimiterSymbolMenu { get; private set; }
            public static char DelimiterSymbolString { get; private set; }
            public static int DelimiterLenght { get; private set; }
        }

        internal class Error
        {
            public static void Show()
            {
                Console.WriteLine("\nВы ввели некорректное значение.");
            }
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
