using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;

namespace MagicTimetable
{
    [Activity(Label = "Magic Timetable")]
    public class SelectedDayActivity : Activity
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

            var timeButton = new Button(this) { Text = "Time-ordered Table" };
            timeButton.SetBackgroundResource(Resource.Drawable.buttonblank);
            timeButton.SetTextColor(Color.White);
            timeButton.TextSize = 24;
            timeButton.SetShadowLayer(2, 2, 2, Color.Black);

            var personalTimeTableButton = new Button(this) { Text = "Personal Timetable" };
            personalTimeTableButton.SetBackgroundResource(Resource.Drawable.buttonblank);
            personalTimeTableButton.SetTextColor(Color.White);
            personalTimeTableButton.TextSize = 24;
            personalTimeTableButton.SetShadowLayer(2, 2, 2, Color.Black);

            var stageButton = new Button(this) { Text = "Stage-ordered Table" };
            stageButton.SetBackgroundResource(Resource.Drawable.buttonblank);
            stageButton.SetTextColor(Color.White);
            stageButton.TextSize = 24;
            stageButton.SetShadowLayer(2, 2, 2, Color.Black);

            initialLayout.AddView(dayButton);
            initialLayout.AddView(stageButton);
            initialLayout.AddView(personalTimeTableButton);
            initialLayout.AddView(timeButton);


            timeButton.Click += delegate
            {
                var timeActivity = new Intent(this, typeof(TimeOrderedActivity));
                timeActivity.PutExtra("EventName", Intent.GetStringExtra("EventName"));
                timeActivity.PutExtra("Day", day);
                StartActivity(timeActivity);
            };

            personalTimeTableButton.Click += delegate
            {
                var timeActivity = new Intent(this, typeof(PersonalTimetableActivity));
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