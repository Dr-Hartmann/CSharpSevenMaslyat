namespace MVPv5.Core.Services;

public class AuditService
{
    public async Task AuditAsync(string action, string user, CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);
        Console.WriteLine($"[AUDIT] {DateTime.Now}: Пользователь '{user}' выполнил действие: {action}");
    }
}
