using System.Linq;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;

namespace MagicTimetable
{
    [Activity(Label = "Magic Timetable")]
    public class DayActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var days = new DocsReader().CreateIdList().Where(d => d.EventName == Intent.GetStringExtra("EventName")).Select(t => t.Day).Distinct().OrderBy(e => e);

            var scrollview = new ScrollView(this);
            scrollview.SetBackgroundColor(Color.DarkGray);

            var initialLayout = new LinearLayout(this) { Orientation = Orientation.Vertical };
            initialLayout.SetPadding(10, 10, 10, 10);
            initialLayout.SetBackgroundColor(Color.DarkGray);

            var dayButton = new Button(this) { Text = "Select a day:", TextSize = 24 };
            dayButton.SetBackgroundColor(Color.SteelBlue);
            dayButton.SetTextColor(Color.White);
            dayButton.SetShadowLayer(2, 2, 2, Color.Black);
            initialLayout.AddView(dayButton);

            var lineupButton = new Button(this) { Text = "Full Line-up:", TextSize = 24 };
            lineupButton.SetBackgroundResource(Resource.Drawable.buttonblank);
            lineupButton.SetTextColor(Color.Red);
            lineupButton.SetShadowLayer(4, 4, 4, Color.Black);

            foreach (var day in days)
            {
                var stageButton = new Button(this) { Text = "Day: " + day, TextSize = 24 };
                stageButton.SetBackgroundResource(Resource.Drawable.buttonblank);
                stageButton.SetTextColor(Color.Cyan);
                stageButton.SetShadowLayer(2, 2, 2, Color.Black);

                var emptyButton = new Button(this);
                emptyButton.SetBackgroundColor(Color.Transparent);
                emptyButton.TextSize = 24;

                initialLayout.AddView(emptyButton);
                initialLayout.AddView(stageButton);

                stageButton.Click += delegate
                {
                    var selectedDayActivity = new Intent(this, typeof(SelectedDayActivity));
                    selectedDayActivity.PutExtra("EventName", Intent.GetStringExtra("EventName"));
                    selectedDayActivity.PutExtra("DayNumber", day.ToString());
                    StartActivity(selectedDayActivity);
                };
            }

            lineupButton.Click += delegate
            {
                var lineupActivity = new Intent(this, typeof(LineupActivity));
                lineupActivity.PutExtra("EventName", Intent.GetStringExtra("EventName"));
                StartActivity(lineupActivity);
            };


            initialLayout.AddView(lineupButton);
            scrollview.AddView(initialLayout);

            SetContentView(scrollview);
        }
    }
}