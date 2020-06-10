using GalaSoft.MvvmLight.Command;

namespace ViewModels
{
    public class MainPageViewModel : Navigation.NavigateViewModel
    {
        public RelayCommand GoToMainPageCommand { get; set; }
        public RelayCommand GoToIncomePageCommand { get; set; }
        public RelayCommand GoToExpensesPageCommand { get; set; }

        /// <summary>
        /// Главный конструктор MainPageViewModel.
        /// </summary>
        public MainPageViewModel()
        {
            GoToMainPageCommand = new RelayCommand(GoToMainPageMethod);
            GoToIncomePageCommand = new RelayCommand(GoToIncomePageMethod);
            GoToExpensesPageCommand = new RelayCommand(GoToExpensesPageMethod);
        }


        /// <summary>
        /// Метод отправки в IncomePage (Доходы).
        /// </summary>
        public void GoToIncomePageMethod()
        {
            Navigate("Pages/IncomePage.xaml");
        }

        /// <summary>
        /// Метод отправки в ExpensesPage (Расходы).
        /// </summary>
        public void GoToExpensesPageMethod()
        {
            Navigate("Pages/ExpensesPage.xaml");
        }

        /// <summary>
        /// Метод отправки в MainPage.
        /// </summary>
        public void GoToMainPageMethod()
        {
            Navigate("Pages/MainPage.xaml");
        }
    }
}
