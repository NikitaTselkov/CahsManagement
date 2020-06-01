using System;
using ViewModels;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IncomeExpensesViewModel incomeExpensesView = new IncomeExpensesViewModel();

            Console.WriteLine("Current------------------");

            Console.WriteLine(incomeExpensesView.CurrentIncomeExpenses);

            Console.WriteLine("-------------------------");

            foreach (var item in incomeExpensesView.IncomeExpenses)
            {
                Console.WriteLine(item);
            }


            var IsStop = false;
            while (IsStop == false)
            {

                Console.WriteLine("1 = Add, 2 = Remove, 3 = Exit");

                var str = Console.ReadLine();
                int str1 = 0;

                if (!string.IsNullOrWhiteSpace(str))
                {
                    str1 = Convert.ToInt32(str);
                }

                switch (str1)
                {
                    case 1:

                        incomeExpensesView.Add();

                        Console.Clear();
                        break;
                    case 2:
                        Console.WriteLine("Введите Id который хотите удалить");

                        int Id = Convert.ToInt32(Console.ReadLine());

                        incomeExpensesView.Remove(Id);

                        Console.Clear();
                        break;
                    case 3:

                        Console.Clear();
                        IsStop = true;

                        break;
                    default:

                        Console.Clear();
                        Console.WriteLine("Nope");
                        break;
                }

                incomeExpensesView = new IncomeExpensesViewModel();

                Console.WriteLine("Current------------------");

                Console.WriteLine(incomeExpensesView.CurrentIncomeExpenses);

                Console.WriteLine("-------------------------");

                foreach (var item in incomeExpensesView.IncomeExpenses)
                {
                    Console.WriteLine(item);
                }
            }

        }
    }
}
