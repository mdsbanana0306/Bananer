using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace bananer.Models.Settings
{
    public class Consumer
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }

        public Consumer(string consumerKey = "", string consumerSecret = "")
        {
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
        }
    }

    public class Account
    {
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }

        public Account(string accessToken = "", string accessTokenSecret = "")
        {
            AccessToken = accessToken;
            AccessTokenSecret = accessTokenSecret;
        }
    }

    public class Setting
    {
        public Consumer Consumer { get; set; } = new Consumer();
        public Account Account { get; set; } = new Account();
    }

    public class SettingManager
    {
        public SettingManager(string fileName)
        {
            this.fileName = fileName;
        }

        private string fileName;

        public Setting Setting
        {
            get;
            private set;
        }

        public void LoadFile()
        {
            if (!System.IO.File.Exists(fileName))
            {
                Setting = new Setting();
                return;
            }

            using (var file = new System.IO.StreamReader(fileName))
            {
                Setting = JsonConvert.DeserializeObject<Setting>(file.ReadToEnd());
            }
        }

        public void SaveFile()
        {
            using (var file = new System.IO.StreamWriter(fileName))
            {
                file.Write(JsonConvert.SerializeObject(Setting, Formatting.Indented));
            }
        }
    }
}
