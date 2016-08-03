using System;
using System.Linq;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;

namespace MagicTimetable
{
    [Activity(Label = "Magic Timetable")]
    public class StageOrderedActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var artistsList = new DocsReader().CreateIdList().Where(d => d.EventName == Intent.GetStringExtra("EventName")).Where(e => e.Day == Convert.ToInt32(Intent.GetStringExtra("Day"))).OrderBy(e => e.Stage).ThenBy(t => t.SetStartTime).ToList();
            var stages = artistsList.Select(t => t.Stage).Distinct();

            var compareDate = DateTime.Parse("07:00:00.000");

            foreach (var artist in artistsList.ToList())
            {
                if (artist.SetStartTime < compareDate)
                {
                    var artist2 = artist;
                    artistsList.Remove(artist);
                    artistsList.Add(artist2);
                }
            }

            var scrollview = new ScrollView(this);
            scrollview.SetBackgroundColor(Color.DarkGray);

            var stageOrderLayout = new LinearLayout(this) { Orientation = Orientation.Vertical };
            stageOrderLayout.SetPadding(10, 10, 10, 10);
            stageOrderLayout.SetBackgroundColor(Color.DarkGray);


            foreach (var stage in stages)
            {
                var stageButton = new Button(this) { Text = stage, TextSize = 24 };
                stageButton.SetBackgroundColor(Color.SteelBlue);
                stageButton.SetTextColor(Color.White);
                stageButton.SetShadowLayer(1, 1, 1, Color.Black);
                stageOrderLayout.AddView(stageButton);


                foreach (var artist in artistsList.Where(a => a.Stage == stage))
                {
                    var artistText = new TextView(this)
                    {
                        Text = artist.Name + " :",
                        TextSize = 21
                    };
                    artistText.SetTextColor(Color.White);

                    var artistTime = new TextView(this)
                    {
                        Text = artist.SetStartTime.ToShortTimeString() + " - " +
                               artist.SetEndTime.ToShortTimeString(),
                        TextSize = 20
                    };
                    artistTime.SetTextColor(Color.Silver);

                    var breakLine = new TextView(this) {Text = "-------------------------------"};
                    breakLine.SetTextColor(Color.Black);

                    stageOrderLayout.AddView(artistText);
                    stageOrderLayout.AddView(artistTime);
                    stageOrderLayout.AddView(breakLine);
                }
            }
            scrollview.AddView(stageOrderLayout);

            SetContentView(scrollview);
        }
    }
}