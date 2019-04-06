namespace OrganizationOfData.Data
{
    using OrganizationOfData.Windows;

    public abstract class BulkFile : Model
    {
        private Bucket[] primaryZone;
        private int factor;

        public Bucket[] PrimaryZone
        {
            get
            {
                return primaryZone;
            }
            set
            {
                primaryZone = value;
                NotifyPropertyChanged(nameof(PrimaryZone));
            }
        }

        public BulkFile(int numberOfBuckets, int factor)
        {
            PrimaryZone = new Bucket[numberOfBuckets];
            this.factor = factor;
        }

        public abstract void FormEmptyBulkFile();
    }
}
