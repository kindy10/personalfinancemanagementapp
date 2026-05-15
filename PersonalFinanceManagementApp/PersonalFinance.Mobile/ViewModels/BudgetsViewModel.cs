//using Android.Database;
using PersonalFinance.Mobile.Helpers;
using PersonalFinance.Mobile.Services;
using PersonalFinance.Shared.DTOs.Budgets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PersonalFinance.Mobile.ViewModels
{
    public class BudgetsViewModel : BaseViewModel
    {
        private readonly BudgetService  _budgetService;


        //Budgets Collection 
        public ObservableCollection<BudgetDto> Budgets { get; set; }


        //Commands
        public ICommand  AddCommand { get; }

        public ICommand EditCommand { get; }

        public ICommand DeleteCommand { get; }


        public BudgetsViewModel()
        {
            _budgetService = new BudgetService();


            Budgets = new ObservableCollection<BudgetDto>();

            //Navigate to add page

            AddCommand = new Command(async ()=> await Shell.Current.GoToAsync("budget-form"));


            //Edit
            EditCommand = new Command<BudgetDto>(async (budget) => await EditBudget(budget));

            //-------DELETE
            DeleteCommand = new Command<Guid>(async (id) => await DeleteBudget(id));

        }

        //Load buget;
        public async Task LoadBudgets()
        {
            try
            {
                var budgets = await _budgetService.GetBudgetsAsync();
                Budgets.Clear();

                foreach (var budget in budgets)
                    Budgets.Add(budget);

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

        //Edit buget
        private async Task EditBudget(BudgetDto budget)
        {
            TemporaryBudgetData.SelectedBudget = budget;

            await Shell.Current.GoToAsync("budget-form");
        }
        //Delete budget 
        private async Task DeleteBudget(Guid id)
        {
            try
            {
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
