using PersonalFinance.Mobile.Services;
using PersonalFinance.Shared.DTOs.Reports;
using System.Collections.ObjectModel;
using System.Windows.Input;
using LiveChartsCore;

using LiveChartsCore.SkiaSharpView;


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


        //----------------LINK TO BUDGET
        public ICommand GoToBudgetCommand { get; }

        //---------------Link to Profile
        public ICommand GoToProfileCommand { get; }

        //Budget usage
        public ObservableCollection<BudgetUsageDto> BudgetUsages { get; set; }


        // Expense serie  (Live Chart)
        private IEnumerable<ISeries> _expenseSeries;

        public IEnumerable<ISeries> ExpenseSeries
        {
            get => _expenseSeries;

            set
            {
                _expenseSeries = value;

                OnPropertyChanged();
            }
        }


        //Trend Series
        private IEnumerable<ISeries> _trendSeries;

        public IEnumerable<ISeries> TrendSeries
        {
            get => _trendSeries;

            set
            {
                _trendSeries = value;

                OnPropertyChanged();
            }
        }

        private string[] _months;

        public string[] Months
        {
            get => _months;

            set
            {
                _months = value;

                OnPropertyChanged();
            }
        }

        //---------XAxes property
        private Axis[] _xAxes;

        public Axis[] XAxes
        {
            get => _xAxes;

            set
            {
                _xAxes = value;

                OnPropertyChanged();
            }
        }

        //---------------Constructor
        public DashboardViewModel() { 
            _reportService = new ReportService();

            //Load dashboard data automatically
            LoadSummary();
            _ = LoadBudgetUsage();
            _ = LoadExpenseChart();
            _ = LoadTrendChart();

            GoToTransactionsCommand = new Command(async () => await Shell.Current.GoToAsync("//transactions"));

            GoToCategoriesCommand = new Command(async () => await Shell.Current.GoToAsync("//categories"));

            GoToBudgetCommand = new Command(async () => await Shell.Current.GoToAsync("//budgets"));

            GoToProfileCommand =new Command(async () => await Shell.Current.GoToAsync( "//profile"));

            BudgetUsages = new ObservableCollection<BudgetUsageDto>();

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
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert(
                        "FULL ERROR",
                        ex.ToString(),
                        "OK");
            }
        }


        //Load Budget
        private async Task LoadBudgetUsage()
        {
            try
            {
                var usages =await _reportService.GetBudgetUsageAsync();

                BudgetUsages.Clear();

                foreach (var usage in usages)
                {
                    BudgetUsages.Add(usage);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert(
                        "Error",
                        ex.Message,
                        "OK");
            }
        }

        //Lod  Expense Chart 
        private async Task LoadExpenseChart()
        {
            try
            {
                var data =
                    await _reportService
                        .GetExpenseCategoriesAsync();

                ExpenseSeries =
                    data.Select(x =>
                        new PieSeries<decimal>
                        {
                            Values =
                                new[] { x.TotalAmount },

                            Name =
                                x.CategoryName,

                            DataLabelsSize = 14,

                            DataLabelsPosition =
                                LiveChartsCore.Measure
                                    .PolarLabelsPosition
                                    .Middle,

                            DataLabelsFormatter =
                                            point =>
                                                $"{point.Context.Series.Name}\n{point.Coordinate.PrimaryValue:C}"
                        })
                    .ToArray();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert(
                        "Chart Error",
                        ex.ToString(),
                        "OK");
            }
        }

        //Load trend
        private async Task LoadTrendChart()
        {
            try
            {
                var trends = await _reportService.GetMonthlyTrendsAsync();

                Months =
                    trends
                        .Select(x => x.Month)
                        .ToArray();

                XAxes =
                [
                    new Axis
                    {
                        Labels = Months
                    }
                ];

                TrendSeries =
                [
                    new LineSeries<decimal>
            {
                Name = "Income",

                Values =
                    trends
                        .Select(x => x.Income)
                        .ToArray()
            },

            new LineSeries<decimal>
            {
                Name = "Expense",

                Values =
                    trends
                        .Select(x => x.Expense)
                        .ToArray()
            }
                ];
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert(
                        "Trend Error",
                        ex.ToString(),
                        "OK");
            }
        }
    }
}
