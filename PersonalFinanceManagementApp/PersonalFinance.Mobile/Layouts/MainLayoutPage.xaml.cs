namespace PersonalFinance.Mobile.Layouts;

public partial class MainLayoutPage : ContentView
{
    public MainLayoutPage()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty PageContentProperty =
        BindableProperty.Create(
            nameof(PageContent),
            typeof(View),
            typeof(MainLayoutPage),
            propertyChanged: OnPageContentChanged);

    public View PageContent
    {
        get => (View)GetValue(PageContentProperty);
        set => SetValue(PageContentProperty, value);
    }

    private static void OnPageContentChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var layout = (MainLayoutPage)bindable;

        if (newValue is View view)
        {
            // VERY IMPORTANT
            view.BindingContext = layout.BindingContext;

            layout.MainContent.Content = view;
        }
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (PageContent != null)
        {
            PageContent.BindingContext = BindingContext;
        }
    }
}