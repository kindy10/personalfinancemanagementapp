using PersonalFinance.Shared.DTOs.Auth;
using PersonalFinance.Shared.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Mobile.Services
{
    public  class AuthService
    {
        private readonly ApiService _apiService;

        public AuthService()
        {
            _apiService = new ApiService();
        }


        //---------Login 
        public async Task<AuthResponseDto> LoginAsync(string email,string password)
        {
            var request = new LoginRequestDto
            {
                Email = email,
                Password = password
            };
            var response = await _apiService.PostAsync<ApiResponse<AuthResponseDto>>("auth/login", request);

            if (response == null)
                throw new Exception("Result  is null");

            if (response.Data == null)
                throw new Exception("Auth data is null");


            if (string.IsNullOrWhiteSpace(response.Data.Token))
                throw new Exception("Token is null");


            //Save JWT token
            await SecureStorage.SetAsync("auth_token",response.Data.Token);

            return response.Data;
        }
    }
}
