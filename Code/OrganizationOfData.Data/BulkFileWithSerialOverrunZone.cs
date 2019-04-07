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
            OverrunZone = new Bucket[NumberOfBuckets];
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
