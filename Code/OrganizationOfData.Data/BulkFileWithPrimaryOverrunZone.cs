using OrganizationOfData.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationOfData.Data
{
    [Serializable]
    public class BulkFileWithPrimaryOverrunZone : BulkFile
    {
        private BucketPointer[] freeSpaceBuckets;

        public BucketPointer[] FreeSpaceBuckets
        {
            get
            {
                return freeSpaceBuckets;
            }

            set
            {
                freeSpaceBuckets = value;
                NotifyPropertyChanged(nameof(FreeSpaceBuckets));
            }
        }

        public override void FormEmptyBulkFile()
        {
            PrimaryZone = new Bucket[NumberOfBuckets];

            RecordPointer recordPointer;
            BucketPointer bucketPointer;

            for (int i = 0; i < NumberOfBuckets; i++)
            {
                bucketPointer = new BucketPointer(Factor)
                {
                    Synonyms = null,
                    Prev = null,
                    Next = null,
                    BlankRecords = Factor
                };
                for (int j = 0; j < Factor; j++)
                {
                    recordPointer = new RecordPointer()
                    {
                        Person = new Person()
                        {
                            Id = 0,
                            FullName = null,
                            Adress = null,
                            Age = 0
                        },
                        Status = Status.empty,
                        NextRecord = null
                    };
                    bucketPointer.Records[j] = recordPointer;
                }
                PrimaryZone[i] = bucketPointer;
            }
        }
    }
}
