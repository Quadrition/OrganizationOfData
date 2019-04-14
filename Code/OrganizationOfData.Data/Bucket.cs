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
        protected Record[] records;

        public Record[] Records
        {
            get
            {
                return records;
            }
            set
            {
                records = value;
                NotifyPropertyChanged(nameof(Records));
            }
        }

        protected int address;

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

        /// <summary>
        /// Initializes a new instance of <see cref="Bucket"/>
        /// </summary>
        /// <param name="factor">Number of records inside of it</param>
        public Bucket(int factor, int address)
        {
            Records = new Record[factor];
            Address = address;
        }
    }
}
