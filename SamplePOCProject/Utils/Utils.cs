
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Configuration;
using System.IO;
namespace SamplePOCProject.Utils
{
    public class Utils
    {
        string CurrentAppdirectory = TestContext.CurrentContext.TestDirectory;
        public IWebDriver driver;
        public Utils(IWebDriver _driver)
        {
            this.driver = _driver;
        }

        public ExtentReports CreateExtentReport(string NameOfHtmlReport)
        {
            DirectoryInfo di = Directory.CreateDirectory(CurrentAppdirectory + ConfigurationManager.AppSettings["FolderOfHtmlReport"]);
            ExtentReports extent = new ExtentReports();

            ExtentV3HtmlReporter reporter = new ExtentV3HtmlReporter(CurrentAppdirectory + ConfigurationManager.AppSettings["FolderOfHtmlReport"] + NameOfHtmlReport);
            reporter.Config.DocumentTitle = "Automation Testing Report";
            reporter.Config.ReportName = "Regression Testing";
            reporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            extent.AttachReporter(reporter);
            extent.AddSystemInfo("Application Under Test", "Sample dotnet POC");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Machine", Environment.MachineName);
            extent.AddSystemInfo("OS", Environment.OSVersion.VersionString);

            return extent;
        }
        public void LogTestStatus(ExtentTest test, IWebDriver WebDriverForExtentReport)
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = "" + TestContext.CurrentContext.Result.StackTrace + "";
            var errorMessage = TestContext.CurrentContext.Result.Message;


            switch (status)
            {
                case TestStatus.Failed:

                    string ScreenShot_Title = TestContext.CurrentContext.Test.MethodName;
                    string screenShotPath = TakeScreenShot(WebDriverForExtentReport, ScreenShot_Title);
                    test.Log(Status.Fail, "Test ended with " + Status.Fail + " – " + errorMessage);
                    test.Log(Status.Fail, "Snapshot below: " + test.AddScreenCaptureFromPath(screenShotPath, ScreenShot_Title + ".png"));
                    TestContext.AddTestAttachment(screenShotPath, ScreenShot_Title);
                    break;
                case TestStatus.Skipped:

                    test.Log(Status.Skip, "Test ended with " + Status.Skip);
                    break;
                default:

                    test.Log(Status.Pass, "Test ended with " + Status.Pass);
                    break;
            }

        }
        public string TakeScreenShot(IWebDriver driver, string screenShotName)
        {
            string localpath = string.Empty;
            try
            {
                ITakesScreenshot ts = (ITakesScreenshot)driver;
                Screenshot screenshot = ts.GetScreenshot(); string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                DirectoryInfo di = Directory.CreateDirectory(CurrentAppdirectory + ConfigurationManager.AppSettings["FolderForScreenShots"]);
                string finalpth = CurrentAppdirectory + ConfigurationManager.AppSettings["FolderForScreenShots"] + "\\" + screenShotName + ".png";
                localpath = new Uri(finalpth).LocalPath;
                screenshot.SaveAsFile(localpath);
            }
            catch (Exception e)
            {
                throw (e);
            }
            return localpath;
        }


    }
}
