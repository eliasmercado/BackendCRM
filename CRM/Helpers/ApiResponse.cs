using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Helpers
{
    public class ApiResponse<T> where T : new()
    {
        public T Data { get; set; }

        public ApiResponse()
        {
            Data = new();
        }
    }
}
