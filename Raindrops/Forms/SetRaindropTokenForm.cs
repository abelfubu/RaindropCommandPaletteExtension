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
            "type": "TextBlock",
            "size": "medium",
            "weight": "bolder",
            "text": "Set Raindrop.io API Token",
            "style": "heading",
            "wrap": true
        },
        {
            "type": "TextBlock",
            "text": "For this extension we need a test access token, for this you need to create an app in [Raindrop.io settings](https://app.raindrop.io/settings/integrations).\n\n
  1. In the For Developers section click on + Create new app.\n
  2. Set the app name, accept the Terms and Guidelines and click Create.\n
  3. Click in the newly created app.\n
  4. In the bottom of the form click on Create test token.\n
  5. Copy the created token.\n",
            "wrap": true
        },
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

    public override CommandResult SubmitForm(string inputs)
    {
        var formInput = JsonNode.Parse(inputs);
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
