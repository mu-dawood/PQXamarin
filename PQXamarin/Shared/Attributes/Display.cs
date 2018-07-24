using System;
using System.Collections.Generic;
using System.Text;

namespace PQXamarin.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DisplayAttribute : Attribute
    {
        public DisplayAttribute(string name)
        {
            Display = name;
        }
        public DisplayAttribute()
        {
        }
        public string Display { get; set; }
    }
}
