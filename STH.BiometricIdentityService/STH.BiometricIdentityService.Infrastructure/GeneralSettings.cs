using System.Collections.Specialized;

namespace STH.BiometricIdentityService.Infrastructure
{
    public class GeneralSettings : BaseSettings
    {
        protected override string GetKeyPrefix()
        {
            return "";
        }

        public GeneralSettings()
        {
        }

        public GeneralSettings(NameValueCollection settings) : base(settings)
        {
        }

        public bool ApplicationEnabled
        {
            get { return GetSetting("ApplicationEnabled", true); }
            set { SetSetting("ApplicationEnabled", value.ToString()); }
        }

        public bool LoggingEnabled
        {
            get { return GetSetting("LoggingEnabled", true); }
            set { SetSetting("LoggingEnabled", value.ToString()); }
        }

        public int MaxLoginAttempts
        {
            get { return GetSetting("MaxLoginAttempts", 3); }
            set { SetSetting("MaxLoginAttempts", value.ToString()); }
        }

        public int AccountBlockedPeriod
        {
            get { return GetSetting("AccountBlockedPeriod", 3); }
            set { SetSetting("AccountBlockedPeriod", value.ToString()); }
        }

        public string Host
        {
            get { return GetSetting<string>("Host"); }
            set { SetSetting("Host", value); }
        }
    }
}