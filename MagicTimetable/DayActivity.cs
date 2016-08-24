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

            var uniqueDays = new DocsReader().CreateIdList().Where(d => d.EventName == Intent.GetStringExtra("EventName")).Select(t => t.Day).Distinct().OrderBy(e => e);

            var layoutCreator = new LayoutCreation();
            var scrollView = layoutCreator.CreateBaiscScrollView(this);
            var linearLayout = layoutCreator.CreateBasicLinearLayout(this);
            linearLayout.AddView(layoutCreator.CreateResourceButton(this, Color.White, "Select a day:"));
            var lineupButton = layoutCreator.CreateResourceButton(this, Color.Red, "Full Line-up:");
            
            foreach (var day in uniqueDays)
            {
                var stageButton = layoutCreator.CreateResourceButton(this, Color.Cyan, "Day: " + day);
                linearLayout.AddView(stageButton);

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
            
            linearLayout.AddView(lineupButton);
            scrollView.AddView(linearLayout);
            SetContentView(scrollView);
        }
    }
}