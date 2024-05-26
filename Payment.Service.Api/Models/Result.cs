using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Payment.Service.Api.Models
{
    /// <summary>
    /// Результирующий глобальный класс
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T>
    {
        /// <summary>
        /// Модель данных
        /// </summary>
        [JsonProperty(PropertyName = "Data")]
        public T Data { get; set; }

        /// <summary>
        /// Успех
        /// </summary>
        [JsonProperty(PropertyName = "Success")]
        public bool Success { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        [JsonProperty(PropertyName = "Message")]
        public string Message { get; set; }

        /// <summary>
        /// Ключ сообщения
        /// </summary>
        [JsonProperty(PropertyName = "Key")]
        public string MessageKey { get; set; }
    }
}
