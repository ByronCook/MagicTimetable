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

            var layoutCreator = new LayoutCreation();
            var scrollView = layoutCreator.CreateBaiscScrollView(this);
            var linearLayout = layoutCreator.CreateBasicLinearLayout(this);

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
                        linearLayout.AddView(layoutCreator.CreateSimpleButton(this, Color.White, Color.SteelBlue, artistsList.ElementAt(artistId).Stage, null, true));
                    }
                }
                else
                {
                    linearLayout.AddView(layoutCreator.CreateSimpleButton(this, Color.White, Color.SteelBlue, artistsList.ElementAt(artistId).Stage, null, true));
                }

                var artistDetails = artistsList.ElementAt(artistId).Name + ": " +
                                    artistsList.ElementAt(artistId).SetStartTime.ToShortTimeString() + " - " +
                                    artistsList.ElementAt(artistId).SetEndTime.ToShortTimeString();

                linearLayout.AddView(layoutCreator.CreateSimpleButton(this, Color.White, Color.SteelBlue, artistDetails, 21, false));
            }

            scrollView.AddView(linearLayout);

            SetContentView(scrollView);
        }
    }
}