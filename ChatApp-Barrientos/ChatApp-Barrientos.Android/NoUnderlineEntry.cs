using Android.Content;
using ChatApp_Barrientos.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(Entry), typeof(NoUnderlineEntry))]
namespace ChatApp_Barrientos.Droid.Renderers
{
    public class NoUnderlineEntry : EntryRenderer
    {
        public NoUnderlineEntry(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}