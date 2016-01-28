using Xamarin.Forms;

namespace Onliner.Cells
{
    public class CommentCell : ViewCell
    {

        public CommentCell()
        {
            Label lblTitle = new Label();
            lblTitle.SetBinding(Label.TextProperty, "Text");
            lblTitle.LineBreakMode = LineBreakMode.WordWrap;

            Label lblDate = new Label();
            lblDate.SetBinding(Label.TextProperty, "CreatedAt");

            var childLayout = new StackLayout
            {
                Padding = new Thickness(5),
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Children = { lblTitle, lblDate }
            };
            

            Image image = new Image();
            image.SetBinding(Image.SourceProperty, "AuthorAvatar");
            image.HeightRequest = 50;
            image.WidthRequest = 50;
			image.Aspect = Aspect.AspectFit;

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
