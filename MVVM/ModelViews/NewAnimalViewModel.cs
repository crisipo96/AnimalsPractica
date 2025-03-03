
using AnimalsPractica.MVVM.Models;
using AnimalsPractica.MVVM.Views;
using System.Windows.Input;

namespace AnimalsPractica.MVVM.ModelViews
{
    public class NewAnimalViewModel
    {
        public  Animal Animal{ get; set; }

        //constructor
        public NewAnimalViewModel() {
            Animal = new Animal();
            Animal.Id = 0;
        }
        //Propiedades privadas
        private string _name;
        private string _type;
        private string _sex;
        private string _dob;

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
                    Name = _name;
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
                   Type = _type;
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
                    Sex = _sex;
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
                    DOB = _dob;
                }
            }
        }


        //Método que guarda el nuevo animal
        public Animal SaveAnimal()
        {
            Animal.Name = _name;
            Animal.Type = _type;
            Animal.Sex = _sex;
            Animal.DOB= _dob;
            return Animal;
        }
        //Método para volver la pantalla principal
        public ICommand AddAnimalGoBack => new Command(async () =>
        {
            Animal newAnimal= SaveAnimal();
            await Application.Current!.MainPage!.Navigation.PushAsync(new MainView(newAnimal));
        });
    }
}
