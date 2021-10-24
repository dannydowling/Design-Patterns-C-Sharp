using Xamarin.Forms;

namespace XamarinHelpers
{
    public class PictureAsButton : ContentView
    {
        public Image imageIcon { get; set; }
        public Label imageLabel { get; set; }
        public ImageSource Icon

        {
            get { return imageIcon.Source; }
            set { imageIcon.Source = value; }
        }
        public string Label
        {
            get { return imageLabel.Text; }
            set { imageLabel.Text = value; }
        }
        public PictureAsButton()
        {
            imageIcon = new Image();
            imageIcon.Source = "{Binding Icon}";
            imageIcon.HorizontalOptions = LayoutOptions.Center;

            imageLabel = new Label();
            imageLabel.Text = "{Binding Label}";
            imageLabel.TextColor = Color.Black;
            imageLabel.HorizontalOptions = LayoutOptions.Center;

            Content = new StackLayout
            {
                Spacing = 10,
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.White,
                Children = { imageIcon, imageLabel }
            };

        }
    }
}