using Xamarin.Forms;

namespace XamarinHelpers
{
    public class ExamplePage : ContentView
    {
        public ExamplePage()
        {
            var Exit = new PictureAsButton();
            Exit.Icon = "MenuExit.png";
            Exit.Label = "Exit";
            var ExittapGestureRecognizer = new TapGestureRecognizer();
            Exit.GestureRecognizers.Add(ExittapGestureRecognizer);
            ExittapGestureRecognizer.Tapped += (s, e) =>
            {
                Environment.Exit(0);
            };

            ZoomContainer zoom = new ZoomContainer();
            Image aboutinfo = new Image();
            aboutinfo.Source = "aboutinfo.png";
            zoom.HeightRequest = 2000;
            zoom.Content = aboutinfo;

            Grid grid = new Grid();

            RowDefinition rowDef1 = new RowDefinition();
            RowDefinition rowDef2 = new RowDefinition();
            RowDefinition rowDef3 = new RowDefinition();

            grid.RowDefinitions.Add(rowDef1);
            grid.RowDefinitions.Add(rowDef2);
            grid.RowDefinitions.Add(rowDef3);


            ColumnDefinition colDef1 = new ColumnDefinition();
            ColumnDefinition colDef2 = new ColumnDefinition();

            grid.ColumnDefinitions.Add(colDef1);
            grid.ColumnDefinitions.Add(colDef2);

            Grid.SetRow(Exit, 0);
            Grid.SetColumn(Exit, 0);
            Grid.SetRow(aboutinfo, 0);
            Grid.SetColumn(aboutinfo, 1);

            grid.Children.Add(Exit);
            grid.Children.Add(aboutinfo);

            var segments = new StackLayout
            {
                Spacing = 0,
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.White,
                Children = { grid, zoom}
            };

            //var view = new RelativeLayout();
            //{
            //    view.Children.Add(segments,
            //    xConstraint: Constraint.Constant(0),
            //    yConstraint: Constraint.Constant(0),
            //    widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
            //    heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));
            //};

            Content = new ScrollView
            {
                BackgroundColor = Color.White,
                Orientation = ScrollOrientation.Both,
                Content = segments
            };
        }
    }
}
           