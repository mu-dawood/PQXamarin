using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Helpers.Shared.Effects;
using System.Linq;
using PQPlugin.Droid.Renderers;
using System;

[assembly: ExportEffect(typeof(PQShadowEffectRenderer), "PQShadow")]
namespace PQPlugin.Droid.Renderers
{
    public class PQShadowEffectRenderer : PlatformEffect
    {




        protected override void OnAttached()
        {
            try
            {
                if (Control is Android.Widget.TextView)
                {
                    var control = Control as Android.Widget.TextView;
                    var effect = (PQShadow)Element.Effects.FirstOrDefault(e => e is PQShadow);
                    if (effect != null)
                    {
                        float radius = effect.Radius;
                        float distanceX = effect.DistanceX;
                        float distanceY = effect.DistanceY;
                        Android.Graphics.Color color = effect.Color.ToAndroid();
                        control.SetShadowLayer(radius, distanceX, distanceY, color);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {

        }
    }
}