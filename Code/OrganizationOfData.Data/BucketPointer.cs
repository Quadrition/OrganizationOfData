namespace OrganizationOfData.Data
{
    using System;

    [Serializable]
    public class BucketPointer : Bucket
    {
        private BucketPointer[] synonyms;
        private BucketPointer prev;
        private BucketPointer next;
        private int blankRecords;

        public BucketPointer(int factor, int address) : base(factor, address)
        {
            
        }

        public BucketPointer[] Synonyms
        {
            get
            {
                return synonyms;
            }
            
            set
            {
                synonyms = value;
                NotifyPropertyChanged(nameof(Synonyms));
            }
        }

        public BucketPointer Prev
        {
            get
            {
                return prev;
            }

            set
            {
                prev = value;
                NotifyPropertyChanged(nameof(Prev));
            }
        }

        public BucketPointer Next
        {
            get
            {
                return next;
            }

            set
            {
                next = value;
                NotifyPropertyChanged(nameof(Next));
            }
        }

        public int BlankRecords
        {
            get
            {
                return blankRecords;
            }

            set
            {
                blankRecords = value;
                NotifyPropertyChanged(nameof(BlankRecords));
            }
        }
    }
}
