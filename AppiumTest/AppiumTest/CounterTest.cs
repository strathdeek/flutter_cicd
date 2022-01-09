
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppiumTest
{
    [TestClass]
    public class CounterTest : CounterSession
    {
        [TestMethod]
        public void CounterTest1()
        {
            var ele0 = session.FindElementByAccessibilityId("Increment");
            Assert.IsNotNull(ele0);
            session.FindElementByAccessibilityId("Increment").Click();
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

    }
}
