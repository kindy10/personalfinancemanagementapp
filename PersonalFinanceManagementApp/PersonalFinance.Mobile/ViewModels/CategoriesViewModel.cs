using Microsoft.EntityFrameworkCore;
using PersonalFinance.Mobile.Helpers;
using PersonalFinance.Mobile.Services;
using PersonalFinance.Shared.DTOs.Categories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PersonalFinance.Mobile.ViewModels
{
    public class CategoriesViewModel :BaseViewModel
    {
        public CategoryService _categoryService;
        //Categories collection
        public ObservableCollection<CategoryDto> Categories { get; set; }

        //Commands
        public ICommand AddCommand { get; }

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public CategoriesViewModel()
        {
            _categoryService = new CategoryService();


            Categories = new ObservableCollection<CategoryDto>();

            //Navigate to dadd form
            AddCommand = new Command(async () => await Shell.Current.GoToAsync("category-form"));

            //Edit category
            EditCommand = new Command<CategoryDto>(async (category) => await EditCategory(category));

            //Delete Category
            DeleteCommand = new Command<Guid>(async (id) => await DeleteCategory(id));

        }
        //Load categories
        public  async Task LoadCategories()
        {
            try
            {
                var categories = await _categoryService.GetCategoriesAsync();

                Categories.Clear();

                foreach (var category in categories)
                    Categories.Add(category);
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

        //------EDIT Category
        private async Task EditCategory(CategoryDto category)
        {
            TemporaryCategoryData.SelectedCategory = category;

            await Shell.Current.GoToAsync("category-form");
        }

        //-----------Delete Category
        public async Task DeleteCategory(Guid id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
                await LoadCategories();
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
