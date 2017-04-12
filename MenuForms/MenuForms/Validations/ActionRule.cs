using MenuForms.Validations.Interfaces;
using System;

namespace MenuForms.Validations
{
    public class ActionRule<T> : IValidationRule<T>
    {
        private readonly Func<T, bool> _predicate;

        public string ValidationMessage { get; set; }

        public ActionRule(Func<T, bool> predicate, string validationMessage)
        {
            _predicate = predicate;
            ValidationMessage = validationMessage;
        }

        public bool Check(T value)
        {
            return _predicate.Invoke(value);
        }
    }
}
