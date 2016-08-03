using System.Linq;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;

namespace MagicTimetable
{
    [Activity(Label = "Magic Timetable", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var events = new DocsReader().CreateIdList().Select(t => t.EventName).Distinct().OrderBy(e => e);

            var scrollview = new ScrollView(this);
            scrollview.SetBackgroundColor(Color.DarkGray);

            if (events.Any())
            {
                var initialLayout = new LinearLayout(this) {Orientation = Orientation.Vertical};
                initialLayout.SetPadding(10, 10, 10, 10);
                initialLayout.SetBackgroundColor(Color.DarkGray);

                var dayButton = new Button(this) { Text = "Select an event:", TextSize = 24 };
                dayButton.SetBackgroundColor(Color.SteelBlue);
                dayButton.SetTextColor(Color.White);
                dayButton.SetShadowLayer(2, 2, 2, Color.Black);

                initialLayout.AddView(dayButton);

                foreach (var eventname in events)
                {
                    var stageButton = new Button(this) {Text = eventname, TextSize = 24};
                    stageButton.SetBackgroundResource(Resource.Drawable.buttonblank);
                    stageButton.SetTextColor(Color.Red);
                    stageButton.SetShadowLayer(4, 4, 4, Color.Black);

                    var emptyButton = new Button(this);
                    emptyButton.SetBackgroundColor(Color.Transparent);
                    emptyButton.TextSize = 28;

                    initialLayout.AddView(emptyButton);
                    initialLayout.AddView(stageButton);

                    stageButton.Click += delegate
                    {
                        var dayActivity = new Intent(this, typeof (DayActivity));
                        dayActivity.PutExtra("EventName", eventname);
                        StartActivity(dayActivity);
                    };
                }
                scrollview.AddView(initialLayout);
            }
            else
            {
                var errorLayout = new LinearLayout(this) { Orientation = Orientation.Vertical };
                errorLayout.SetPadding(10, 10, 10, 10);
                errorLayout.SetBackgroundColor(Color.DarkGray);

                var errorText = new TextView(this);
                errorText.Text = "There are no events planned.";
                errorText.TextSize = 24;
                errorText.SetTextColor(Color.Red);
                errorLayout.AddView(errorText);
                scrollview.AddView(errorLayout);
            }

            SetContentView(scrollview);
        }
    }
}

