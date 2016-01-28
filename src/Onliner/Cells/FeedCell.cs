using Xamarin.Forms;

namespace Onliner.Cells
{
    public class FeedCell : ViewCell
	{
        public FeedCell()
        {

            Label lblTitle = new Label();
            lblTitle.SetBinding(Label.TextProperty, "Title");
            lblTitle.LineBreakMode = LineBreakMode.WordWrap;
			lblTitle.Font = Font.SystemFontOfSize (14);

            Label lblDate = new Label();
            lblDate.SetBinding(Label.TextProperty, "PublishDate");
			lblDate.Font = Font.SystemFontOfSize (12);

            var childLayout = new StackLayout
            {
                Padding = new Thickness(5),
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Children = { lblTitle, lblDate }
            };
				
            Image image = new Image();
            image.SetBinding(Image.SourceProperty, "Image");
            image.HeightRequest = 50;
            image.WidthRequest = 50;
			image.Aspect = Aspect.AspectFill;

            var layout = new StackLayout
            {
                Padding = new Thickness(5),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Children = { image, childLayout }
            };

            View = layout;
        }
    }
}
