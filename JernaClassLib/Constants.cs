using JernaClassLib.Enums;
using JernaClassLib.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JernaClassLib;

public static class Constants
{
    public const string EmailAddress = "jernaschooltesting@gmail.com";
    public const string ConfigKeyForEmailPass = "emailpassword";
    public const string PhysicalSearch = "physical";
    public const string DigitalSearch = "digital";
    public const string GradeSearch = "grade";
    public const string AllSearch = "all";
    public const string CoachSearch = "coaching";
    public const string ToolSearch = "tools-for-parents";
    public const string AuthKeyDefault = "RNDSTR";
    public const string AuthSchemeName = "AspAuth";
    public const string Admin = "Admin";
    public const string NotesClaim = "notes";
    public const string UserIdClaim = "user_id";
    public const string ApplicationType = "applicationType";
    public const string MauiApplication = "Maui";
    public const string WebApplication = "Web";
    public const string MuaiBaseAddress = "MuaiBaseAddress";

    public static readonly LearningEnvironment Structured = new(LearningEnvironmentEnum.Structured, "Structured Learning Environment");
    public static readonly LearningEnvironment Open = new(LearningEnvironmentEnum.Open, "Open Child Driven Learning Environment");
    public static readonly LearningEnvironment Other = new(LearningEnvironmentEnum.Other, "Other");
    public static readonly LearningEnvironment[] LearningEnvironments = [Structured, Open, Other];

    public static readonly SubscriptionLength Weekly = new(SubscriptionLengthEnum.Weekly, "Weekly");
    public static readonly SubscriptionLength Biweekly = new(SubscriptionLengthEnum.Biweekly, "Biweekly", null);
    public static readonly SubscriptionLength Monthly = new(SubscriptionLengthEnum.Monthly, "Monthly");
    public static readonly SubscriptionLength[] SubscriptionLengths = [Weekly, Biweekly, Monthly];

    public static readonly ToolsForParentsType Plant = new(ToolsForParentsTypeEnum.Plant, "Plant");
    public static readonly ToolsForParentsType Animal = new(ToolsForParentsTypeEnum.Animal, "Animal");
    public static readonly ToolsForParentsType Dinosaur = new(ToolsForParentsTypeEnum.Dinosaur, "Dinosaur");
    public static readonly ToolsForParentsType[] ToolsForParentsTypes = [Plant, Animal, Dinosaur];

    public static string FrontLoadedInGradeSearch(string tabIndex) => $"{tabIndex}-";

    private static readonly Regex EmailRegex = new Regex(
       @"^[^\s@]+@[^\s@]+\.[^\s@]+$", // Simplified regex for basic email validation
       RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false; 
        }
        return EmailRegex.IsMatch(email); 
    }

    public static string SanitizeHtml(string input) => Regex.Replace(input, "<.*?>", string.Empty);

}
