using ApexToolsLauncher.Core.Config.GUI;
using ApexToolsLauncher.Core.Libraries;
using ApexToolsLauncher.GUI.Services.Game;
using ApexToolsLauncher.GUI.Services.Mod;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Color = MudBlazor.Color;

namespace ApexToolsLauncher.GUI.Components.Panels;

public partial class GameTabsView : MudComponentBase, IDisposable
{
    [Inject]
    protected IProfileConfigService? ProfileConfigService { get; set; }
    
    [Inject]
    protected IModConfigService? ModConfigService { get; set; }
    
    [Parameter]
    public string GameId { get; set; } = ConstantsLibrary.InvalidString;
    
    [Parameter]
    public string ProfileId { get; set; } = ConstantsLibrary.InvalidString;

    [Parameter]
    public string PanelStyle { get; set; } = "";
    
    protected ProfileConfig ProfileConfig { get; set; } = new();
    
    // badge colour when a non-vanilla profile is selected
    protected Color ProfileBadgeColour => !ConstantsLibrary.IsStringInvalid(ProfileId) ? Color.Secondary : Color.Primary;
    
    // badge colour when the vanilla profile is selected
    protected Color VanillaBadgeColour => ConstantsLibrary.IsStringInvalid(ProfileId) ? Color.Secondary : Color.Primary;
    protected int EnabledModCount => ProfileConfig.ModConfigs.Count;
    protected int EnabledLaunchArgumentCount => ProfileConfig.LaunchArguments.Count;
    protected int EnabledProfileCount { get; set; } = 0;
    
    protected void ReloadData()
    {
        if (ProfileConfigService is null) return;
        if (ModConfigService is null) return;
        
        ProfileConfig = ProfileConfigService.Get(GameId, ProfileId);
        EnabledProfileCount = ProfileConfigService.GetAllFromGame(GameId).Count;
    }
    
    protected override async Task OnParametersSetAsync()
    {
        await Task.Run(ReloadData);
    }
    
    protected async void OnConfigReloaded()
    {
        await Task.Run(ReloadData);
        await InvokeAsync(StateHasChanged);
    }
    
    protected override void OnInitialized()
    {
        ProfileConfigService?.RegisterOnReload(OnConfigReloaded);
        ModConfigService?.RegisterOnReload(OnConfigReloaded);
    }

    public void Dispose()
    {
        ProfileConfigService?.UnregisterOnReload(OnConfigReloaded);
        ModConfigService?.UnregisterOnReload(OnConfigReloaded);
    }
}
