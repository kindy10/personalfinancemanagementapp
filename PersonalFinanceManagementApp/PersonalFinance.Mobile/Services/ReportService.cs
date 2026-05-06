using PersonalFinance.Shared.DTOs.Common;
using PersonalFinance.Shared.DTOs.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Mobile.Services
{
    public  class ReportService
    {
        //Service responsible for calling report endpoints
        private readonly ApiService _apiService;

        public ReportService()
        {
            _apiService = new ApiService();
        }

        //Get dashboard summary from API

        public async Task<SummaryDto> GetSummaryAsync()
        {
            //Call API
            //GET /api/reports/summary

            var response = await _apiService.GetAsync<ApiResponse<SummaryDto>>("reports/summary");

            //Validate response
            if (response.Data == null)
                throw new Exception("Summary data is null");

            //Return summary data

            return response.Data;
        }
    }
}
