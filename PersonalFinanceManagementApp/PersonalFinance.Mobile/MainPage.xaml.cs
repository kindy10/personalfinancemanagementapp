using PersonalFinance.Mobile.Services;

namespace PersonalFinance.Mobile
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        /*private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }*/
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var api = new ApiService();

            try
            {
                var result = await api.GetAsync<object>("transactions");
                Console.WriteLine("Api works");
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.ToString()); 
            }
        }
    }
}
