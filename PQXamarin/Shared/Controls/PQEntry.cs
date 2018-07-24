using PQXamarin.Attributes;
using PQXamarin.Base;
using PQXamarin.Interfaces;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace PQXamarin.Controls
{
    public class PQEntry : Entry
    {

        #region Validation 
        public event EventHandler<string> OnError;
        public event EventHandler OnRemoveError;
        PropertyInfo propertyInfo;
        bool _touched = false;
        int index = 0;
        public PQEntry()
        {

            TextChanged += PQDefualtEntry_TextChanged;
            Focused += PQEntry_Focused;
            Unfocused += PQEntry_Unfocused;
        }
        private void PQEntry_Unfocused(object sender, FocusEventArgs e)
        {
            Validate();
        }

        private void PQEntry_Focused(object sender, FocusEventArgs e)
        {
            _touched = true;
            var ctx = BindingContext as ValidationBase;
            if (ctx != null)
                ctx.CurrentIndex = index;
        }

        private void PQDefualtEntry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var name = e.PropertyName;
            if (name == nameof(ShowError))
            {
                _touched = true;
                Validate();
            }
        }

        protected override void OnBindingContextChanged()
        {

            base.OnBindingContextChanged();

            var ctx = BindingContext as ValidationBase;
            if (ctx == null)
            {
                propertyInfo = null;
            }

            string condition = "properties";
            var _propertiesFieldInfo = typeof(BindableObject)
                           .GetRuntimeFields()
                           .Where(x => x.IsPublic == false && x.Name.Contains(condition))
                           .FirstOrDefault();
            var _properties = _propertiesFieldInfo
                                 .GetValue(this) as IList;
            if (_properties == null)
            {
                return;
            }

            var fields = _properties[0]
                .GetType().GetRuntimeFields();

            FieldInfo bindingFieldInfo = fields.FirstOrDefault(x => x.Name.Equals("Binding"));
            FieldInfo propertyFieldInfo = fields.FirstOrDefault(x => x.Name.Equals("Property"));
            foreach (var item in _properties)
            {
                Binding binding = bindingFieldInfo.GetValue(item) as Binding;
                BindableProperty property = propertyFieldInfo.GetValue(item) as BindableProperty;
                if (binding != null && property != null && property.PropertyName.Equals("Text"))
                {
                    propertyInfo = BindingContext?.GetType()?
                    .GetRuntimeProperty(binding.Path);
                }

            }
            index = ctx.GetNextInt();
            ctx.PropertyChanged -= Ctx_PropertyChanged;
            ctx.PropertyChanged += Ctx_PropertyChanged;
            SetPlaceHolder();
            PropertyChanged -= PQDefualtEntry_PropertyChanged;
            PropertyChanged += PQDefualtEntry_PropertyChanged;

        }


        private void Ctx_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ValidationBase.ShowErrorsProperty.PropertyName)
                ShowError = (BindingContext as ValidationBase).ShowErrors;
            if (e.PropertyName == ValidationBase.CurrentIndexProperty.PropertyName&&IsFocused==false)
            {
                var ctx = BindingContext as ValidationBase;
                if (ctx != null && ctx.CurrentIndex == index)
                    Focus();
                
            }
        }

        private void SetPlaceHolder()
        {
            if (propertyInfo != null && this.BindingContext != null)
            {
                var _attributes = propertyInfo
                    .GetCustomAttribute<DisplayAttribute>();

                if (_attributes != null)
                {
                    Placeholder = _attributes.Display;
                }
            }
        }


        void Validate()
        {
            if (!_touched)
                return;
            if (propertyInfo != null && this.BindingContext != null)
            {
                var _attributes = propertyInfo
                    .GetCustomAttributes()
                    .Where(w=>w is IValidationAttribute)
                    .Cast<IValidationAttribute>()
                    .OrderBy(o=>o.Priority);

                if (_attributes != null&&_attributes.Count()>0)
                {
                    foreach (var attr in _attributes)
                    {
                        var IAttr = attr as IValidationAttribute;

                        if (!IAttr.Validate(BindingContext, Text))
                        {
                            HasError = true;
                            if (ShowError)
                            {
                                OnError?.Invoke(this, IAttr.ErrorMessage);
                                ErrorMessage = IAttr.ErrorMessage;
                            }
                            if (BindingContext is ValidationBase)
                            {
                                var obj = BindingContext as ValidationBase;
                                if (obj != null && propertyInfo != null)
                                {
                                    obj.SetError(propertyInfo.Name);
                                }
                            }
                            return;
                        }
                    }
                }
            }
            if (HasError)
            {
                OnRemoveError?.Invoke(this, null);
            }
            ErrorMessage = string.Empty;
            HasError = false;
            if (BindingContext is ValidationBase)
            {
                var obj = BindingContext as ValidationBase;
                if (obj != null && propertyInfo != null)
                {
                    obj.RemoveError(propertyInfo.Name);
                }
            }
        }
        private void PQDefualtEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Text.Length > MaxLength)
            {
                Text = Text.Remove(Text.Length - 1);
                return;
            }
            Validate();
        }


        //properties
        public static readonly BindableProperty HasErrorProperty =
            BindableProperty.Create("HasError", typeof(bool), typeof(PQEntry), defaultValue: false);
        public bool HasError
        {
            get { return (bool)GetValue(HasErrorProperty); }
            private set
            {
                SetValue(HasErrorProperty, value);
            }
        }

        public static readonly BindableProperty ShowErrorProperty =
             BindableProperty.Create("ShowError", typeof(bool), typeof(PQEntry), defaultValue: false);
        public bool ShowError
        {
            get { return (bool)GetValue(ShowErrorProperty); }
            set
            {
                SetValue(ShowErrorProperty, value);
            }
        }

        public static readonly BindableProperty ErrorMessageProperty =
            BindableProperty.Create("ErrorMessage", typeof(string), typeof(PQEntry), defaultValue: string.Empty);
        public string ErrorMessage
        {
            get { return (string)GetValue(ErrorMessageProperty); }
            private set
            {
                SetValue(ErrorMessageProperty, value);
            }
        }

        #endregion
    }



}
