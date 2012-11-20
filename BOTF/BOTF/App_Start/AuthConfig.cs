using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using BOTF.Models;

namespace BOTF
{
    public class userService
    {
        public string Token { get; set; }
        public string Provider { get; set; }
    }

    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            
         

            /*Tweeter Configuration*/
            Settings.Settings.TwitterConsumerKey = "LKLeLT9sw38CdVTnWb8IIw";
            Settings.Settings.TwitterConsumerSecret = "uKr3F9GKjNkBGuE7DCbZvR4xcRcsPPU1ibLpPg0U";

            /*Facebook Configuration*/
            Settings.Settings.FacebookAppId = "413005845422605";
            Settings.Settings.FacebookAppSecret = "f8afed946bc4c981cc70908f4c9a1a56";
            

            /*Facebook/Twitter callback Configuration*/
            Settings.Settings.TwitterCallbackURL = "http://btf.azurewebsites.net/Account/TwitterCallback";
            Settings.Settings.FacebookCallbackURL = "http://btf.azurewebsites.net/Account/FBAuth";

            /*LastFM Configuration*/
            Settings.Settings.LastFMKey = "637768bd3e08f631449bea2f1e28bf72";
           

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            OAuthWebSecurity.RegisterTwitterClient(
                consumerKey: Settings.Settings.TwitterConsumerKey,
                consumerSecret: Settings.Settings.TwitterConsumerSecret);


            OAuthWebSecurity.RegisterFacebookClient(
                appId: Settings.Settings.FacebookAppId,
                appSecret: Settings.Settings.FacebookAppSecret
               
             );
             
           

            //OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
