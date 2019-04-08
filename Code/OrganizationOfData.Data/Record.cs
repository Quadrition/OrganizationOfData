namespace OrganizationOfData.Data
{
    using OrganizationOfData.Windows;
    using System;
    using System.ComponentModel.DataAnnotations;

    [Serializable]
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

        [Required(ErrorMessage = "Status sloga je obavezan")]
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
