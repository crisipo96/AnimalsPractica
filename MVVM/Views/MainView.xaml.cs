
using AnimalsPractica.MVVM.Models;
using AnimalsPractica.MVVM.ModelViews;

namespace AnimalsPractica.MVVM.Views;

public partial class MainView : ContentPage
{
	public Animal Animal { get; set; }
	public MainView(Animal a)
	{
		
		InitializeComponent();
		BindingContext = new MainViewModel(a);
		Animal = a;
	}
}