using PQXamarin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PQXamarin.Base
{
    public class ValidationBase : BindableObject
    {
        private List<int> Indexes = new List<int>();
        private Dictionary<string, bool> ModelState = new Dictionary<string, bool>();

        public bool AllValidationTouched
        {
            get
            {
                foreach (var item in this.GetType().GetProperties())
                {
                    var _attributes = item
                   .GetCustomAttributes();

                    if (_attributes != null)
                    {
                        foreach (var attr in _attributes)
                        {
                            if (attr is IValidationAttribute)
                            {
                                if (!ModelState.Keys.Any(a => a == item.Name))
                                    return false;
                            }
                        }
                    }
                }
                return true;
            }
        }
        public bool HasErrors
        {
            get
            {
                return ModelState.Any(a => a.Value == false);
            }
        }
        public int ErrorsCount
        {
            get
            {
                return ModelState.Count(a => a.Value == false);
            }
        }

        public static readonly BindableProperty ShowErrorsProperty =
             BindableProperty.Create("ShowErrors", typeof(bool), typeof(ValidationBase), defaultValue: false);
        public bool ShowErrors
        {
            get { return (bool)GetValue(ShowErrorsProperty); }
            set
            {
                SetValue(ShowErrorsProperty, value);
            }
        }

        public static readonly BindableProperty CurrentIndexProperty =
             BindableProperty.Create("CurrentIndex", typeof(int), typeof(ValidationBase), defaultValue: 0);
        public int CurrentIndex
        {
            get { return (int)GetValue(CurrentIndexProperty); }
            set
            {
                SetValue(CurrentIndexProperty, value);
            }
        }

        internal int GetNextInt()
        {
            if(Indexes.Count<=0)
            {
                Indexes.Add(0);
                return 0;
            }
            else
            {
                var last = Indexes.Last();
                Indexes.Add(last + 1);
                return last + 1;
            }
        }
        

        public void SetError(string property)
        {
            ModelState[property] = false;

        }
        public void RemoveError(string property)
        {
            ModelState[property] = true;
        }
    }
}
