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

        /// <summary>
        /// Initializes a new instance of <see cref="Bucket"/>
        /// </summary>
        /// <param name="factor">Number of records inside of it</param>
        public Bucket(int factor)
        {
            Records = new Record[factor];
        }
    }
}
