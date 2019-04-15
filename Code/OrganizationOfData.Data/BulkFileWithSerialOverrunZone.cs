namespace OrganizationOfData.Data
{
    using OrganizationOfData.Windows;
    using System;

    [Serializable]
    public class BulkFileWithSerialOverrunZone : BulkFile
    {
        public Bucket[] OverrunZone;

        public BulkFileWithSerialOverrunZone() : base()
        {

        }

        public override void FormEmptyBulkFile()
        {
            PrimaryZone = new Bucket[NumberOfBuckets];
            OverrunZone = new Bucket[NumberOfBuckets];

            Record record;
            Bucket bucket;

            for (int i = 0; i < NumberOfBuckets; i++)
            {
                bucket = new Bucket(Factor, i);
                for (int j = 0; j < Factor; j++)
                {
                    record = new Record()
                    {
                        Person = new Person()
                        {
                            Id = 0,
                            FullName = null,
                            Adress = null,
                            Age = 0
                        },
                        Status = Status.empty
                    };
                    bucket.Records[j] = record;
                }
                PrimaryZone[i] = bucket;
            }

            for (int i = 0; i < NumberOfBuckets; i++)
            {
                bucket = new Bucket(Factor, i + NumberOfBuckets);
                for (int j = 0; j < Factor; j++)
                {
                    record = new Record()
                    {
                        Person = new Person()
                        {
                            Id = 0,
                            FullName = null,
                            Adress = null,
                            Age = 0
                        },
                        Status = Status.empty
                    };
                    bucket.Records[j] = record;
                }
                OverrunZone[i] = bucket;
            }
        }
    }
}
