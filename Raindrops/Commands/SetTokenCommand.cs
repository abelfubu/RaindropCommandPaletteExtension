// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Meziantou.Framework.Win32;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace Raindrops.Commands;

internal sealed class SetTokenCommand : ICommand
{
    public string Name { get; } = "Set API Token";
    public string Title { get; } = "Set Raindrop.io API Token";
    public string Subtitle { get; } = "Sets the API token for Raindrop.io";
    public ICommandItemIcon? Icon { get; }

    private readonly CommandInput _tokenInput = new("Token", "Enter your Raindrop.io API token");

    public SetTokenCommand()
    {
        Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
    }

    public CommandInput[] Inputs => [_tokenInput];

    public Task ExecuteAsync(IExecuteContext context)
    {
        var token = context.GetInputValue(_tokenInput);
        if (string.IsNullOrWhiteSpace(token))
        {
            return Task.CompletedTask;
        }

        CredentialManager.WriteCredential(
            applicationName: "Raindrop.io",
            userName: "API_TOKEN",
            secret: token,
            persistence: CredentialPersistence.LocalMachine);

        return Task.CompletedTask;
    }
}
