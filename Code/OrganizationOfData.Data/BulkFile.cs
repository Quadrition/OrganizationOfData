namespace OrganizationOfData.Data
{
    using OrganizationOfData.Windows;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    /// Represents a bulk file with primary zone
    /// </summary>
    [Serializable]
    public abstract class BulkFile : Model
    {
        protected Bucket[] primaryZone;
        protected int factor;
        protected int numberOfBuckets;
        protected TransformationMethod transformationMethod;

        public Bucket[] PrimaryZone
        {
            get
            {
                return primaryZone;
            }
            set
            {
                primaryZone = value;
                NotifyPropertyChanged(nameof(PrimaryZone));
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Faktor baketiranja mora biti pozitivan")]
        public int Factor
        {
            get
            {
                return factor;
            }
            set
            {
                factor = value;
                NotifyPropertyChanged(nameof(Factor));
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Broj baketa mora biti pozitivan")]
        public int NumberOfBuckets
        {
            get
            {
                return numberOfBuckets;
            }
            set
            {
                numberOfBuckets = value;
                NotifyPropertyChanged(nameof(NumberOfBuckets));
            }
        }

        public TransformationMethod TransformationMethod
        {
            get
            {
                return transformationMethod;
            }
            set
            {
                transformationMethod = value;
                NotifyPropertyChanged(nameof(TransformationMethod));
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BulkFile"/> class
        /// </summary>
        public BulkFile()
        {

        }

        /// <summary>
        /// Forms an empty bulk file along with <see cref="NumberOfBuckets"/> <see cref="Bucket"/>
        /// and <see cref="Factor"/> records inside of each <see cref="Bucket"/>
        /// </summary>
        public abstract void FormEmptyBulkFile();

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
                "NumberOfBuckets",
                "Factor"
            };

            return ValidatedProperties.FirstOrDefault(perp => OnValidate(perp) != null) == null;
        }
    }
}
