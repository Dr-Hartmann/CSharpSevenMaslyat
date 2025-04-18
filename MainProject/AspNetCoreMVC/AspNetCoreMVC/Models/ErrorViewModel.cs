namespace AspNetCoreMVC.Models
{
    /// <summary>
    /// Модель представления для страницы ошибок.
    /// Содержит информацию об ошибке, которая произошла при обработке запроса.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Уникальный идентификатор запроса, в котором произошла ошибка.
        /// Используется для отслеживания ошибок в логах.
        /// </summary>
        public string RequestId { get; set; } = string.Empty;

        /// <summary>
        /// Вычисляемое свойство, которое определяет, нужно ли отображать RequestId.
        /// Возвращает true, если RequestId не пустой.
        /// Используется для условного отображения ID запроса на странице ошибки.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
