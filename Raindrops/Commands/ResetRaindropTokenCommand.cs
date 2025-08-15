using Meziantou.Framework.Win32;
using Microsoft.CommandPalette.Extensions.Toolkit;
using Raindrops.Constants;

namespace Raindrops.Commands;

internal sealed partial class ResetRaindropTokenCommand : InvokableCommand
{
    public const string Title = "Reset Raindrop.io API Token";

    public ResetRaindropTokenCommand()
    {
        Icon = new("\uF8B0");
    }

    public override CommandResult Invoke()
    {
        CredentialManager.DeleteCredential(Application.Name);

        CommandResult.ShowToast("Raindrop.io API Token reset successfully. Use 'Raindrops: Set API Token' to configure it again.");
        return CommandResult.GoHome();
    }
}