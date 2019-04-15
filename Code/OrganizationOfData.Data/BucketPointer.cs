namespace OrganizationOfData.Data
{
    using System;

    [Serializable]
    public class BucketPointer : Bucket
    {
        private RecordPointer u;
        private BucketPointer b;
        private BucketPointer n;
        private int e;

        public BucketPointer(int factor, int address) : base(factor, address)
        {
            
        }

        public RecordPointer U
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

        public BucketPointer B
        {
            get
            {
                return b;
            }

            set
            {
                b = value;
                NotifyPropertyChanged(nameof(B));
            }
        }

        public BucketPointer N
        {
            get
            {
                return n;
            }

            set
            {
                n = value;
                NotifyPropertyChanged(nameof(N));
            }
        }

        public int E
        {
            get
            {
                return e;
            }

            set
            {
                e = value;
                NotifyPropertyChanged(nameof(E));
            }
        }
    }
}
