using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EC2
{
    [TestClass]
    public class Tests
    {
        private static IWebDriver Driver;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            Driver = new ChromeDriver(options);
        }

        [TestMethod]
        public void EC2_CodeTest()
        {
            EC2PageObject ec2Page = new EC2PageObject(Driver);
            ec2Page.Navigate();

            //Objective 1. Confirm title change
            ec2Page.RDSTab.Click();
            Assert.AreEqual("EC2Instances.info Easy Amazon RDS Instance Comparison", ec2Page.HeaderText.Text);

            //Objective 2. Confirm background row color change
            ec2Page.TestRow.Click();
            Assert.IsTrue(ec2Page.TestRow.GetAttribute("class").Contains("highlight"));
            Assert.AreEqual("rgba(211, 255, 255, 1)", ec2Page.TestRow.GetCssValue("background-color"));

            //Objective 3. Confirm “Confirm selected” button action
            ec2Page.CompareButton.Click();
            Assert.AreEqual("End Compare", ec2Page.CompareButton.Text);
            Assert.AreEqual(1, ec2Page.RowCount());
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            Driver.Dispose();
        }
    }
}