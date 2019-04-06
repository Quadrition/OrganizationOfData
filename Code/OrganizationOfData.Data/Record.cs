namespace OrganizationOfData.Data
{
    using OrganizationOfData.Windows;

    public class Record : Model
    {
        private Person person;
        private Status status;

        public Person Person
        {
            get
            {
                return person;
            }
            set
            {
                person = value;
                NotifyPropertyChanged(nameof(Person));
            }
        }

        public Status Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                NotifyPropertyChanged(nameof(Status));
            }
        }
    }
}
