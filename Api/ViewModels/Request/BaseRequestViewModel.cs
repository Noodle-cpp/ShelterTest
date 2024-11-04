using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels.Request
{
    public abstract class BaseRequestViewModel
    {
        /// <summary>
        /// Тип операции
        /// </summary>
        [Required]
        public string Operation { get; set; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid? Id { get; set; }
    }
}
