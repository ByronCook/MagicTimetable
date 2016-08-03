using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;

namespace MagicTimetable
{
    [Activity(Label = "TimetableActivity")]
    public class TimetableActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var scrollview = new ScrollView(this);
            scrollview.SetBackgroundColor(Color.DarkGray);

            var initialLayout = new LinearLayout(this) { Orientation = Orientation.Vertical };
            initialLayout.SetPadding(10, 10, 10, 10);
            initialLayout.SetBackgroundColor(Color.DarkGray);

            var day = Intent.GetStringExtra("DayNumber") ?? "No day";

            var dayButton = new Button(this)
            {
                Text = "Day: " + day,
                TextSize = 24,
                Clickable = false
            };
            dayButton.SetBackgroundColor(Color.SteelBlue);
            dayButton.SetTextColor(Color.White);
            dayButton.SetShadowLayer(2, 2, 2, Color.Black);

            var emptyButton = new Button(this);
            emptyButton.SetBackgroundColor(Color.Transparent);
            emptyButton.TextSize = 20;

            var timeButton = new Button(this) { Text = "Time-ordered Table" };
            timeButton.SetBackgroundColor(Color.SteelBlue);
            timeButton.SetTextColor(Color.White);
            timeButton.TextSize = 24;
            timeButton.SetShadowLayer(2, 2, 2, Color.Black);

            var emptyButton2 = new Button(this);
            emptyButton2.SetBackgroundColor(Color.Transparent);
            emptyButton2.TextSize = 20;

            var stageButton = new Button(this) { Text = "Stage-ordered Table" };
            stageButton.SetBackgroundColor(Color.SteelBlue);
            stageButton.SetTextColor(Color.White);
            stageButton.TextSize = 24;
            stageButton.SetShadowLayer(2, 2, 2, Color.Black);

            initialLayout.AddView(dayButton);
            initialLayout.AddView(emptyButton);
            initialLayout.AddView(stageButton);
            initialLayout.AddView(emptyButton2);
            initialLayout.AddView(timeButton);

            timeButton.Click += delegate
            {
                var timeActivity = new Intent(this, typeof(TimeOrderedActivity));
                timeActivity.PutExtra("EventName", Intent.GetStringExtra("EventName"));
                timeActivity.PutExtra("Day", day);
                StartActivity(timeActivity);
            };

            stageButton.Click += delegate
            {
                var timeActivity = new Intent(this, typeof(StageOrderedActivity));
                timeActivity.PutExtra("EventName", Intent.GetStringExtra("EventName"));
                timeActivity.PutExtra("Day", day);
                StartActivity(timeActivity);
            };

            scrollview.AddView(initialLayout);

            SetContentView(scrollview);
        }
    }
}