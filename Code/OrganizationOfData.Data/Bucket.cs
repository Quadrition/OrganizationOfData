namespace OrganizationOfData.Data
{
    using OrganizationOfData.Windows;
    using System;

    /// <summary>
    /// Represents a list of Records inside of it
    /// </summary>
    [Serializable]
    public class Bucket : Model
    {
        public Record[] Records { get; set; }

        private int address;

        public int Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
                NotifyPropertyChanged(nameof(Address));
            }
        }

        private int overrunedRecords;

        public int OverrunedRecords
        {
            get
            {
                return overrunedRecords;
            }
            set
            {
                overrunedRecords = value;

                NotifyPropertyChanged(nameof(OverrunedRecords));
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Bucket"/>
        /// </summary>
        /// <param name="factor">Number of records inside of it</param>
        public Bucket(int factor, int address)
        {
            Records = new Record[factor];
            Address = address;
            OverrunedRecords = 0;
        }

        /// <summary>
        /// Forms an empty bulk file along with <see cref="NumberOfBuckets"/> <see cref="Bucket"/>
        /// and <see cref="Factor"/> records inside of each <see cref="Bucket"/>
        /// </summary>
        public void FormEmptyBucket()
        {
            Record record;

            for (int j = 0; j < Records.Length; j++)
            {
                record = new Record()
                {
                    Person = new Person(),
                    Status = Status.empty
                };
                Records[j] = record;
            }
        }
    }
}
