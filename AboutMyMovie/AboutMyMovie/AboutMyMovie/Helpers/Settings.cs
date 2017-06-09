// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace AboutMyMovie.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        public static bool IsLoggedIn => !string.IsNullOrEmpty(UserId);

        public const string AuthTokenKey = "authtoken";
        private static readonly string AuthTokenDefault = string.Empty;

        public static string AuthToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(AuthTokenKey, AuthTokenDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AuthTokenKey, value);
            }
        }

        public const string UserIdKey = "userid";
        private static readonly string UserIdDefault = string.Empty;

        public static string UserId
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserIdKey, UserIdDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserIdKey, value);
            }
        }

        public const string UserNameKey = "username";
        private static readonly string UserNameDefault = string.Empty;

        public static string UserName
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserNameKey, UserNameDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserNameKey, value);
            }
        }

        public const string UserImageUrlKey = "userimg";
        private static readonly string UserImageUrlDefault = string.Empty;

        public static string UserImageUrl
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserImageUrlKey, UserImageUrlDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserImageUrlKey, value);
            }
        }
    }
}