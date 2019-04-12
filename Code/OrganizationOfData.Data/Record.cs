namespace OrganizationOfData.Data
{
    using OrganizationOfData.Windows;
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents an record with desired model within it and its status
    /// </summary>
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

        /// <summary>
        /// Initializes a new instance of <see cref="Record"/> class
        /// </summary>
        public Record()
        {

        }

        // <summary>
        /// Checks if all entity's properties are valid
        /// </summary>
        /// <returns>True if all properties is valid, otherwise false</returns>
        public virtual bool IsValid()
        {
            return Person.IsValid();
        }
    }
}
