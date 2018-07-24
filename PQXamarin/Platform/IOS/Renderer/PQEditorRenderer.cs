
using System.ComponentModel;
using PQXamarin.Controls;
using Xamarin.Forms;
using PQXamarin.Platform.IOS.Renderer;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Foundation;

[assembly: ExportRenderer(typeof(PQEditor), typeof(PQEditorRenderer))]
namespace PQXamarin.Platform.IOS.Renderer
{
    public class PQEditorRenderer : EditorRenderer
    {

        private UILabel _placeholderLabel { get; set; }
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (_placeholderLabel == null)
                createLabel();

            Element.Focused += Element_Focused;
            Element.Unfocused += Element_Unfocused;
            SetPlaceholder();
            SetPlaceholderColor();
        }

        private void Element_Unfocused(object sender, FocusEventArgs e)
        {
            var elm = Element as PQEditor;
            if (elm.Text == null || elm.Text.Length <= 0)
                _placeholderLabel.Hidden = false;
            else
                _placeholderLabel.Hidden = true;
        }

        private void Element_Focused(object sender, FocusEventArgs e)
        {
            _placeholderLabel.Hidden = true;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == PQEditor.PlaceholderProperty.PropertyName)
                SetPlaceholder();
            else if (e.PropertyName == PQEditor.PlaceholderColorProperty.PropertyName)
                SetPlaceholderColor();
        }

        void createLabel()
        {
            _placeholderLabel = new UILabel
            {
                BackgroundColor = UIColor.Clear,

            };
            Control.AddSubview(_placeholderLabel);
            var edgeInsets = Control.TextContainerInset;
            var lineFragmentPadding = Control.TextContainer.LineFragmentPadding;
            var vConstraints = NSLayoutConstraint.FromVisualFormat(
            "V:|-" + edgeInsets.Top + "-[_placeholderLabel]-" + edgeInsets.Bottom + "-|", 0, new NSDictionary(),
            NSDictionary.FromObjectsAndKeys(
                new NSObject[] { _placeholderLabel }, new NSObject[] { new NSString("_placeholderLabel") }));
            var hConstraints = NSLayoutConstraint.FromVisualFormat(
                "H:|-" + lineFragmentPadding + "-[_placeholderLabel]-" + lineFragmentPadding + "-|",
                0, new NSDictionary(),
                NSDictionary.FromObjectsAndKeys(
                    new NSObject[] { _placeholderLabel }, new NSObject[] { new NSString("_placeholderLabel") })
            );

            _placeholderLabel.TranslatesAutoresizingMaskIntoConstraints = false;

            Control.AddConstraints(hConstraints);
            Control.AddConstraints(vConstraints);

        }

        void SetPlaceholder()
        {
            if (Control == null || Element == null || !(Element is PQEditor))
                return;
            if (_placeholderLabel == null)
                createLabel();
            var text = (Element as PQEditor)?.Placeholder;
            _placeholderLabel.Text = text;
        }
        void SetPlaceholderColor()
        {
            if (Control == null || Element == null || !(Element is PQEditor))
                return;
            if (_placeholderLabel == null)
                createLabel();
            _placeholderLabel.TextColor = (Element as PQEditor).PlaceholderColor.ToUIColor();
        }
    }
}