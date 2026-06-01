//using Android.Database;
using PersonalFinance.Mobile.Helpers;
using PersonalFinance.Mobile.Services;
using PersonalFinance.Shared.DTOs.Budgets;
using PersonalFinance.Shared.DTOs.Reports;
using System.Collections.ObjectModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

using System.Windows.Input;

namespace PersonalFinance.Mobile.ViewModels
{
    public class BudgetsViewModel : BaseViewModel
    {
        private readonly BudgetService  _budgetService;
        private readonly ReportService _reportService;

        //Budgets Collection 
        public ObservableCollection<BudgetCardDto> Budgets { get; set; }
        public ObservableCollection<BudgetUsageDto> BudgetUsages { get; set; }
        public IEnumerable<ISeries> BudgetSeries { get; set; }

        //Commands
        public ICommand  AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ShowOptionsCommand { get; }


        //Total budgets 
        private decimal _totalBudget;
        public decimal TotalBudget
        {
            get => _totalBudget;
            set
            {
                _totalBudget = value;
                OnPropertyChanged();
            }
        }
        //Current Month
        public string CurrentMonth => DateTime.Now.ToString("MMMM yyyy");
        public BudgetsViewModel()
        {
            _budgetService = new BudgetService();
            _reportService = new ReportService();


            Budgets = new ObservableCollection<BudgetCardDto>();
            BudgetUsages = new ObservableCollection<BudgetUsageDto>();

            //Navigate to add page

            AddCommand = new Command(async ()=> await Shell.Current.GoToAsync("budget-form"));


            //Edit
            EditCommand = new Command<BudgetCardDto>(async (budget) => await EditBudget(budget));

            //-------DELETE
            DeleteCommand = new Command<Guid>(async (id) => await DeleteBudget(id));

            //-------SHOW OPTIONS
            ShowOptionsCommand = new Command<BudgetCardDto>(async budget =>
            {
                string action =
                    await Application.Current.MainPage.DisplayActionSheet(
                        budget.CategoryName,
                        "Cancel",
                        null,
                        "Edit",
                        "Delete");

                if (action == "Edit")
                {
                    EditCommand.Execute(budget);
                }
                else if (action == "Delete")
                {
                    DeleteCommand.Execute(budget.Id);
                }
            });
            
        }
        //Load buget;
        public async Task LoadBudgets()
        {
            try
            {
                var budgets = await _budgetService.GetBudgetsAsync();

                var usages = await _reportService.GetBudgetUsageAsync();

                Budgets.Clear();

                foreach (var budget in budgets)
                {
                    var usage =
                        usages.FirstOrDefault(
                            x => x.CategoryName == budget.CategoryName);
                    var card = new BudgetCardDto
                                {
                                    Id = budget.Id,

                                    CategoryName = budget.CategoryName,

                                    Icon =
                    CategoryIconHelper.GetIcon(
                        budget.CategoryName),

                                    Month = budget.Month,

                                    Limit = usage?.Limit
                        ?? budget.MonthlyLimit,

                                    Spent = usage?.Spent ?? 0,

                                    Remaining = usage?.Remaining
                            ?? budget.MonthlyLimit,

                                    Percentage =
                    usage?.DisplayPercentage ?? 0
                                };

                    Budgets.Add(card);

                    

                }

                TotalBudget = Budgets.Sum(x => x.Limit);

                //Build chart series
                BudgetSeries =
                        Budgets.Select(x =>
                           new PieSeries<double>
                           {
                               Values = new[] { (double)x.Limit },

                               Name = x.CategoryName,
                               InnerRadius = 80
                           })
                       .ToArray();

                OnPropertyChanged(nameof(BudgetSeries));

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    ex.Message,
                    "OK");
            }
        }
        //---------LOAD BUDGET USAGE
        private async Task LoadBudgetUsage()
        {
            try
            {
                var usages = await _reportService.GetBudgetUsageAsync();

                BudgetUsages.Clear();

                foreach (var usage in usages)
                {
                    BudgetUsages.Add(usage);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    ex.Message,
                    "OK");
            }
        }

        //Edit buget
        private async Task EditBudget(BudgetCardDto budget)
        {
            TemporaryBudgetData.SelectedBudget =
                new BudgetDto
                {
                    Id = budget.Id,
                    CategoryName = budget.CategoryName,
                    MonthlyLimit = budget.Limit,
                    Month = budget.Month
                };

            await Shell.Current.GoToAsync("budget-form");
        }
        //--------------Delete budget 
        private async Task DeleteBudget(Guid id)
        {
            try
            {
                bool confirm = await Application.Current.MainPage.DisplayAlert("Delete budget?",
                    "Are you sure you want to delete this budget ?",
                    "Delete", "cancel");
                if (!confirm)
                    return;
                await _budgetService.DeleteBudgetAsync(id);

                await LoadBudgets();

            }
            catch(Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert(
                        "Error",
                        ex.Message,
                        "OK");
            }
        }

    }
}
