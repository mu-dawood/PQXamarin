using PQXamarin.Interfaces;
using System;
using System.Text.RegularExpressions;

namespace PQXamarin.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EmailAttribute : Attribute, IValidationAttribute
    {
        public EmailAttribute() { }
        public EmailAttribute(string Message)
        {
            ErrorMessage = Message;
        }
        public string ErrorMessage { get; set; }
        public int Priority { get; set; }

        public bool Validate(object Context, object Text)
        {
           
            if (!(Text is string))
                return false;


            try
            {
                return Regex.IsMatch(Text.ToString(),
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
