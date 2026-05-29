using PersonalFinance.Mobile.Services;
using PersonalFinance.Shared.DTOs.Categories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PersonalFinance.Shared.DTOs.Enums;

namespace PersonalFinance.Mobile.ViewModels
{
    public  class CategoryFormViewModel :BaseViewModel
    {
        private readonly CategoryService _categoryService;

        //Used for update mode
        private Guid _categoryId;

        //Determines create or edit mode
        public bool IsEditMode { get; set; }

        //Page title
        private string _pageTitle = "Add Category";

        public string PageTitle
        {
            get => _pageTitle; 
            set
            {
                _pageTitle = value;
                OnPropertyChanged();
            }
        }

        //Category Name
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        //selected Type 
        private CategoryType _selectedType;
        public CategoryType SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                OnPropertyChanged();
            }
        }
        private bool _isIncome;
        public bool IsIncome
        {
            get => _isIncome;
            set
            {
                _isIncome = value;

                if (value)
                    SelectedType = CategoryType.Income;

                OnPropertyChanged();
            }
        }
        private bool _isExpense;
        public bool IsExpense
        {
            get => _isExpense;
            set
            {
                _isExpense = value;

                if (value)
                    SelectedType = CategoryType.Expense;

                OnPropertyChanged();
            }
        }

        //Types for picker
        public ObservableCollection<string> Types { get; set; }

        //-----Save command
        public ICommand  SaveCommand { get; }

        public CategoryFormViewModel()
        {
            _categoryService = new CategoryService();

            //Available category types
            Types = new ObservableCollection<string>
            {
                "Income",
                "Expense"
            };

            SaveCommand = new Command(async () => await Save());
        }

        //Load category for editing
        public void LoadCategory(CategoryDto category)
        {
            IsEditMode = true;
            _categoryId = category.Id;
            Name = category.Name;

            SelectedType =Enum.Parse<CategoryType>(category.Type);

            PageTitle = "Edit Category";

            if (SelectedType == CategoryType.Income)
            {
                IsIncome = true;
            }
            else
            {
                IsExpense = true;
            }
        }

        //-----------SAVE CATEGORY
        public async Task Save()
        {
            try
            {
                ////Validation
                //if (string.IsNullOrWhiteSpace(Name))
                //{
                //    await Application.Current.MainPage
                //    .DisplayAlert(
                //        "Validation",
                //        "Name is required",
                //        "OK");

                //    return;
                //}
                //if (string.IsNullOrWhiteSpace(SelectedType))
                //{
                //    await Application.Current.MainPage
                //        .DisplayAlert(
                //            "Validation",
                //            "Please select a type",
                //            "OK");

                //    return;
                //}
                //if (!Enum.TryParse<CategoryType>( SelectedType,true,out var categoryType)){
                //    await Application.Current.MainPage
                //        .DisplayAlert(
                //            "Validation",
                //            "Invalid category type",
                //            "OK");

                //    return;
                //}

                //-------CREATE MODE
                if (!IsEditMode)
                {
                    var request = new CreateCategoryRequestDto
                    {
                        Name = Name,
                        Type = SelectedType
                    };
                    await _categoryService.CreateCategoryAsync(request);
                }
                //EDIT MODE
                else
                {
                    var request = new UpdateCategoryRequestDto
                    {
                        Name = Name,
                        Type =  SelectedType
                    };
                    await _categoryService.UpdateCategoryAsync(_categoryId, request);
                }
                //Success message
                // Success message
                await Application.Current.MainPage
                    .DisplayAlert(
                        "Success",
                        "Category saved",
                        "OK");

                //Navigate back
                await Shell.Current.GoToAsync("..");

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

    }
}
