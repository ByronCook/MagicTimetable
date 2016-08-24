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
            var artists = new DocsReader().CreateLineupIdList().OrderBy(e => e.Stage);
            var stages = artists.Select(t => t.Stage).Distinct();

            var layoutCreator = new LayoutCreation();
            var scrollView = layoutCreator.CreateBaiscScrollView(this);
            var linearLayout = layoutCreator.CreateBasicLinearLayout(this);

            var checkBoxlist = new List<CheckBox>();

            foreach (var stage in stages)
            {
                linearLayout.AddView(layoutCreator.CreateSimpleButton(this, Color.White, Color.SteelBlue, stage, null, true));
                
                foreach (var artist in artists.Where(e => e.Stage == stage))
                {
                    var artistButton = new CheckBox(this)
                    {
                        TextSize = 23,
                        Text = artist.Name
                    };

                    var savedList = txtHandler.ReadTxtFile();
                    if (savedList.Contains(artistButton.Text))
                    {
                        artistButton.Checked = true;
                    }

                    checkBoxlist.Add(artistButton);
                    linearLayout.AddView(artistButton);
                }
            }

            var saveButton = layoutCreator.CreateSimpleButton(this, Color.White, Color.SteelBlue, "Save", null, true);

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

            linearLayout.AddView(saveButton);
            scrollView.AddView(linearLayout);

            SetContentView(scrollView);
        }
    }
}