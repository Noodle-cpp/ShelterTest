using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelterServiceClient.ViewModels.Requests
{
    public abstract class BaseRequestViewModel
    {
        /// <summary>
        /// Тип операции
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid? Id { get; set; }
    }
}
