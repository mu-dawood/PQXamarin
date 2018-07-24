using Xamarin.Forms;
using PQPlugin.IOs.Renderers;
using Xamarin.Forms.Platform.iOS;
using CoreAnimation;
using Helpers.Shared.Effects;
using System.Linq;
using CoreGraphics;

[assembly: ResolutionGroupName("PQ")]
[assembly: ExportEffect(typeof(PQGradiantEffectRenderer), "PQGradiant")]
namespace PQPlugin.IOs.Renderers
{
    public class PQGradiantEffectRenderer : PlatformEffect
    {
        CAGradientLayer gradientDrawable;

        void SetColors()
        {
            var effect = (PQGradiantEffect)Element.Effects.FirstOrDefault(e => e is PQGradiantEffect);
            if (effect == null)
                return;
            var colors = effect.Colors;
            if (colors != null && colors.Length > 0)
            {
                CGColor[] ioscolors = colors.Select(s => s.ToCGColor()).ToArray();
                gradientDrawable.Colors = ioscolors;
            }
        }

        protected override void OnAttached()
        {
            if (gradientDrawable == null)
            {
                gradientDrawable = new CAGradientLayer();
                Control.Layer.InsertSublayer(gradientDrawable, 0);
                var effect = (PQGradiantEffect)Element.Effects.FirstOrDefault(e => e is PQGradiantEffect);
                if (effect == null)
                    return;
                SetColors();

                //switch (effect.Oriantaion)
                //{
                //    case Helpers.Shared.GradiantOriantaion.LeftToRight:
                //        gradientDrawable.StartPoint = effect.IsRadial? new CGPoint(.5,.5): new CGPoint(0,0);
                //        gradientDrawable.EndPoint = effect.IsRadial? new CGPoint(0,0): new CGPoint(1,0);
                //        break;
                //    case Helpers.Shared.GradiantOriantaion.RightToLeft:
                //        gradientDrawable.EndPoint = effect.IsRadial ? new CGPoint(.5, .5) : new CGPoint(0, 0);
                //        gradientDrawable.StartPoint = effect.IsRadial ? new CGPoint(0, 0) : new CGPoint(1, 0);

                //        break;
                //    case Helpers.Shared.GradiantOriantaion.TopToBottom:
                //        gradientDrawable.Locations = new Foundation.NSNumber[] { 0,0, 1.0 };
                //        gradientDrawable.

                //        break;
                //    case Helpers.Shared.GradiantOriantaion.BottomToTop:
                //        gradientDrawable.Locations = new Foundation.NSNumber[] { 1, 0, 0.0 };

                //        break;
                //    default:
                //        gradientDrawable.SetOrientation(GradientDrawable.Orientation.LeftRight);

                //        break;
                //}
                //gradientDrawable.SetGradientRadius(150);
                //gradientDrawable.SetShape(ShapeType.Rectangle);

                //gradientDrawable.SetCornerRadius(effect.Radius);
                //gradientDrawable.SetGradientType(effect.IsRadial ? GradientType.RadialGradient : GradientType.LinearGradient);


            }
        }

        protected override void OnDetached()
        {
            gradientDrawable = null;
        }
    }
}