namespace OrganizationOfData.Data
{
    using OrganizationOfData.Windows;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public abstract class BulkFile : Model
    {
        protected Bucket[] primaryZone;
        protected int factor;
        protected int numberOfBuckets;
        protected TransformationMethod transformationMethod;

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

        public BulkFile()
        {

        }

        public abstract void FormEmptyBulkFile();

        protected override string OnValidate(string propertyName)
        {
            return base.OnValidate(propertyName);
        }

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
