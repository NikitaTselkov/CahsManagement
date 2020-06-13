using GalaSoft.MvvmLight.Messaging;
using System.Data.SqlClient;

namespace ViewModels.Navigation
{
    public class NavigateViewModel : SaveAndLoad
    {
        public NavigateViewModel()
        {

        }

        public void Navigate(string url)
        {
            Messenger.Default.Send<NavigateArgs>(new NavigateArgs(url));
        }

    }
}
