namespace OrganizationOfData.Data
{
    using System;

    [Serializable]
    public class RecordPointer : Record
    {
        private Record u;

        public Record U
        {
            get
            {
                return u;
            }
            set
            {
                u = value;
                NotifyPropertyChanged(nameof(U));
            }
        }

        public RecordPointer()
        {

        }
    }
}
