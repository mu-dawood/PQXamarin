
using System.ComponentModel;
using PQXamarin.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using PQXamarin.Platform.Droid.Renderer;
using Android.Content;
[assembly: ExportRenderer(typeof(PQEditor), typeof(PQEditorRenderer))]
namespace PQXamarin.Platform.Droid.Renderer
{
    public class PQEditorRenderer : EditorRenderer
    {
        public PQEditorRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            SetPlaceholder();
            SetPlaceholderColor();
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == PQEditor.PlaceholderProperty.PropertyName)
                SetPlaceholder();
            else if (e.PropertyName == PQEditor.PlaceholderColorProperty.PropertyName)
                SetPlaceholderColor();
            
        }

        void SetPlaceholder()
        {
            if (Control == null || Element == null || !(Element is PQEditor))
                return;
            var text= (Element as PQEditor)?.Placeholder;
            Control.Hint = text;
        }
        void SetPlaceholderColor()
        {
            if (Control == null || Element == null || !(Element is PQEditor))
                return;
            Control.SetHintTextColor((Element as PQEditor).PlaceholderColor.ToAndroid());
        }
    }
}