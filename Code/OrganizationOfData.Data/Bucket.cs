﻿namespace OrganizationOfData.Data
{
    using OrganizationOfData.Windows;

    public class Bucket : Model
    {
        private Record[] records;

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

        public Bucket(int factor)
        {
            Records = new Record[factor];
        }
    }
}
