using Microsoft.Maui.Graphics;

namespace CommunityToolkit.Maui.Markup.Sample.Constants;

static class ColorConstants
{
	[System.Obsolete]
	public static Color NavigationBarBackgroundColor { get; } = Color.FromHex("FF6601");
	public static Color NavigationBarTextColor { get; } = Colors.Black;

	[System.Obsolete]
	public static Color TextCellDetailColor { get; } = Color.FromHex("828282");
	public static Color TextCellTextColor { get; } = Colors.Black;

	[System.Obsolete]
	public static Color BrowserNavigationBarBackgroundColor { get; } = Color.FromHex("FFE6D5");
	[System.Obsolete]
	public static Color BrowserNavigationBarTextColor { get; } = Color.FromHex("3F3F3F");
}