﻿using CourseCatalog.App;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace CourseCatalog.App
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        private static void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                CookieDomain = "alsde.edu",
                CookieName = "Courses.ApplicationCookie." + Constants.Environment, 
                CookieHttpOnly = true, 
                CookieSecure = CookieSecureOption.Always
            });
        }
    }
}