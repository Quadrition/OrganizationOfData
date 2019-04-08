namespace OrganizationOfData.Data
{
    using OrganizationOfData.Windows;
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Reprezents a model class which is used as an example in application
    /// </summary>
    [Serializable]
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

        [StringLength(32, ErrorMessage = "Ime osobe je preveliko")]
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

        [StringLength(32, ErrorMessage = "Adresa osobe je prevelika")]
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

        [Range(0, int.MaxValue, ErrorMessage = "Broj godina osobe je neispravan")]
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

        /// <summary>
        /// Initializes a new instance of <see cref="Person"/> class
        /// </summary>
        public Person()
        {

        }
    }
}
