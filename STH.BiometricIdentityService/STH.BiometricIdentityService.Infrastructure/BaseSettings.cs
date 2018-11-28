using System;
using System.Collections.Specialized;
using System.Configuration;
using STH.BiometricIdentityService.Infrastructure.Extensions;

namespace STH.BiometricIdentityService.Infrastructure
{
    public abstract class BaseSettings: ISettings
    {
        private NameValueCollection _settings;

        private string _keyPrefix;
        protected virtual string GetKeyPrefix()
        {
            if (_keyPrefix == null)
            {
                _keyPrefix = GetType().Name.Replace("Settings", "");
            }
            return _keyPrefix;
        }

        protected BaseSettings()
            : this(ConfigurationManager.AppSettings)
        {
        }

        protected BaseSettings(NameValueCollection settings)
        {
            _settings = settings;
        }

        private string GetValue(string name)
        {
            return _settings[GetKey(name)];
        }

        private string GetKey(string name)
        {
            var prefix = GetKeyPrefix();
            var key = name;
            if (prefix.IsNotEmpty())
                key = prefix + "." + key;
            return key;
        }

        public T GetSetting<T>(string name)
            where T : IConvertible
        {
            return GetSetting(name, default(T));
        }

        public void SetSetting(string name, string value)
        {
            _settings[GetKey(name)] = value;
        }

        public T GetSetting<T>(string name, T defaultValue) where T : IConvertible
        {
            var value = GetValue(name);

            return value == null ? defaultValue : (T)Convert.ChangeType(value, typeof(T));
        }
    }

    public interface ISettings
    {
        T GetSetting<T>(string name) where T : IConvertible;

        void SetSetting(string name, string value);

        T GetSetting<T>(string name, T defaultValue) where T : IConvertible;
    }
}