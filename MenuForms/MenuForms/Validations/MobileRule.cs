using MenuForms.Validations.Interfaces;
using System.Text.RegularExpressions;

namespace MenuForms.Validations
{
    public class MobileRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;
            Regex regex = new Regex("^(\\+91[\\-\\s]?)?[0]?(91)?[789]\\d{9}$");
            Match match = regex.Match(str);

            return match.Success;
        }
    }
}

