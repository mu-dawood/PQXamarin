using Xamarin.Forms;

namespace PQXamarin.Shared.Effects
{
    public class PQGradiantEffect : RoutingEffect
    {
        public PQGradiantEffect(string effectId) : base("PQ.PQGradiant")
        {
        }
        public PQGradiantEffect() : base("PQ.PQGradiant")
        { 
        }
        public Color StartColor { get; set; } = Color.Green;
        public Color EndColor { get; set; } = Color.Blue;
        public Color[] Colors => new Color[] { StartColor,EndColor};
        public GradiantOriantaion Oriantaion { get; set; } = GradiantOriantaion.LeftToRight;
        public bool IsRadial { get; set; } = false;
        public float Radius { get; set; }
    }

   
}
