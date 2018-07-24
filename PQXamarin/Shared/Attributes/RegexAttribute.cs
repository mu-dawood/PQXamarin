using PQXamarin.Interfaces;
using System;
using System.Text.RegularExpressions;

namespace PQXamarin.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RegexAttribute : Attribute,IValidationAttribute
    {

        public RegexAttribute() { }
        public RegexAttribute(string Pattern, string Message)
        {
            this.Pattern = Pattern;
            ErrorMessage = Message;
        }
        public int Priority { get; set; }
        public string ErrorMessage { get; set; }
        public string Pattern { get; set; }

        public RegexOptions RegexOptions { get; set; }
        public bool Validate(object Context, object Text)
        {
            if (string.IsNullOrEmpty(Pattern))
                return true;
            if (Text is string)
            {
                Match match = Regex.Match(Text.ToString(), Pattern, RegexOptions);
                return match.Success;
            }
            else
                return false;
            
        }
    }
}
