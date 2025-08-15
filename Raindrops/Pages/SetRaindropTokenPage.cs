using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using Raindrops.Forms;

namespace Raindrops.Pages;

internal sealed partial class SetRaindropTokenPage : ContentPage
{
    public const string PageTitle = "Set Raindrop.io API Token";

    public SetRaindropTokenPage()
    {
        Icon = new("\uF8B0");
    }

    public override IContent[] GetContent()
    {
        return [new SetRaindropTokenForm()];
    }
}