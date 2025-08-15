// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using Raindrops.Commands;

namespace Raindrops;

public partial class RaindropsCommandsProvider : CommandProvider
{
    private readonly ICommandItem[] _commands;

    public RaindropsCommandsProvider()
    {
        DisplayName = "Raindrops";
        Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
        _commands = [
            new CommandItem(new RaindropsPage()) { Title = DisplayName },
            new CommandItem(new SetTokenCommand()),
        ];
    }

    public override ICommandItem[] TopLevelCommands()
    {
        return _commands;
    }

}
