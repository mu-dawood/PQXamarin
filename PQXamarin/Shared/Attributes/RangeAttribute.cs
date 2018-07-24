using PQXamarin.Interfaces;
using System;
using System.Text.RegularExpressions;

namespace PQXamarin.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RangeAttribute : Attribute,IValidationAttribute
    {


        public RangeAttribute() { }
        public RangeAttribute(int Min,int Max, string Message)
        {
            this.Min = Min;
            this.Max = Max;
            ErrorMessage = Message;
        }

        public string ErrorMessage { get; set; }
        public int Priority { get; set; }
        public double Min { get; set; } = double.MinValue;
        public double Max { get; set; } = double.MaxValue;
        public bool Validate(object Context, object Text)
        {
            if (!double.TryParse(Text.ToString(), out double number))
                return false;
            return number >= Min&&number<=Max;
        }
    }
}
