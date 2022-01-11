
using System;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace AppiumTest
{
    [TestClass]
    public class CounterTest : CounterSession
    {
        [TestMethod]
        public async Task ScreenshotTest()
        {
            await Task.Delay(1000);
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
            ss.SaveAsFile(Path.Combine(GetScreenShotDirectory(), "screenshot.png"), ScreenshotImageFormat.Png);
        }

        private string GetScreenShotDirectory()
        {
            string cur = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string screenshotDir = Path.Combine(cur, "..","..","..", "Screenshots");
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
