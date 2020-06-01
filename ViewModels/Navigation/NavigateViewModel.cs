using GalaSoft.MvvmLight.Messaging;


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
