using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Shared.DTOs.Common
{
    public class ApiResponse<T>
    {
        public bool Succes { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    

        public static ApiResponse<T> SuccessResponse(T data, string message = null)
        {
                return new ApiResponse<T>
                {
                    Succes = true,
                    Message = message,
                    Data = data
                };
        }

        public static ApiResponse<T> FailResponse(string message)
        {
            return new ApiResponse<T>
            {
                Succes = false,
                Message = message,
                Data = default

            };
        }
    }
}
