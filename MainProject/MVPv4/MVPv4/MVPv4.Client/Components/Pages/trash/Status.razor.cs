using Microsoft.AspNetCore.Components;

namespace MVPv4.Client.Pages.Components;

public partial class StatusComponent : ComponentBase
{
    public readonly string[] GetStatuses = Enum.GetNames<StatusEnum>();

    public enum StatusEnum
    {
        Yes,
        No,
        YesNoProbably
    }
}
