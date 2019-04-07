namespace OrganizationOfData.Data
{
    using OrganizationOfData.Windows;
    using System.ComponentModel.DataAnnotations;

    public abstract class BulkFile : Model
    {
        protected Bucket[] primaryZone;
        protected int factor;
        protected int numberOfBuckets;

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

        public BulkFile()
        {
            PrimaryZone = new Bucket[numberOfBuckets];
        }

        public abstract void FormEmptyBulkFile();
    }
}
