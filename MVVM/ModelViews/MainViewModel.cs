
using AnimalsPractica.MVVM.Models;
using AnimalsPractica.MVVM.Views;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Windows.Input;

namespace AnimalsPractica.MVVM.ModelViews
{
    [AddINotifyPropertyChangedInterface]
    public  class MainViewModel
    {
        public  MainViewModel(Animal animal) {

            GetAnimals();
            if (animal != null) 
            {
                if (animal.Id == 0)
                {
                    var lastId = GetLastId(Animals);
                    animal.Id = lastId + 1;
                    AddAnimal(animal);
                }
                else
                {
                    ModifyAnimal(animal);
                    UpdateAnimal(animal);
                  
                 
                }
            }
        }

        private int _id;
        public int IDanimal
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    IDanimal = _id;
                }
            }
        }
        
        public ObservableCollection<Animal> Animals { get; set; } = new ObservableCollection<Animal>();

        private HttpClient _httpClient = new HttpClient();
        private JsonSerializerOptions _jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        private const string _urlBase = "https://678685a6f80b78923aa7301d.mockapi.io/api/v1";

        public async void GetAnimals()
        {
            var url = $"{_urlBase}/animals";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {

                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    List<Animal>? animals = await JsonSerializer.DeserializeAsync<List<Animal>>(stream, _jsonOptions);
                    if (animals != null)
                    {
                         animals.ForEach(a => Animals.Add(a));
                    }
                }
            }
            else
            {
                Console.WriteLine("Ha habido un error");
            }
        }

        public ICommand GetAnimalByIdCommand => new Command(async () =>
        {
            Animal? animal = null;
            try
            {
                var url = $"{_urlBase}/animals/{IDanimal}";
                animal = await _httpClient.GetFromJsonAsync<Animal>(url, _jsonOptions);
                await Application.Current!.MainPage!.DisplayAlert(animal.Name, $"Tipo: {animal.Type} , Sexo: {animal.Sex},  Nacimiento: {animal.DOB}" , "OK");

            }
            catch (Exception ex)
            {
                await Application.Current!.MainPage!.DisplayAlert("Buscar animal fallido", "No hay ningún animal con ese ID", "OK");
            }
        });
        
        public async void AddAnimal(Animal animal)
        {
          
            var url = $"{_urlBase}/animals";

            var json = JsonSerializer.Serialize(animal, _jsonOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            if (animal != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    Animals.Add(animal);
                    Animals.Clear();
                    GetAnimals();
                    await Application.Current!.MainPage!.DisplayAlert("Añadir animal", "Animal añadido con éxito", "OK");
                }
                else
                {
                    await Application.Current!.MainPage!.DisplayAlert("Añadir animal fallido", "No se ha podido añadir el animal", "OK");
                }
                
            }
            

        }

        public async void UpdateAnimal (Animal a)
        {

            Animal? animal = a as Animal;
            if (animal != null)
            {
                var url = $"{_urlBase}/animals/{animal.Id}";
              

                var json = JsonSerializer.Serialize(animal, _jsonOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    Animals.Clear();
                    GetAnimals();
                    await Application.Current!.MainPage!.DisplayAlert("Actualizar animal", "Animal actualizado con éxito", "OK");
                   
                   
                }
                else
                {
                    await Application.Current!.MainPage!.DisplayAlert("Error", "No se pudo actualizar el animal.", "OK");
                }
            }

        }

        public ICommand DeleteAnimalCommand => new Command(async (a) =>
        {
           
            Animal? animal = a as Animal;

            if(animal != null)
            {
                var url = $"{_urlBase}/animals/{animal.Id}";
                var response = await _httpClient.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    Animals.Remove(animal);
                    await Application.Current!.MainPage!.DisplayAlert("Eliminar animal", "Animal eliminado con éxito", "OK");
                }
                else
                {
                    await Application.Current!.MainPage!.DisplayAlert("Error", "No se pudo eliminar el animal.", "OK");
                }
            }
              
        });

        //Método que modifica un animal de la lista
        public void ModifyAnimal(Animal animal)
        {

           var existingAnimal = Animals.FirstOrDefault(a => a.Id == animal.Id); 
            if (existingAnimal != null)
            {
                existingAnimal.Name = animal.Name;
                existingAnimal.Type = animal.Type;
                existingAnimal.Sex = animal.Sex;
                existingAnimal.DOB = animal.DOB;
            }

        }
        //Método para obtener el id del último animal
        public int GetLastId(ObservableCollection<Animal> animals)
        {
            var lastTask = Animals.LastOrDefault();
            if (lastTask != null)
            {
                int lastId = lastTask.Id;
                return lastId;
            }
            return 0;
        }
        //Command para ir a la pantalla de añadir un animal
        public ICommand newAnimalViewCommand => new Command(async () => {

            await Application.Current!.MainPage!.Navigation.PushAsync(new NewAnimalView());
        });

        //Command para ir a la pantalla de actualizar un animal(pasamos como parametro el animal)
        public ICommand UpdateAnimalViewCommand => new Command(async (a) => {

            if (a is Animal animal)
            {
                await Application.Current!.MainPage!.Navigation.PushAsync(new UpdateAnimalView(animal));
            }
            
        });

    }
}
