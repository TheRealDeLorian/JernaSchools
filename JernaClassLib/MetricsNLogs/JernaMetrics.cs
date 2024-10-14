using System.Diagnostics.Metrics;

namespace JernaClassLib.MetricsNLogs;

public class JernaMetrics
{
    public const string JernaMeterName = "JernaMeter";
    public static Meter JernaMeter = new(JernaMeterName, "1.0.0");
    public static Counter<int> HomePageTimesVisitedCount = JernaMeter.CreateCounter<int>("times_visited_home_page");
    public static Counter<int> SubmittedCarts = JernaMeter.CreateCounter<int>("submitted_carts");
    public static UpDownCounter<int> UsersLoggedIn = JernaMeter.CreateUpDownCounter<int>("users_logged_in");
    public static UpDownCounter<int> ItemsInAllCarts = JernaMeter.CreateUpDownCounter<int>("items_in_all_carts");
}


