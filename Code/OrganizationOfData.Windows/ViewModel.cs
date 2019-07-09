namespace OrganizationOfData.Windows
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    /// Base class for a ViewModel in the MVVM pattern.
    /// </summary>
    public abstract class ViewModel : ObservableObject
    {
        /*public string this[string columnName]
        {
            get
            {
                return OnValidate(columnName);
            }
        }

        [Obsolete]
        public string Error
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        protected virtual string OnValidate(string propertyName)
        {
            var context = new ValidationContext(this)
            {
                MemberName = propertyName
            };

            var results = new Collection<ValidationResult>();
            bool isValid = Validator.TryValidateObject(this, context, results, true);

            if (!isValid)
            {
                ValidationResult result = results.SingleOrDefault(p => p.MemberNames.Any(memberName => memberName == propertyName));

                return result == null ? null : result.ErrorMessage;
            }

            return null;
        }*/
        //TODO da li treba ovo da se obrise
    }
}
