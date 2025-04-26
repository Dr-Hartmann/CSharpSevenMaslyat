using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.Services
{
    /// <summary>
    /// Сервис аудита для фиксации действий пользователя (имитация).
    /// </summary>
    public class AuditService
    {
        /// <summary>
        /// Асинхронно записывает событие аудита.
        /// </summary>
        public async Task AuditAsync(string action, string user, CancellationToken cancellationToken)
        {
            // Имитация задержки записи аудита
            await Task.Delay(30, cancellationToken);
            Console.WriteLine($"[AUDIT] {DateTime.Now}: Пользователь '{user}' выполнил действие: {action}");
        }
    }
} 