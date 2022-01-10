
using System;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace AppiumTest
{
    [TestClass]
    public class CounterTest : CounterSession
    {
        [TestMethod]
        public void CounterTest1()
        {
            var ele0 = session.FindElementByName("0");
            Assert.IsNotNull(ele0);
            var counter = session.FindElementByName("Increment");
            Assert.IsNotNull(counter);
            counter.Click();
            var ele1 = session.FindElementByName("1") ;
            Assert.IsNotNull(ele1);
            var screenshot = session.GetScreenshot();
            TakeScreenshot();
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // Create session to launch a Calculator window
            Setup(context);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }

        private void TakeScreenshot()
        {
            Screenshot ss = session.GetScreenshot();

            // This succeeds because it's saving to a file...
            ss.SaveAsFile(Path.Combine(GetScreenShotDirectory(), "Hello, world!.png"), ScreenshotImageFormat.Png);

            // System.UnauthorizedAccessException because it's trying to overwrite a directory...
            //ss.SaveAsFile(GetScreenShotDirectory(), ScreenshotImageFormat.Png);

        }

        private string GetScreenShotDirectory()
        {
            string cur = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string screenshotDir = Path.Combine(cur, @"..\..\..\", "Screenshots");
            SetAccessRule(screenshotDir);
            return screenshotDir;
        }

        private void SetAccessRule(string directory)
        {
            System.Security.AccessControl.DirectorySecurity sec = (new DirectoryInfo(directory)).GetAccessControl(); ;
            FileSystemAccessRule accRule = new FileSystemAccessRule(Environment.UserDomainName + "\\" + Environment.UserName, FileSystemRights.FullControl, AccessControlType.Allow);
            sec.AddAccessRule(accRule);
        }

    }
}
