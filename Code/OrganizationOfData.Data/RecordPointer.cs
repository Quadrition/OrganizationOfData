namespace OrganizationOfData.Data
{
    using System;

    [Serializable]
    public class RecordPointer : Record
    {
        private Record nextRecord;

        public Record NextRecord
        {
            get
            {
                return nextRecord;
            }
            set
            {
                nextRecord = value;
                NotifyPropertyChanged(nameof(NextRecord));
            }
        }

        public RecordPointer()
        {

        }
    }
}
