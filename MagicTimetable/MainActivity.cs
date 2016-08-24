using System.Linq;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;

namespace MagicTimetable
{
    [Activity(Label = "Magic Timetable", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var events = new DocsReader().CreateIdList().Select(t => t.EventName).Distinct().OrderBy(e => e);

            var layoutCreator = new LayoutCreation();
            var scrollView = layoutCreator.CreateBaiscScrollView(this);

            if (events.Any())
            {
                var linearLayout = layoutCreator.CreateBasicLinearLayout(this);

                linearLayout.AddView(layoutCreator.CreateSimpleButton(this, Color.White, Color.SteelBlue, "Select an event:", null, true));

                foreach (var eventname in events)
                {
                    var stageButton = layoutCreator.CreateResourceButton(this, Color.Red, eventname);

                    linearLayout.AddView(stageButton);

                    stageButton.Click += delegate
                    {
                        var dayActivity = new Intent(this, typeof (DayActivity));
                        dayActivity.PutExtra("EventName", eventname);
                        StartActivity(dayActivity);
                    };
                }
                scrollView.AddView(linearLayout);
            }
            else
            {
                scrollView.AddView(layoutCreator.CreateErrorLayout(this));
            }

            SetContentView(scrollView);
        }
    }
}

