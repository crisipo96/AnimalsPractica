using AnimalsPractica.MVVM.ModelViews;

namespace AnimalsPractica.MVVM.Views;

public partial class NewAnimalView : ContentPage
{
   
    public NewAnimalView()
	{
		InitializeComponent();
		BindingContext = new NewAnimalViewModel();
    }
}