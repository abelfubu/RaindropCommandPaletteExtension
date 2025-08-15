using System;
using Meziantou.Framework.Win32;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using Raindrops.Constants;
using Raindrops.Helpers;
using Raindrops.Pages;
using Raindrops.Services;

namespace Raindrops;

internal sealed partial class RaindropsPage : DynamicListPage, IDisposable
{
    public const string IconUrl = "https://help.raindrop.io/brand/icon_raw.svg";
    public const string PageId = "RaindropsPage";
    public const string PageTitle = "Raindrops";
    public const string PageSubtitle = "Your Raindrop.io Bookmarks";
    private Credential? credential;
    private ListItem[]? items;
    private readonly Debouncer debouncer = new();

    public RaindropsPage()
    {
        Id = PageId;
        Icon = new(IconUrl);
        Title = PageTitle;
    }

    public override IListItem[] GetItems()
    {
        credential = CredentialManager.ReadCredential(Application.Name);

        if (credential?.Password is null)
        {
            return [
                new ListItem(new SetRaindropTokenPage())
                {
                    Title = SetRaindropTokenPage.PageTitle,
                    Subtitle = "Set your Raindrop.io API Token"
                }];
        }

        if (items is not null)
        {
            return items;
        }

        items = RaindropService.GetBookmarksAsync(
            searchTerm: SearchText,
            raindropApiKey: credential.Password,
            cancellationToken: debouncer.GetToken()).GetAwaiter().GetResult();

        if (items?.Length == 0)
        {
            return [new ListItem(new NoOpCommand()) { Title = "No bookmarks found." }];
        }

        return items ?? [];
    }

    public override async void UpdateSearchText(string oldSearch, string newSearch)
    {
        if (oldSearch == newSearch || items is null) return;


        await debouncer.DebounceAsync(async () =>
        {
            IsLoading = true;

            items = await RaindropService.GetBookmarksAsync(
                raindropApiKey: credential?.Password ?? string.Empty,
                searchTerm: newSearch,
                cancellationToken: debouncer.GetToken());

            RaiseItemsChanged(items.Length);
            IsLoading = false;

        }, 300);

    }

    public void Dispose()
    {
        debouncer.Dispose();
    }
}
