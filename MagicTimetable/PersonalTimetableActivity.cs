using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Graphics;
using Android.OS;

namespace MagicTimetable
{
    [Activity(Label = "PersonalTimetableActivity")]
    public class PersonalTimetableActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var layoutCreator = new LayoutCreation();
            var scrollView = layoutCreator.CreateBaiscScrollView(this);
            var linearLayout = layoutCreator.CreateBasicLinearLayout(this);
            
            var artistsList = new DocsReader().CreateIdList().Where(d => d.EventName == Intent.GetStringExtra("EventName")).Where(y => y.Day == Convert.ToInt32(Intent.GetStringExtra("Day"))).OrderBy(a => a.SetStartTime.Hour).ToList();
            var txtHandler = new PersonalTxtHandler();
            var selectedArtistList = txtHandler.ReadTxtFile();

            if (selectedArtistList.Count != 0)
            {
                var actualList = new List<Artist>();

                foreach (var artist in selectedArtistList)
                {
                    var list = artistsList.Where(t => t.Name == artist);

                    foreach (var item in list)
                    {
                        actualList.Add(item);
                    }
                }

                var artistlist2 = actualList.OrderBy(t => t.SetStartTime).ToList();

                var compareDate = DateTime.Parse("07:00:00.000");

                foreach (var artist in artistlist2.ToList())
                {
                    if (artist.SetStartTime < compareDate)
                    {
                        var artist2 = artist;
                        artistlist2.Remove(artist);
                        artistlist2.Add(artist2);
                    }
                }

                for (var artistId = 0; artistId < artistlist2.Count(); artistId++)
                {
                    if (artistId != 0)
                    {
                        if (artistlist2.ElementAt(artistId).Stage != artistlist2.ElementAt(artistId - 1).Stage)
                        {
                            linearLayout.AddView(layoutCreator.CreateSimpleButton(this, Color.White, Color.SteelBlue, artistlist2.ElementAt(artistId).Stage, null, true));
                        }
                    }
                    //Only creates first artists as it needs a first to be able to compare stages above ^
                    else
                    {
                        linearLayout.AddView(layoutCreator.CreateSimpleButton(this, Color.White, Color.SteelBlue, artistlist2.ElementAt(artistId).Stage, null, true));
                    }
                    var artistsDetails = artistlist2.ElementAt(artistId).Name + ": " +
                                         artistlist2.ElementAt(artistId).SetStartTime.ToShortTimeString() + " - " +
                                         artistlist2.ElementAt(artistId).SetEndTime.ToShortTimeString();

                    linearLayout.AddView(layoutCreator.CreateSimpleButton(this, Color.White, Color.DarkGray, artistsDetails, 19, false));
                }

                scrollView.AddView(linearLayout);
            }

            SetContentView(scrollView);
        }
    }
}