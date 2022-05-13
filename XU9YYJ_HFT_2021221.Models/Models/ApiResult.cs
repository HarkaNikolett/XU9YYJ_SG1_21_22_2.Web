using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XU9YYJ_HFT_2021221.Models.Models
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; }
        public List<string> Messages { get; set; }
        public ApiResult()
        {

        }
        public ApiResult(bool success, List<string> messages = null)
        {
            this.IsSuccess = success;
            Messages = messages;
        }
    }
}
