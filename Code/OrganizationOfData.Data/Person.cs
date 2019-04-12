namespace OrganizationOfData.Data
{
    using OrganizationOfData.Windows;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    /// Reprezents a model class which is used as an example in application
    /// </summary>
    [Serializable]
    public class Person : Model
    {
        private int id;
        private string fullName;
        private string adress;
        private int? age;

        [Required(ErrorMessage = "Id osobe je obavezan")]
        [Range(1, int.MaxValue, ErrorMessage = "Id osobe mora biti između 1 i 2147483647")]
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

        [Range(0, 100, ErrorMessage = "Broj godina osobe može biti između 0 i 100")]
        public int? Age
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

        /// <summary>
        /// Checks if property is valid
        /// </summary>
        /// <param name="propertyName">A property to be validated</param>
        /// <returns>True if the property is valid, otherwise false</returns>
        protected override string OnValidate(string propertyName)
        {
            return base.OnValidate(propertyName);
        }

        // <summary>
        /// Checks if all entity's properties are valid
        /// </summary>
        /// <returns>True if all properties is valid, otherwise false</returns>
        public virtual bool IsValid()
        {
            string[] ValidatedProperties =
            {
                "Id",
                "FullName",
                "Adress",
                "Age"
            };

            return ValidatedProperties.FirstOrDefault(perp => OnValidate(perp) != null) == null;
        }
    }
}
