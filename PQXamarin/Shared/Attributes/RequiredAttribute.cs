using PQXamarin.Interfaces;
using System;
using System.Text.RegularExpressions;

namespace PQXamarin.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredAttribute : Attribute, IValidationAttribute
    {
        public RequiredAttribute(string Message)
        {
            ErrorMessage = Message;
        }
        public string ErrorMessage { get; set; }
        public int Priority { get; set; }

        public bool Validate(object Context, object Value)
        {
            if (Value is string)
            {
                var Text = Value.ToString();
                return !string.IsNullOrEmpty(Text) && !string.IsNullOrWhiteSpace(Text);
            }
            else
                return Value != null;
        }
    }
}
