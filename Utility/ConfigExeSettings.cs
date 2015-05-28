using System;
using System.Configuration;

namespace Utility
{
    /// <summary>    
    /// 对exe.Config文件中的appSettings段进行读写配置操作    
    /// 注意：调试时，写操作将写在vhost.exe.config文件中    
    /// </summary>    
    public static class ConfigExeSettings
    {
        static readonly string ExePath = AppDomain.CurrentDomain.BaseDirectory + "OptimalControlService.exe";

        /// <summary>    
        /// 写入值    
        /// </summary>    
        /// <param name="key">键值名称</param>    
        /// <param name="value">数值</param>    
        public static void SetValue(string key, string value)
        {
            //增加的内容写在appSettings段下 <add key="RegCode" value="0"/>    
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ExePath);
            if (config.AppSettings.Settings[key] == null)
            {
                config.AppSettings.Settings.Add(key, value);
            }
            else
            {
                config.AppSettings.Settings[key].Value = value;
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");//重新加载新的配置文件     
        }

        /// <summary>
        /// Sets the setting string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void SetSettingString(string key, string value)
        {
            string tempString = value.Trim();
            if (tempString.Length>0)
            {
                SetValue(key, tempString);
            }
        }

        /// <summary>
        /// Sets the setting int.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void SetSettingInt(string key, string value)
        {
            int iTempNumber;
            string tempString = value.Trim();
            if(Int32.TryParse(tempString, out iTempNumber))
            {
                SetValue(key, tempString);
            }
        }

        /// <summary>    
        /// 读取指定key的值    
        /// </summary>    
        /// <param name="key">键值名称</param>    
        /// <returns>value</returns>    
        public static string GetValue(string key)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ExePath);
            if (config.AppSettings.Settings[key] == null)
                return "";
            else
                return config.AppSettings.Settings[key].Value;
        }

        /// <summary>
        /// Gets the setting string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaltValue">The defalt value.</param>
        /// <returns>setting</returns>
        public static string GetSettingString(string key, string defaltValue)
        {
            string tempString = GetValue(key).Trim();
            if (tempString.Equals(""))
            {
                SetValue(key,defaltValue);
                return defaltValue;
            }
            return tempString;
        }

        /// <summary>
        /// Gets the setting int.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaltValue">The defalt value.</param>
        /// <returns>setting</returns>
        public static int GetSettingInt(string key ,int defaltValue)
        {
            int iTempNumber;
            string tempString = GetValue(key).Trim();
            if (Int32.TryParse(tempString, out iTempNumber))
            {
                return iTempNumber;
            }
            SetValue(key, defaltValue.ToString());
            return defaltValue;
        }

        /// <summary>
        /// Gets the setting single.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaltValue">The defalt value.</param>
        /// <returns>setting</returns>
        public static Single GetSettingSingle(string key, Single defaltValue)
        {
            Single sTempNumber;
            string tempString = GetValue(key).Trim();
            if (Single.TryParse(tempString, out sTempNumber))
            {
                return sTempNumber;
            }
            SetValue(key, defaltValue.ToString());
            return defaltValue;
        }

        /// <summary>
        /// Gets the setting double.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaltValue">The defalt value.</param>
        /// <returns>setting</returns>
        public static double GetSettingDouble(string key, double defaltValue)
        {
            double dTempNumber;
            string tempString = GetValue(key).Trim();
            if (double.TryParse(tempString, out dTempNumber))
            {
                return dTempNumber;
            }
            SetValue(key, defaltValue.ToString());
            return defaltValue;
        }

        /// <summary>
        /// Gets the setting bool.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaltValue">if set to <c>true</c> [defalt value].</param>
        /// <returns></returns>
        public static bool GetSettingBool(string key, bool defaltValue)
        {
            bool bTempNumber;
            string tempString = GetValue(key).Trim();
            if (bool.TryParse(tempString, out bTempNumber))
            {
                return bTempNumber;
            }
            SetValue(key, defaltValue.ToString());
            return defaltValue;
        }

        /// <summary>
        /// Gets the setting bool.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaltValue">The defalt value.</param>
        /// <returns></returns>
        public static byte GetSettingByte(string key, byte defaltValue)
        {
            byte bTempNumber;
            string tempString = GetValue(key).Trim();
            if (byte.TryParse(tempString, out bTempNumber))
            {
                return bTempNumber;
            }
            SetValue(key, defaltValue.ToString());
            return defaltValue;
        }
    }
}
