using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels.Navigation;

namespace ViewModels
{
    public class IncomeViewModel : NavigateViewModel
    {

        #region Дата

        /// <summary>
        /// Текущая дата.
        /// </summary>
        public DateTime CurrentDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Теукщий день.
        /// </summary>
        private int currentDay = 100;
        public int CurrentDay
        {
            get
            {
                if (currentDay == 100)
                {
                    return CurrentDate.Day;
                }
                else
                {
                    return currentDay;
                }
            }

            set
            {
                currentDay = value;
                RaisePropertyChanged("");
            }
        }

        /// <summary>
        /// Теукщий год.
        /// </summary>
        private int currentYear = 0;
        public int CurrentYear
        {
            get
            {
                if (currentYear == 0)
                {
                    return CurrentDate.Year;
                }
                else
                {
                    return currentYear;
                }
            }

            set
            {
                currentYear = value;
                RaisePropertyChanged("");
            }
        }

        /// <summary>
        /// Теукщий Месяц.
        /// </summary>
        private Months currentMonth = 0;
        public Months CurrentMonth
        {
            get
            {
                if (currentMonth == 0)
                {

                    Months result = currentMonth;

                    foreach (Months months in Enum.GetValues(typeof(Months)))
                    {
                        if (CurrentDate.Month == (int)months)
                        {
                            result = months;
                        }
                    }

                    return result;
                }
                else
                {
                    return currentMonth;
                }

            }
            set
            {
                currentMonth = value;
                RaisePropertyChanged("");
            }
        }

        /// <summary>
        /// Началась ли новая неделя.
        /// </summary>
        public bool IsNewWeek { get; set; } = Load<bool>(PATH_NEW_WEEK);

        /// <summary>
        /// Теукщий день недели.
        /// </summary>
        public DayOfWeek CurrentDayOfWeek
        {
            get { return CurrentDate.DayOfWeek; }
        }

        public RelayCommand RightButtonCommand { get; set; }
        public RelayCommand LeftButtonCommand { get; set; }

        /// <summary>
        /// Метод перемещающий на неделью в перед.
        /// </summary>
        public void RightButtonMethod(object param)
        {
            int days = DateTime.DaysInMonth(CurrentYear, (int)CurrentMonth);
            int week = 7;
            int months = 12;
            string Path = CURRENT_PATH_INCOME;
            int indexPath = PathsIncome.IndexOf(Path);


            if (PathsIncome.Count > indexPath + 1)
            {
                CURRENT_PATH_INCOME = PathsIncome[indexPath + 1];

                CurrentDay += week;

                if (CurrentDay > days)
                {
                    if ((int)CurrentMonth != months)
                    {
                        CurrentMonth++;
                    }
                    else
                    {
                        CurrentMonth = (Months)1;
                        CurrentYear++;
                    }

                    CurrentDay -= days;
                }

                Refresh();
            }
        }

        /// <summary>
        /// Метод перемещающий на неделью назад.
        /// </summary>
        public void LeftButtonMethod(object param)
        {
            int month = (int)CurrentMonth;
            int week = 7;
            string Path = CURRENT_PATH_INCOME;
            int indexPath = PathsIncome.IndexOf(Path);

            if (PathsIncome.Count > indexPath && indexPath > 0)
            {
                CURRENT_PATH_INCOME = PathsIncome[indexPath - 1];

                CurrentDay -= week;

                if (CurrentDay <= 0)
                {
                    if (CurrentMonth != Months.Январь)
                    {
                        CurrentMonth--;
                        month -= 1;
                    }
                    else
                    {
                        CurrentMonth = (Months)12;
                        month = 12;
                        CurrentYear--;
                    }

                    int days = DateTime.DaysInMonth(CurrentYear, month);

                    CurrentDay += days;
                }

                Refresh();
            }
        }

        #endregion

        #region Пути

        /// <summary>
        /// Путь к текущей строке доходов.
        /// </summary>
        private string CURRENT_PATH_INCOME { get; set; }

        /// <summary>
        /// Путь к списку всех путей к строкам доходов.
        /// </summary>
        private const string PATH_ALL_INCOMES = "AllPathsIncome.json";

        /// <summary>
        /// Путь к значению IsNewWeek.
        /// </summary>
        private const string PATH_NEW_WEEK = "NewWeekIncome.json";

        /// <summary>
        /// Список всех путей к строкам доходов.
        /// </summary>
        private List<string> PathsIncome { get; set; }

        #endregion

        /// <summary>
        /// Текущая строка доходов.
        /// </summary>
        public IncomeExpenses CurrentIncomeExpenses { get; set; }

        /// <summary>
        /// Нужно ли показывать CheckBox удаления.
        /// </summary>
        public Visibility IsRemove { get; set; } = Visibility.Collapsed;

        /// <summary>
        /// Список доходов.
        /// </summary>
        public List<IncomeExpenses> Incomes { get; set; }

        public RelayCommand AddRowCommand { get; set; }
        public RelayCommand SaveRowCommand { get; set; }
        public RelayCommand RemoveRowCommand { get; set; }

        /// <summary>
        /// Главный Конструктор.
        /// </summary>
        public IncomeViewModel()
        {
            AddRowCommand = new RelayCommand(Add);
            SaveRowCommand = new RelayCommand(Save);
            RemoveRowCommand = new RelayCommand(Remove);

            RightButtonCommand = new RelayCommand(RightButtonMethod);
            LeftButtonCommand = new RelayCommand(LeftButtonMethod);

            PathsIncome = GetData<string>(PATH_ALL_INCOMES);

            GetCurrentPath();

            if (CheckNewWeek())
            {
                if (IsNewWeek == false)
                {
                    CreateNewTable();
                    CreateNewTable();

                    GetCurrentPath();

                    Refresh();

                    IsNewWeek = true;

                    Save(PATH_NEW_WEEK, IsNewWeek);
                }
            }
            else
            {
                IsNewWeek = false;

                Save(PATH_NEW_WEEK, IsNewWeek);
            }

            Incomes = GetData<IncomeExpenses>(CURRENT_PATH_INCOME);

        }

        /// <summary>
        /// Метод создания новой таблицы.
        /// </summary>
        private void CreateNewTable()
        {
            Never:
            var Path = Guid.NewGuid().ToString() + ".json";

            if (PathsIncome.FirstOrDefault(s => s.Equals(Path)) == null)
            {
                PathsIncome.Add(Path);

                Save(PATH_ALL_INCOMES, PathsIncome);
            }
            else
            {
                goto Never;
            }
        }

        /// <summary>
        /// Метод обновляйщий таблицу.
        /// </summary>
        private void Refresh()
        {
            Incomes = GetData<IncomeExpenses>(CURRENT_PATH_INCOME);
            RaisePropertyChanged("");
        }

        /// <summary>
        /// Метод получения текущего пути.
        /// </summary>
        private void GetCurrentPath()
        {
            var path = PathsIncome.LastOrDefault();
            if (path == null)
            {
                CURRENT_PATH_INCOME = "IncomeExpenses.json";
            }
            else 
            {
                CURRENT_PATH_INCOME = path;
            }
        }

        /// <summary>
        /// Метод проверка на новую неделю.
        /// </summary>
        private bool CheckNewWeek()
        {
            if (CurrentDayOfWeek == DayOfWeek.Monday)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Метод загрузки данных.
        /// </summary>
        private List<T> GetData<T>(string Path)
        {
            var List = new List<T>();
            List = Load<List<T>>(Path);

            if (List == null)
            {
                List = new List<T>();
            }
            return List;
        }

        /// <summary>
        /// Метод добавления строки доходов.
        /// </summary>
        private void Add(object param)
        {
            CurrentIncomeExpenses = new IncomeExpenses();

            var MaxId = Incomes.Count;

            CurrentIncomeExpenses.Title = "NewTitle" + (MaxId + 1);

            Incomes.Add(CurrentIncomeExpenses);

            Save(CURRENT_PATH_INCOME, Incomes);

            Refresh();
        }

        /// <summary>
        /// Метод удаления строки доходов.
        /// </summary>
        private void Remove(object param)
        {
            if (IsRemove == Visibility.Collapsed)
            {
                IsRemove = Visibility.Visible;

                Save(CURRENT_PATH_INCOME, Incomes);
            }
            else
            {
                var result = new List<IncomeExpenses>();

                foreach (var item in Incomes)
                {
                    if (item.Check == false)
                    {

                        result.Add(item);
                    }
                }

                Save(CURRENT_PATH_INCOME, result);

                IsRemove = Visibility.Collapsed;
            }

            Refresh();
        }

        /// <summary>
        /// Метод сохранения изменений.
        /// </summary>
        private void Save(object param)
        {
            Save(CURRENT_PATH_INCOME, Incomes);
        }

    }
}
