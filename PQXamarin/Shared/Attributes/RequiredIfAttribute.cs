using PQXamarin.Interfaces;
using System;

namespace PQXamarin.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredIfAttribute : Attribute, IValidationAttribute
    {

        public RequiredIfAttribute() { }
        public RequiredIfAttribute(string PropertyName,object Value, string Message)
        {
            ErrorMessage = Message;
            this.PropertyName = PropertyName;
            this.Value = Value;
        }
        public int Priority { get; set; }

        public string ErrorMessage { get; set; }
        public string PropertyName { get; set; }
        public object Value { get; set; }
        public bool Validate(object Context, object value)
        {
            if (string.IsNullOrEmpty(PropertyName))
                return true;
            var prop = Context.GetType().GetProperty(PropertyName);
            if (prop == null)
                return true;
            if (prop.GetValue(Context) != Value)
                return true;
            if (value is string)
            {
                var Text = value.ToString();
                return !string.IsNullOrEmpty(Text) && !string.IsNullOrWhiteSpace(Text);
            }
            else
                return value != null;
        }
    }
}
