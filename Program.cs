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
        private string _delimiterString;
        private string _delimiterMenu;

        public CriminalDatabase()
        {
            char delimiterSymbolMenu = '=';
            char delimiterSymbolString = '-';
            int delimiterLenght = 75;

            _delimiterString = new string(delimiterSymbolString, delimiterLenght);
            _delimiterMenu = new string(delimiterSymbolMenu, delimiterLenght);
            _amnestiableCrime = "Антиправительственное";

            Fill();
        }

        private enum Menu
        {
            Show = 1,
            HoldAmnesty = 2,
            Exit = 3,
        }

        public void Run()
        {
            bool isOpen = true;

            while (isOpen)
            {
                Console.Clear();
                Console.WriteLine($"База данных преступников.\n" + _delimiterString + $"\n{(int)Menu.Show}" +
                                  $" - Показать список заключенных.\n{(int)Menu.HoldAmnesty} - Провести амнистию." +
                                  $"\n{(int)Menu.Exit} - Выйти из программы.\n" + _delimiterMenu);

                Console.Write("Выберите действие: ");

                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    Console.Clear();

                    switch ((Menu)number)
                    {
                        case Menu.Show:
                            Show(_criminals);
                            break;

                        case Menu.HoldAmnesty:
                            HoldAmnesty();
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

        private void HoldAmnesty()
        {
            List<Prisoner> filteredPrisoners = _criminals.Where(prisoner => prisoner.Crime != _amnestiableCrime).ToList();

            if (filteredPrisoners.Count == _criminals.Count)
            {
                Console.WriteLine("Амнистировать некого.");
            }
            else
            {
                Console.WriteLine("Амнистия проведена");
                _criminals = filteredPrisoners;
            }
        }

        private void Show(List<Prisoner> prisoners)
        {
            Console.WriteLine("Данные преступников.\n" + _delimiterMenu);

            foreach (var prisoner in prisoners)
                Console.WriteLine($"ФИО: {prisoner.FullName}\nПреступление: {prisoner.Crime}\n" + _delimiterString);
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
