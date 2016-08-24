using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;

namespace MagicTimetable
{
    [Activity(Label = "Magic Timetable")]
    public class SelectedDayActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var layoutCreator = new LayoutCreation();
            var scrollView = layoutCreator.CreateBaiscScrollView(this);
            var linearLayout = layoutCreator.CreateBasicLinearLayout(this);

            var day = Intent.GetStringExtra("DayNumber") ?? "No day";

            linearLayout.AddView(layoutCreator.CreateSimpleButton(this, Color.White, Color.SteelBlue, "Day: " + day, null,
                false));

            var timeButton = layoutCreator.CreateResourceButton(this, Color.White, "Time-ordered Table");
            var personalTimeTableButton = layoutCreator.CreateResourceButton(this, Color.White, "Personal Timetable");
            var stageButton = layoutCreator.CreateResourceButton(this, Color.White, "Stage-ordered Table");

            linearLayout.AddView(stageButton);
            linearLayout.AddView(personalTimeTableButton);
            linearLayout.AddView(timeButton);


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

            scrollView.AddView(linearLayout);

            SetContentView(scrollView);
        }
    }
}