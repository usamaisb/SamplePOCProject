using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
namespace SamplePOCProject
{
    [TestFixture]
    public class LoginTest
    {

        IWebDriver driver;

        [SetUp]
        public void Setup() {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }
        [Test]
        public void LoginTestIntoApp()
        {

        }
        [TearDown]
        public void TearDown() {
            driver.Quit();
        }
    }
}
