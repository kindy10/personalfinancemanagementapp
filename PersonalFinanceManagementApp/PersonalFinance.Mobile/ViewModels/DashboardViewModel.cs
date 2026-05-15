using PersonalFinance.Mobile.Services;
using PersonalFinance.Shared.DTOs.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PersonalFinance.Mobile.ViewModels
{
    public class DashboardViewModel :BaseViewModel
    {
        //Service for calling backend API
        private readonly ReportService _reportService;

        //Properties that will be displayed in UI

        private decimal _totalIncome { get; set; }    
        public decimal TotalIncome
        {
            get => _totalIncome;
            set
            {
                _totalIncome = value;
                //Notify UI
                OnPropertyChanged();
            }
        }
        private decimal _totalExpense { get; set; }
        public decimal TotalExpense
        {
            get => _totalExpense;
            set
            {
                _totalExpense = value;
                //Notify UI
                OnPropertyChanged();
            }
        }

        private decimal _balance { get; set; }
        public decimal Balance
        {
            get => _balance;

            set
            {
                _balance = value;

                //Notify UI
                OnPropertyChanged();
            }
        }

        //Link to transactions
        public ICommand GoToTransactionsCommand { get; }


        //-------------LINK TO CATEGORIES
        public ICommand GoToCategoriesCommand { get; }

        //---------------Constructor
        public DashboardViewModel() { 
            _reportService = new ReportService();

            //Load dashboard data automatically
            LoadSummary();

            GoToTransactionsCommand = new Command(async () => await Shell.Current.GoToAsync("//transactions"));

            GoToCategoriesCommand = new Command(async () => await Shell.Current.GoToAsync("//categories"));

        }


        public async void LoadSummary()
        {
            try
            {
                //Call backend
                SummaryDto summary = await _reportService.GetSummaryAsync();

                //Fill propreties with API data
                TotalIncome = summary.TotalIncome;

                TotalExpense = summary.TotalExpense;

                Balance = summary.Balance;
            }
            catch(Exception ex)
            {
                //if API FAILS

                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
