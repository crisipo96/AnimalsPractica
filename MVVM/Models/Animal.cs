

using PropertyChanged;

namespace AnimalsPractica.MVVM.Models
{
    [AddINotifyPropertyChangedInterface]
    public class Animal 
    {
        public int Id {  get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public  string Sex { get; set; } =string.Empty;
        public string DOB { get; set; } = string.Empty ;

    }
}
