using OpenQA.Selenium;
using System;

namespace SamplePOCProject.PageObjects
{
    public class Login
    {
        public IWebDriver driver;
        private String UserName_Element = "user-name";
        private String Password_Element = "password";
        private String LoginButton_Element = "login-button";

        public Login(IWebDriver _driver)
        {
            this.driver = _driver;

        }

        public void EnterUsername(String Username)
        {
            driver.FindElement(By.Id(UserName_Element)).SendKeys(Username);
        }
        public void EnterPassword(String Password)
        {
            driver.FindElement(By.Id(Password_Element)).SendKeys(Password);
        }
        public void ClickOnLogin()
        {
            driver.FindElement(By.Id(LoginButton_Element)).Click();
        }
    }
}
