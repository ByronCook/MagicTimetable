using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;

namespace MagicTimetable
{
    [Activity(Label = "Magic Timetable1")]
    public class LineupActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var txtHandler = new PersonalTxtHandler();
            var lineUp = new DocsReader().CreateLineupIdList().OrderBy(e => e.Stage);
            var stages = lineUp.Select(t => t.Stage).Distinct();

            var scrollview = new ScrollView(this);
            scrollview.SetBackgroundColor(Color.DarkGray);

            var initialLayout = new LinearLayout(this) { Orientation = Orientation.Vertical };
            initialLayout.SetPadding(10, 10, 10, 10);
            initialLayout.SetBackgroundColor(Color.DarkGray);

            var checkBoxlist = new List<CheckBox>();

            foreach (var stage in stages)
            {
                var stageButton = new Button(this) { Text = stage, TextSize = 24 };
                stageButton.SetBackgroundColor(Color.SteelBlue);
                stageButton.SetTextColor(Color.White);
                stageButton.SetShadowLayer(1, 1, 1, Color.Black);
                initialLayout.AddView(stageButton);

                foreach (var artist in lineUp.Where(e => e.Stage == stage))
                {
                    var artistButton = new CheckBox(this);
                    artistButton.TextSize = 23;
                    artistButton.Text = artist.Name;
                    
                    var savedList = txtHandler.ReadTxtFile();
                    if (savedList.Contains(artistButton.Text))
                    {
                        artistButton.Checked = true;
                    }

                    checkBoxlist.Add(artistButton);
                    initialLayout.AddView(artistButton);
                }
            }

            var saveButton = new Button(this) { Text = "Save", TextSize = 30 };
            saveButton.SetBackgroundColor(Color.SteelBlue);
            saveButton.SetTextColor(Color.White);
            saveButton.SetShadowLayer(4, 4, 4, Color.Black);

            saveButton.Click += delegate
            {
                var selectedArtistsList = new List<string>();

                foreach (var checkBox in checkBoxlist)
                {
                    if (checkBox.Checked)
                    {
                        selectedArtistsList.Add(checkBox.Text);
                    }
                }

                txtHandler.WriteTxtFile(selectedArtistsList);
                new AlertDialog.Builder(this)
                .SetPositiveButton("Ok", (sender, args) =>
                {
                    // User pressed yes
                })
                .SetMessage("Saved succesfully!")
                .SetTitle("Saved")
                .Show(); 
            };

            initialLayout.AddView(saveButton);
            scrollview.AddView(initialLayout);

            SetContentView(scrollview);
        }
    }
}