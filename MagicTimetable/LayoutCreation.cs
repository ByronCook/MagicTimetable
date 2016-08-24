using System;
using Android.Content;
using Android.Graphics;
using Android.Widget;

namespace MagicTimetable
{
    class LayoutCreation
    {
        public ScrollView CreateBaiscScrollView(Context previousContext)
        {
            var scrollView = new ScrollView(previousContext);
            scrollView.SetBackgroundColor(Color.DarkGray);

            return scrollView;
        }

        public Button CreateResourceButton(Context previousContext, Color textColor, string text)
        {
            var button = new Button(previousContext) { Text = text, TextSize = 24 };
            button.SetBackgroundResource(Resource.Drawable.buttonblank);
            button.SetTextColor(textColor);
            button.SetShadowLayer(2, 2, 2, Color.Black);

            return button;
        }

        public Button CreateSimpleButton(Context previousContext, Color textColor, Color backgroundColor, string text, int? textSize, bool clickable)
        {
            if (textSize == null)
            {
                textSize = 24;
            }
            var actualSize = Convert.ToInt32(textSize);

            var button = new Button(previousContext) { Text = text, TextSize = actualSize };
            button.SetBackgroundColor(backgroundColor);
            button.SetTextColor(textColor);
            button.SetShadowLayer(2, 2, 2, Color.Black);
            button.Clickable = clickable;

            return button; 
        }

        public LinearLayout CreateBasicLinearLayout(Context previousContext)
        {
            var layout = new LinearLayout(previousContext) { Orientation = Orientation.Vertical };
            layout.SetPadding(10, 10, 10, 10);
            layout.SetBackgroundColor(Color.DarkGray);

            return layout;
        }

        public LinearLayout CreateErrorLayout(Context previousContext)
        {
            var errorLayout = new LinearLayout(previousContext) { Orientation = Orientation.Vertical };
            errorLayout.SetPadding(10, 10, 10, 10);
            errorLayout.SetBackgroundColor(Color.DarkGray);

            var errorText = new TextView(previousContext)
            {
                Text = "There are no events planned.",
                TextSize = 24
            };
            errorText.SetTextColor(Color.Red);
            errorLayout.AddView(errorText);

            return errorLayout;
        }
    }
}