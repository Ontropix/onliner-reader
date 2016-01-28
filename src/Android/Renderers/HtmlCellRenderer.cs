using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Widget;
using Android.Text;

using Color = Xamarin.Forms.Color;
using View = global::Android.Views.View;
using ViewGroup = global::Android.Views.ViewGroup;
using Context = global::Android.Content.Context;

using Onliner.Android;
using Onliner.Cells;

[assembly: ExportCell(typeof(FeedCell), typeof(HtmlCellRenderer))]
[assembly: ExportCell(typeof(CommentCell), typeof(HtmlCellRenderer))]
namespace Onliner.Android
{
	public class HtmlCellRenderer : ViewCellRenderer
	{
		protected override View GetCellCore (Cell item, View convertView, ViewGroup parent, Context context)
		{

            //TODO: rewrite
			var layout = (ViewGroup)base.GetCellCore (item, convertView, parent, context);
			var  cell = (ViewGroup)((ViewGroup)layout.GetChildAt (0)).GetChildAt(1);


			TextView label = (TextView)((LabelRenderer)(cell.GetChildAt(0))).GetChildAt(0);
			label.SetTextColor(Color.FromHex("738182").ToAndroid());
			label.TextSize = Font.SystemFontOfSize(NamedSize.Medium).ToScaledPixel();
			label.TextFormatted = Html.FromHtml (label.Text);
            
			return layout;
		}
	
	}
}

