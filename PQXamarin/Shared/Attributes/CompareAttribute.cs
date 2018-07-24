using PQXamarin.Interfaces;
using System;
using Xamarin.Forms.Internals;


namespace PQXamarin.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CompareAttribute : Attribute, IValidationAttribute
    {
        public CompareAttribute() { }
        public CompareAttribute(string PropertyName, string Message)
        {
            ErrorMessage = Message;
            this.PropertyName = PropertyName;
        }
        public int Priority { get; set; }
        public string ErrorMessage { get; set; }
        public string PropertyName { get; set; }
        public bool Validate(object Context, object Value)
        {
            if (string.IsNullOrEmpty(PropertyName))
                return true;
            foreach (var prop in Context.GetType().GetProperties())
            {
                if (prop.Name == PropertyName)
                {
                    var val = prop.GetValue(Context);
                    if (Value is string)
                        return val == Value;
                    else if ((val == null && Value != null) || (Value == null && val != null))
                        return false;
                    else
                        return val.GetType().Equals(Value.GetType());
                }
            }
            return true;
        }
    }
    
}
