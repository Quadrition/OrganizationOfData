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
        protected Person person;
        protected Status status;

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

        /// <summary>
        /// Initializes a new instance of <see cref="Record"/> class and copies data from existing Record
        /// </summary>
        /// <param name="record">Record which data should be copied</param>
        public Record(Record record)
        {
            Person = new Person
            {
                Id = record.Person.Id,
                FullName = record.Person.FullName,
                Adress = record.Person.Adress,
                Age = record.Person.Age
            };
            Status = record.Status;
        }

        /// <summary>
        /// Checks if all entity's properties are valid
        /// </summary>
        /// <returns>True if all properties is valid, otherwise false</returns>
        public virtual bool IsValid()
        {
            return Person.IsValid();
        }
    }
}
