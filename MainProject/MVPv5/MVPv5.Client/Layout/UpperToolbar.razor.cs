using Microsoft.AspNetCore.Components;

namespace MVPv5.Client.Layout;

public partial class UpperPanelUpperToolbarBase : ComponentBase
{
    public bool ShowDiv { get; set; } = false;
    public void SwitchShow()
    {
        ShowDiv = !ShowDiv;
    }
}
