using OrganizationOfData.Data;
using OrganizationOfData.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationOfData.DesktopClient.ViewModels
{
    public class BucketPointerControlViewModel : ViewModel
    {
        private RecordControlViewModel[] recordControlViewModels;

        public RecordControlViewModel[] RecordControlViewModels
        {
            get
            {
                return recordControlViewModels;
            }
            set
            {
                recordControlViewModels = value;
                NotifyPropertyChanged();
            }
        }

        private int e;

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

        public BucketPointerControlViewModel(int e, int factor)
        {
            E = e;
            RecordControlViewModels = new RecordControlViewModel[factor];
        }

        public BucketPointerControlViewModel(BucketPointerControlViewModel bucketPointerControlViewModel)
        {
            RecordControlViewModels = new RecordControlViewModel[bucketPointerControlViewModel.RecordControlViewModels.Length];
            E = bucketPointerControlViewModel.E;

            for (int i = 0; i < bucketPointerControlViewModel.RecordControlViewModels.Length; i++)
            {
                RecordControlViewModels[i] = new RecordControlViewModel()
                {
                    Record = new Record(bucketPointerControlViewModel.RecordControlViewModels[i].Record)
                };
            }
        }

        public BucketPointerControlViewModel(BucketPointer bucket) : this(bucket.E, bucket.Records.Length)
        {
            for (int i = 0; i < bucket.Records.Length; i++)
            {
                RecordControlViewModels[i] = new RecordControlViewModel()
                {
                    Record = bucket.Records[i]
                };
            }
        }
    }
}
