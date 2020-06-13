using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Navigation
{
    public class ViewModelLocator
    {

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IncomeViewModel>();

            SimpleIoc.Default.Register<MainPageViewModel>();

            SimpleIoc.Default.Register<ExpensesViewModel>();

        }

        public IncomeViewModel MainIncome
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IncomeViewModel>();
            }
        }

        public ExpensesViewModel MainExpenses
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ExpensesViewModel>();
            }
        }

        public MainPageViewModel MainPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainPageViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
