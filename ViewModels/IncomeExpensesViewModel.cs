using GalaSoft.MvvmLight.Command;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.Navigation;

namespace ViewModels
{
    public class IncomeExpensesViewModel : NavigateViewModel
    {

        /// <summary>
        /// Путь к строке расходов / доходов.
        /// </summary>
        private const string PATH_INCOME_EXPENSES = "IncomeExpenses.json";

        /// <summary>
        /// Текущая строка расходов / доходов.
        /// </summary>
        public IncomeExpenses CurrentIncomeExpenses { get; set; }

        /// <summary>
        /// Список расходов / доходов.
        /// </summary>
        public List<IncomeExpenses> IncomeExpenses { get; set; }

        /// <summary>
        /// Главный Конструктор.
        /// </summary>
        public IncomeExpensesViewModel()
        {
            CreateIncomeExpenses();
        }

        /// <summary>
        /// Метод создания строки расходов / доходов.
        /// </summary>
        public void CreateIncomeExpenses()
        {
            IncomeExpenses = new List<IncomeExpenses>();
            IncomeExpenses = Load<List<IncomeExpenses>>(PATH_INCOME_EXPENSES);

            if (IncomeExpenses == null)
            {
                IncomeExpenses = new List<IncomeExpenses>();
            }
        }

        /// <summary>
        /// Метод добавления строки расходов / доходов.
        /// </summary>
        public void Add()
        {
            if (CurrentIncomeExpenses == null)
            {
                CurrentIncomeExpenses = new IncomeExpenses();
            }

            var MaxId = IncomeExpenses.Count;

            CurrentIncomeExpenses.Id = MaxId + 1;
            CurrentIncomeExpenses.Title = "T" + (MaxId + 1);

            CurrentIncomeExpenses.Value.Add((DayOfWeek)1, 3000);
            CurrentIncomeExpenses.Value.Add((DayOfWeek)2, 0);
            CurrentIncomeExpenses.Value.Add((DayOfWeek)3, 0);
            CurrentIncomeExpenses.Value.Add((DayOfWeek)4, 0);
            CurrentIncomeExpenses.Value.Add((DayOfWeek)5, 4000);
            CurrentIncomeExpenses.Value.Add((DayOfWeek)6, 0);

            IncomeExpenses.Add(CurrentIncomeExpenses);

            Save(PATH_INCOME_EXPENSES, IncomeExpenses);
        }

        /// <summary>
        /// Метод удаления строки расходов / доходов.
        /// </summary>
        /// <param name="Id"> Id удаляемого обьекта. </param>
        public void Remove(int Id)
        {
            var result = new List<IncomeExpenses>();

            var IntValue = 1;
            foreach (var item in IncomeExpenses)
            {
                if (item.Id != Id)
                {
                    item.Id = IntValue;
                    IntValue++;

                    result.Add(item);
                }
            }

            Save(PATH_INCOME_EXPENSES, result);

        }

    }
}
