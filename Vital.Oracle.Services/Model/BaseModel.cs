using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.Oracle.Services.Model
{
    public abstract class BaseModel : INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Checks if the ValidationContext of this model is correct.
        /// If Validation is an expensive action, consider using <example>bool Validate(List&lt;ValidationResult&gt; errors)</example> method instead.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return this.Validate(new List<ValidationResult>());
            }
        }

        public bool Validate(List<ValidationResult> errors)
        {
            Validator.TryValidateObject(this, new ValidationContext(this), errors);

            return errors.Count == 0;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
