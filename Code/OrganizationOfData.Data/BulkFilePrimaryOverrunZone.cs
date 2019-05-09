namespace OrganizationOfData.Data
{
    using OrganizationOfData.Windows;
    using System;

    [Serializable]
    public class BulkFilePrimaryOverrunZone : BulkFile
    {

        private BucketPointer start;

        public BucketPointer Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value;
                NotifyPropertyChanged(nameof(Start));
            }
        }        

        public BulkFilePrimaryOverrunZone() : base()
        {

        }

        public override void FormEmptyBulkFile()
        {
            PrimaryZone = new BucketPointer[NumberOfBuckets];

            RecordPointer record;
            BucketPointer bucket;

            for (int i = 0; i < NumberOfBuckets; i++)
            {
                bucket = new BucketPointer(Factor, i)
                {
                    U = null,
                    B = null,
                    N = null,
                    E = Factor
                };
                for (int j = 0; j < Factor; j++)
                {
                    record = new RecordPointer()
                    {
                        Person = new Person()
                        {
                            Id = 0,
                            FullName = null,
                            Adress = null,
                            Age = 0
                        },
                        Status = Status.empty,
                        U = null
                    };
                    bucket.Records[j] = record;
                }
                PrimaryZone[i] = bucket;
            }
        }
    }
}
