namespace PersonalFinance.Mobile.Helpers;

public static class CategoryIconHelper
{
    public static string GetIcon(string categoryName)
    {
        return categoryName?.ToLower() switch
        {
            "food" => "🍔",
            "transport" => "🚕",
            "salary" => "💰",
            "shopping" => "🛒",
            "rent" => "🏠",
            "education" => "🎓",
            "health" => "💊",
            "entertainment" => "🎮",
            "travel" => "✈️",
            "utilities" => "⚡",

            _ => "📊"
        };
    }
}