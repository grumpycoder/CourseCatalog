﻿using System;
using System.ComponentModel;
using System.Configuration;

namespace CourseCatalog.App.Helpers
{
    public static class AppSettings
    {
        public static T Get<T>(string key)
        {
            var appSetting = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(appSetting)) throw new Exception($"Application Configuration key \" {key} not found \".");

            var converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)(converter.ConvertFromInvariantString(appSetting));
        }

        public static T GetDatabaseString<T>(string key)
        {
            var appSetting = ConfigurationManager.ConnectionStrings[key].ConnectionString;
            if (string.IsNullOrWhiteSpace(appSetting)) throw new Exception($"Database Configuration key \" {key} not found \".");

            var converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)(converter.ConvertFromInvariantString(appSetting));
        }

        internal static object GetDatabaseString<T>(object databaseContextName)
        {
            throw new NotImplementedException();
        }
    }
}
