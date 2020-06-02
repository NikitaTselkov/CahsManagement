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

            SimpleIoc.Default.Register<IncomeExpensesViewModel>();

            SimpleIoc.Default.Register<MainPageViewModel>();

        }

        public IncomeExpensesViewModel MainIncomeExpenses
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IncomeExpensesViewModel>();
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
