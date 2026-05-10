using PersonalFinance.Shared.DTOs.Common;
using PersonalFinance.Shared.DTOs.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Mobile.Services
{
   public  class TransactionService
    {
        //Call backend API
        private readonly ApiService _apiService;

        public TransactionService()
        {
            _apiService = new ApiService();
        }

        //--------------------------------Get all transactions
        public async Task<List<TransactionDto>> GetTransactionAsync()
        {
            //Call backend enpoint:
            //Get /api/transactions

            var response = await _apiService
                .GetAsync<ApiResponse<List<TransactionDto>>>("transactions");

            //validate response
            if(response.Data == null )
                return new List<TransactionDto>();

            return response.Data;
        }

        //---------------------------------Create new Transaction
        public async Task CreateTransactionAsync(CreateTransactionRequestDto request)
        {
            await _apiService.PostAsync<object>("transactions", request);
        }


        //--------------------Update
        public async Task UpdateTransactionAsync(Guid id, UpdateTransactionRequestDto request)
        {

            //Send PUT request to backend

            await _apiService.PutAsync($"transactions/{id}", request);
        }

        //----------------Delete transaction
        public async Task DeleteTransactionAsync(Guid id)
        {
            await _apiService.DeleteAsync($"transactions/{id}");
        }
    }
}
