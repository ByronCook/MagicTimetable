using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace MagicTimetable
{
    [Activity(Label = "Magic Timetable")]
    public class TimeOrderedActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var scrollview = new ScrollView(this);
            scrollview.SetBackgroundColor(Color.DarkGray);

            var timeOrderLayout = new LinearLayout(this) { Orientation = Orientation.Vertical };
            timeOrderLayout.SetPadding(10, 10, 10, 10);
            timeOrderLayout.SetBackgroundColor(Color.DarkGray);

            var artistsList = new DocsReader().CreateIdList().Where(d => d.EventName == Intent.GetStringExtra("EventName")).Where(y => y.Day == Convert.ToInt32(Intent.GetStringExtra("Day"))).OrderBy(a => a.SetStartTime.Hour).ToList();

            var CompareDate = DateTime.Parse("07:00:00.000");

            foreach (var artist in artistsList.ToList())
            {
                if (artist.SetStartTime < CompareDate)
                {
                    var artist2 = artist;
                    artistsList.Remove(artist);
                    artistsList.Add(artist2);
                }
            }

            for (var artistId = 0; artistId < artistsList.Count(); artistId++)
            {
                if (artistId != 0)
                {
                    if (artistsList.ElementAt(artistId).Stage != artistsList.ElementAt(artistId - 1).Stage)
                    {
                        CreateStageButton(artistsList, artistId, timeOrderLayout);
                    }
                }
                else
                {
                    CreateStageButton(artistsList, artistId, timeOrderLayout);
                }
                var artistButton = new Button(this)
                {
                    Text = artistsList.ElementAt(artistId).Name + ": " +
                           artistsList.ElementAt(artistId).SetStartTime.ToShortTimeString() + " - " +
                           artistsList.ElementAt(artistId).SetEndTime.ToShortTimeString(),
                    TextSize = 21,
                    Clickable = false
                };
                artistButton.SetTextColor(Color.White);
                artistButton.SetBackgroundColor(Color.DarkGray);
                artistButton.SetShadowLayer(1, 1, 1, Color.Black);

                timeOrderLayout.AddView(artistButton);
            }

            scrollview.AddView(timeOrderLayout);

            SetContentView(scrollview);
        }
        private void CreateStageButton(IEnumerable<Artist> artistList, int artistId, ViewGroup currentLayout)
        {
            var stageButton = new Button(this) { Text = artistList.ElementAt(artistId).Stage, TextSize = 24 };
            stageButton.SetBackgroundColor(Color.SteelBlue);
            stageButton.SetTextColor(Color.White);
            currentLayout.AddView(stageButton);
        }
    }
}