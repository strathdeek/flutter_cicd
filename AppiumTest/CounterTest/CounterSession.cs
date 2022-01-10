using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.IO;
using System.Reflection;

namespace AppiumTest
{
    public class CounterSession
    {
        // Note: append /wd/hub to the URL if you're directing the test at Appium
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        //private const string CounterAppPath = @"C:\Users\kevin\source\repos\flutter_cicd\build\windows\runner\Release\flutter_cicd.exe";

        protected static WindowsDriver<WindowsElement> session;

        public static void Setup(TestContext context)
        {
            // Launch Calculator application if it is not yet launched
            if (session == null)
            {
                // Create a new session to bring up an instance of the Calculator application
                // Note: Multiple calculator windows (instances) share the same process Id
                string cur = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string rootDir = Path.Combine(cur, @"..\..\..\");
                string CounterAppPath = Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "..", "..", "build", "windows", "runner", "Release", "flutter_cicd.exe");
                Console.WriteLine(CounterAppPath);
                AppiumOptions options = new AppiumOptions();
                options.AddAdditionalCapability("app", CounterAppPath);
                options.AddAdditionalCapability("deviceName", "WindowsPC");
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), options);
                Assert.IsNotNull(session);
                // Set implicit timeout to 1.5 seconds to make element search to retry every 500 ms for at most three times
                session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);
            }
        }

        public static void TearDown()
        {
            // Close the application and delete the session
            if (session != null)
            {
                session.Quit();
                session = null;
            }
        }
    }
}
