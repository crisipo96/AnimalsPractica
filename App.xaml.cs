using AnimalsPractica.MVVM.Models;
using AnimalsPractica.MVVM.Views;

namespace AnimalsPractica
{
    public partial class App : Application
    {
        Animal animal = null;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainView(animal));
        
        }
    }
}
