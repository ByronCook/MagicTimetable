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
    [Activity(Label = "PersonalTimetableActivity")]
    public class PersonalTimetableActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var scrollview = new ScrollView(this);
            scrollview.SetBackgroundColor(Color.DarkGray);

            var personalTableLayout = new LinearLayout(this) { Orientation = Orientation.Vertical };
            personalTableLayout.SetPadding(10, 10, 10, 10);
            personalTableLayout.SetBackgroundColor(Color.DarkGray);

            var artistsList = new DocsReader().CreateIdList().Where(d => d.EventName == Intent.GetStringExtra("EventName")).Where(y => y.Day == Convert.ToInt32(Intent.GetStringExtra("Day"))).OrderBy(a => a.SetStartTime.Hour).ToList();
            var txtHandler = new PersonalTxtHandler();
            var selectedArtistList = txtHandler.ReadTxtFile();

            if (selectedArtistList.Count != 0)
            {
                var actualList = new List<Artist>();

                foreach (var artist in selectedArtistList)
                {
                    var list = artistsList.Where(t => t.Name == artist);

                    foreach (var listt in list)
                    {
                        actualList.Add(listt);
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
                            CreateStageButton(artistlist2, artistId, personalTableLayout);
                        }
                    }
                    else
                    {
                        CreateStageButton(artistlist2, artistId, personalTableLayout);
                    }
                    var artistButton = new Button(this)
                    {
                        Text = artistlist2.ElementAt(artistId).Name + ": " +
                               artistlist2.ElementAt(artistId).SetStartTime.ToShortTimeString() + " - " +
                               artistlist2.ElementAt(artistId).SetEndTime.ToShortTimeString(),
                        TextSize = 19,
                        Clickable = false
                    };
                    artistButton.SetTextColor(Color.White);
                    artistButton.SetBackgroundColor(Color.DarkGray);
                    artistButton.SetShadowLayer(1, 1, 1, Color.Black);

                    personalTableLayout.AddView(artistButton);
                }

                scrollview.AddView(personalTableLayout);
            }
            else
            {
                var errorLayout = new LinearLayout(this) { Orientation = Orientation.Vertical };
                errorLayout.SetPadding(10, 10, 10, 10);
                errorLayout.SetBackgroundColor(Color.DarkGray);

                var errorText = new TextView(this)
                {
                    Text = "There are no events planned.",
                    TextSize = 24
                };
                errorText.SetTextColor(Color.Red);
                errorLayout.AddView(errorText);
                scrollview.AddView(errorLayout);
            }

            

            SetContentView(scrollview);
        }

        private void CreateStageButton(IEnumerable<Artist> artistList, int artistId, ViewGroup currentLayout)
        {
            var stageButton = new Button(this) { Text = artistList.ElementAt(artistId).Stage, TextSize = 23 };
            stageButton.SetBackgroundColor(Color.SteelBlue);
            stageButton.SetTextColor(Color.White);
            currentLayout.AddView(stageButton);
        }
    }
}