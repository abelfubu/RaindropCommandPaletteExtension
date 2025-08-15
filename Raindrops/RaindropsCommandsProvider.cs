using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using Raindrops.Commands;
using Raindrops.Pages;

namespace Raindrops;

public partial class RaindropsCommandsProvider : CommandProvider
{
    private readonly ICommandItem[] _commands;

    public RaindropsCommandsProvider()
    {
        DisplayName = RaindropsPage.PageTitle; ;
        Icon = new(RaindropsPage.IconUrl);
        _commands = [
            new CommandItem(new RaindropsPage()) { Title = DisplayName, Subtitle = RaindropsPage.PageSubtitle },
            new CommandItem(new SetRaindropTokenPage()) { Title = SetRaindropTokenPage.PageTitle },
            new CommandItem(new ResetRaindropTokenCommand()) { Title = ResetRaindropTokenCommand.Title },
        ];
    }

    public override ICommandItem[] TopLevelCommands()
    {
        return _commands;
    }

}