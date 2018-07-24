using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Helpers.Shared.Effects;
using System.Linq;
using PQPlugin.Droid.Renderers;

[assembly: ResolutionGroupName("PQ")]
[assembly: ExportEffect(typeof(PQGradiantEffectRenderer), "PQGradiant")]
namespace PQPlugin.Droid.Renderers
{
    public class PQGradiantEffectRenderer : PlatformEffect
    {
        GradientDrawable gradientDrawable;

        void SetColors()
        {
            var effect = (PQGradiantEffect)Element.Effects.FirstOrDefault(e => e is PQGradiantEffect);
            if (effect == null)
                return;
            var colors = effect.Colors;
            if (colors != null && colors.Length > 0)
            {
                int[] androidcolors = colors.Select(s => s.ToAndroid()).Select(s => s.ToArgb()).ToArray();
                gradientDrawable.SetColors(androidcolors);
            }
        }

        protected override void OnAttached()
        {
            if (gradientDrawable == null)
            {
                gradientDrawable = new GradientDrawable();
                Control.Background = gradientDrawable;
                var effect = (PQGradiantEffect)Element.Effects.FirstOrDefault(e => e is PQGradiantEffect);
                if (effect == null)
                    return;
                SetColors();
                if (!effect.IsRadial)
                    switch (effect.Oriantaion)
                    {
                        case Helpers.Shared.GradiantOriantaion.LeftToRight:
                            gradientDrawable.SetOrientation(GradientDrawable.Orientation.LeftRight);
                            break;
                        case Helpers.Shared.GradiantOriantaion.RightToLeft:
                            gradientDrawable.SetOrientation(GradientDrawable.Orientation.RightLeft);

                            break;
                        case Helpers.Shared.GradiantOriantaion.TopToBottom:
                            gradientDrawable.SetOrientation(GradientDrawable.Orientation.TopBottom);

                            break;
                        case Helpers.Shared.GradiantOriantaion.BottomToTop:
                            gradientDrawable.SetOrientation(GradientDrawable.Orientation.BottomTop);

                            break;
                        default:
                            gradientDrawable.SetOrientation(GradientDrawable.Orientation.LeftRight);

                            break;
                    }
                gradientDrawable.SetGradientRadius(150);
                gradientDrawable.SetShape(ShapeType.Rectangle);

                gradientDrawable.SetCornerRadius(effect.Radius);
                gradientDrawable.SetGradientType(effect.IsRadial ? GradientType.RadialGradient : GradientType.LinearGradient);
                

            }
        }

        protected override void OnDetached()
        {
            gradientDrawable = null;
        }
    }
}