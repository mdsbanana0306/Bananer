using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

using CoreTweet;

namespace bananer.Models
{
    public class LoginManager : NotificationObject
    {
        /*
         * NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */

        Settings.Consumer consumer;

        public LoginManager(Settings.Consumer consumer)
        {
            this.consumer = consumer;
        }

        public Settings.Account Account
        {
            get;
            private set;
        }

        OAuth.OAuthSession authSession;

        public Uri CreateAuthUri()
        {
            authSession = OAuth.Authorize(consumer.ConsumerKey, consumer.ConsumerSecret);
            return authSession.AuthorizeUri;
        }

        public void Auth(string pinCode)
        {
            if (authSession == null)
            {
                // 先にCreateAuthUri()する必要がある
                throw new InvalidOperationException();
            }

            if (Account == null)
            {
                var token = authSession.GetTokens(pinCode);
                Account = new Settings.Account(token.AccessToken, token.AccessTokenSecret);
            }
        }
    }
}
