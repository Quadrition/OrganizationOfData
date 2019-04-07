namespace OrganizationOfData.Data
{
    using OrganizationOfData.Windows;
    using System.ComponentModel.DataAnnotations;

    public class Person : Model
    {
        private int id;
        private string fullName;
        private string adress;
        private int age;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Id osobe je obavezan")]
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                NotifyPropertyChanged(nameof(Id));
            }
        }

        public string FullName
        {
            get
            {
                return fullName;
            }
            set
            {
                fullName = value;
                NotifyPropertyChanged(nameof(FullName));
            }
        }

        public string Adress
        {
            get
            {
                return adress;
            }
            set
            {
                adress = value;
                NotifyPropertyChanged(nameof(Adress));
            }
        }

        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
                NotifyPropertyChanged(nameof(Age));
            }
        }
    }
}
