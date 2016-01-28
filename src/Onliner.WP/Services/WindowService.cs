using System.Windows;
using Xamarin.Forms;
using Size = System.Windows.Size;

[assembly : Dependency(typeof(Onliner.WP.WindowService))]

namespace Onliner.WP
{
	public class WindowService: Onliner.IWindowService
	{
		public Xamarin.Forms.Size Bounds {
			get
			{
			    Size size = Application.Current.RootVisual.RenderSize;
                return new Xamarin.Forms.Size(size.Width, size.Height);
			}
		}
	}
}

