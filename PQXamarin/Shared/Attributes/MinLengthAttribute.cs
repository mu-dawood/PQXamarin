using PQXamarin.Interfaces;
using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace PQXamarin.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MinLengthAttribute : Attribute, IValidationAttribute
    {
        public MinLengthAttribute() { }
        public MinLengthAttribute(int Min,string Message)
        {
            this.Min = Min;
            ErrorMessage = Message;
        }
        public string ErrorMessage { get; set; }
        public int Priority { get; set; }

        public int Min { get; set; } = 0;
        public bool Validate(object Context, object Value)
        {
            if (Value is string)
                return Value.ToString().Length >= Min;
            else if (Value is IList)
            {
                var list = Value as IList;
                return list.Count >= Min;
            }
            else
                return false;
        }
    }
}
