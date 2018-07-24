using PQXamarin.Interfaces;
using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace PQXamarin.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RangeLengthAttribute : Attribute,IValidationAttribute
    {
        public RangeLengthAttribute() { }
        public RangeLengthAttribute(int Min, int Max, string Message)
        {
            this.Min = Min;
            this.Max = Max;
            ErrorMessage = Message;
        }
        
        public string ErrorMessage { get; set; }
        public int Priority { get; set; }
        public int Min { get; set; } = 0;
        public int Max { get; set; } = int.MaxValue;
        public bool Validate(object Context, object Value)
        {
            if (Value is string)
            {
                var str= Value.ToString();
                return str.Length >= Min && str.Length <= Max;
            }
            else if (Value is IList)
            {
                var list = Value as IList;
                return list.Count >= Min&&list.Count<=Max;
            }
            else
                return false;
            
        }
    }
}
