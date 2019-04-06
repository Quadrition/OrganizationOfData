namespace OrganizationOfData.Data
{
    using OrganizationOfData.Windows;
    using System;

    [Serializable]
    public class BulkFileWithSerialOverrunZone : BulkFile
    {
        public Bucket[] OverrunZone;

        public BulkFileWithSerialOverrunZone(int numberOfBuckets, int factor) : base(numberOfBuckets, factor)
        {
            OverrunZone = new Bucket[numberOfBuckets];
        }

        public override void FormEmptyBulkFile()
        {
            foreach(Bucket bucket in PrimaryZone)
            {
                foreach(Record record in bucket.Records)
                {
                    record.Person.Id = 0;
                    record.Person.FullName = null;
                    record.Person.Adress = null;
                    record.Person.Age = 0;
                    record.Status = Status.empty;
                }
            }
        }
    }
}
