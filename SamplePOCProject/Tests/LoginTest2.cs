using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RelevantCodes.ExtentReports;
using SamplePOCProject.PageObjects;
using System;
using System.Configuration;

namespace SamplePOCProject
{
    [TestFixture]
    public class LoginTest2
    {

        public IWebDriver driver;
        protected ExtentReports _extent;
        protected ExtentTest _test;
        Utils.Utils utils;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            String A = ConfigurationManager.AppSettings["NameOfHtmlReport"];
            utils = new Utils.Utils(driver);
            _extent = utils.CreateExtentReport(ConfigurationManager.AppSettings["NameOfHtmlReport"]);
        }
        [SetUp]
        public void Setup()
        {
            _test = _extent.StartTest(TestContext.CurrentContext.Test.MethodName);
            driver = new ChromeDriver();
            utils = new Utils.Utils(driver);
            driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["loginUrl"]);
        }
        [Test]
        [Category("RegressionTests")]
        public void LoginTestIntoApp_Pass_3()
        {
            Login login = new Login(driver);
            login.EnterUsername(ConfigurationManager.AppSettings["username"]);
            login.EnterPassword(ConfigurationManager.AppSettings["password"]);
            login.ClickOnLogin();
            PageObjects.Dashboard.Dashboard dasboard = new PageObjects.Dashboard.Dashboard(driver);
            Assert.IsTrue(driver.FindElement(By.Id(dasboard.BuurgerMenu_Element)).Displayed);
        }
        [Test]
        [Category("RegressionTests")]
        public void LoginTestIntoApp_Fail_4()
        {
            Login login = new Login(driver);
            login.EnterUsername(ConfigurationManager.AppSettings["username"]);
            login.EnterPassword(ConfigurationManager.AppSettings["username"]);
            login.ClickOnLogin();
            PageObjects.Dashboard.Dashboard dasboard = new PageObjects.Dashboard.Dashboard(driver);
            Assert.IsTrue(driver.FindElement(By.Id(dasboard.BuurgerMenu_Element)).Displayed);
        }
        [TearDown]
        public void TearDown()
        {
            utils.LogTestStatus(_test, driver);
            _extent.Flush();
            driver.Quit();

        }
    }
}
