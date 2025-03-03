using AnimalsPractica.MVVM.Models;
using AnimalsPractica.MVVM.ModelViews;

namespace AnimalsPractica.MVVM.Views;

public partial class UpdateAnimalView : ContentPage
{
    public Animal Animal { get; set; }
    public UpdateAnimalView(Animal animal)
	{
		InitializeComponent();
		BindingContext = new UpdateAnimalModelView(animal);
		Animal=animal;
	}
}