using OpenQA.Selenium;
using System;

namespace SamplePOCProject.PageObjects.Dashboard
{
    public class Dashboard
    {
        public IWebDriver driver;
        public String BuurgerMenu_Element = "react-burger-menu-btn";
        public Dashboard(IWebDriver _driver)
        {
            this.driver = _driver;
        }

    }
}
