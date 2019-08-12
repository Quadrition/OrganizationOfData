namespace OrganizationOfData.Data
{
    using OrganizationOfData.Windows;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    /// Represents a bulk file with primary and overrun zone
    /// </summary>
    [Serializable]
    public class BulkFile : Model
    {
        public Bucket[] PrimaryZone { get; set; }
        public Record[] OverrunZone { get; set; }

        private int factor;

        public TransformationMethod TransformationMethod { get; set; }

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

        private int numberOfBuckets;

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

        private int numberOfRecordsInOverrunZone;

        /// <summary>
        /// Gets or sets number of records in overrun zone
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Broj slogova mora biti pozitivan")]
        public int NumberOfRecordsInOverrunZone
        {
            get
            {
                return numberOfRecordsInOverrunZone;
            }
            set
            {
                numberOfRecordsInOverrunZone = value;

                NotifyPropertyChanged(nameof(NumberOfRecordsInOverrunZone));
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
        public void FormEmptyBulkFile()
        {
            PrimaryZone = new Bucket[NumberOfBuckets];
            OverrunZone = new Record[NumberOfRecordsInOverrunZone];

            Bucket bucket;

            for (int i = 0; i < NumberOfBuckets; i++)
            {
                bucket = new Bucket(Factor, i);

                bucket.FormEmptyBucket();
                PrimaryZone[i] = bucket;
            }

            for (int i = 0; i < NumberOfRecordsInOverrunZone; i++)
            {
                OverrunZone[i] = new Record()
                {
                    Person = new Person(),
                    Status = Status.empty
                };
            }
        }

        // <summary>
        /// Checks if all entity's properties are valid
        /// </summary>
        /// <returns>True if all properties is valid, otherwise false</returns>
        public virtual bool IsValid
        {
            get
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
}
