using System.Text.Json.Nodes;
using Meziantou.Framework.Win32;
using Microsoft.CommandPalette.Extensions.Toolkit;
using Raindrops.Constants;

namespace Raindrops.Forms;

internal sealed partial class SetRaindropTokenForm : FormContent
{
    public SetRaindropTokenForm()
    {
        var token = CredentialManager.ReadCredential(Application.Name);

        TemplateJson = $$"""
{
    "$schema": "http://adaptivecards.io/schemas/adaptive-card.json",
    "type": "AdaptiveCard",
    "version": "1.6",
    "body": [
        {
            "id": "token",
            "type": "Input.Text",
            "label": "Raindrop.io API Token",
            "value": "{{token?.Password ?? string.Empty}}",
            "isRequired": true,
            "errorMessage": "API Token is required.",
            "style": "password"
        }
    ],
    "actions": [
        {
            "type": "Action.Submit",
            "title": "Save",
            "data": {
                "name": "token"
            }
        }
    ]
}
""";
    }

    public override CommandResult SubmitForm(string payload)
    {
        var formInput = JsonNode.Parse(payload);
        if (formInput == null)
        {
            return CommandResult.GoHome();
        }

        var token = formInput["token"]?.GetValue<string>() ?? string.Empty;

        CredentialManager.WriteCredential(
            applicationName: Application.Name,
            userName: "API_TOKEN",
            secret: token,
            persistence: CredentialPersistence.LocalMachine);

        return CommandResult.GoHome();
    }
}
