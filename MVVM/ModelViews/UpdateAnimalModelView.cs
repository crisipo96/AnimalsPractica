
using AnimalsPractica.MVVM.Models;
using AnimalsPractica.MVVM.Views;
using System.Windows.Input;

namespace AnimalsPractica.MVVM.ModelViews
{
    public class UpdateAnimalModelView
    {
        public Animal Animal { get; set; }

        //Propiedades privadas
        private int _id;
        private string _name;
        private string _type;
        private string _sex;
        private string _dob;

        //constructor
        public UpdateAnimalModelView(Animal animal) { 

           Animal = animal;
           _id= Animal.Id;
           _name= Animal.Name;
           _type= Animal.Type;
           _sex=  Animal.Sex;
           _dob= Animal.DOB;
        }

        //Propiedades públicas
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    Animal.Name = _name;
                }
            }
        }
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    Animal.Type = _type;
                }
            }
        }
        public string Sex
        {
            get
            {
                return _sex;
            }
            set
            {
                if (_sex != value)
                {
                    _sex = value;
                    Animal.Sex = _sex;
                }
            }
        }
        public string DOB
        {
            get
            {
                return _dob;
            }
            set
            {
                if (_dob != value)
                {
                    _dob = value;
                    Animal.DOB = _dob;
                }
            }
        }

        //Método que guarda el animal
        public Animal SaveAnimal()
        {
            Animal.Name = _name;
            Animal.Type = _type;
            Animal.Sex = _sex;
            Animal.DOB = _dob;
            return Animal;
        }
        //Método para  volver la pantalla principal
        public ICommand UpdateAnimalGoBack => new Command(async () =>
        {
            Animal updatedAnimal = SaveAnimal();
            await Application.Current!.MainPage!.Navigation.PushAsync(new MainView(updatedAnimal));
        });
    }
}
