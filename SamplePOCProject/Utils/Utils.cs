using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;
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

        //public ExtentReports CreateExtentReport(string NameOfHtmlReport)
        //{
        //    DirectoryInfo di = Directory.CreateDirectory(CurrentAppdirectory + ConfigurationManager.AppSettings["FolderOfHtmlReport"]);
        //    ExtentReports extent = new ExtentReports();

        //    ExtentV3HtmlReporter reporter = new ExtentV3HtmlReporter(CurrentAppdirectory + ConfigurationManager.AppSettings["FolderOfHtmlReport"] + NameOfHtmlReport);
        //    reporter.Config.DocumentTitle = "Automation Testing Report";
        //    reporter.Config.ReportName = "Regression Testing";
        //    reporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
        //    extent.AttachReporter(reporter);
        //    extent.AddSystemInfo("Application Under Test", "Sample dotnet POC");
        //    extent.AddSystemInfo("Environment", "QA");
        //    extent.AddSystemInfo("Machine", Environment.MachineName);
        //    extent.AddSystemInfo("OS", Environment.OSVersion.VersionString);

        //    return extent;
        //}

        public ExtentReports CreateExtentReport(string NameOfHtmlReport)
        {
            DirectoryInfo di = Directory.CreateDirectory(CurrentAppdirectory + ConfigurationManager.AppSettings["FolderOfHtmlReport"]);
            ExtentReports extent = new ExtentReports(CurrentAppdirectory + ConfigurationManager.AppSettings["FolderOfHtmlReport"] + NameOfHtmlReport, false);
            try
            {
                //this is a generic function to create extent reports an its directory
                // we can call this funtion whenever we want to create a report
                // path of report to create can be passed in parameters of this function
                //To create report directory and add HTML report into it




                extent.AddSystemInfo("Environment", "Dev");
                extent.AddSystemInfo("User Name", "ZAPP");
                //extent.AttachReporter(htmlReporter);
            }
            catch (Exception e)
            {
                throw (e);
            }



            return extent;
        }

        //public void LogTestStatus(ExtentTest test, IWebDriver WebDriverForExtentReport)
        //{
        //    var status = TestContext.CurrentContext.Result.Outcome.Status;
        //    var stacktrace = "" + TestContext.CurrentContext.Result.StackTrace + "";
        //    var errorMessage = TestContext.CurrentContext.Result.Message;


        //    switch (status)
        //    {
        //        case TestStatus.Failed:

        //            string ScreenShot_Title = TestContext.CurrentContext.Test.MethodName;
        //            string screenShotPath = TakeScreenShot(WebDriverForExtentReport, ScreenShot_Title);
        //            test.Log(Status.Fail, "Test ended with " + Status.Fail + " – " + errorMessage);
        //            test.Log(Status.Fail, "Snapshot below: " + test.AddScreenCaptureFromPath(screenShotPath, ScreenShot_Title + ".png"));
        //            TestContext.AddTestAttachment(screenShotPath, ScreenShot_Title);
        //            break;
        //        case TestStatus.Skipped:

        //            test.Log(Status.Skip, "Test ended with " + Status.Skip);
        //            break;
        //        default:

        //            test.Log(Status.Pass, "Test ended with " + Status.Pass);
        //            break;
        //    }

        //}

        public void LogTestStatus(ExtentTest test, IWebDriver WebDriverForExtentReport)
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = "" + TestContext.CurrentContext.Result.StackTrace + "";
            var errorMessage = TestContext.CurrentContext.Result.Message;
            LogStatus logstatus;
            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = LogStatus.Fail;
                    string ScreenShot_Title = TestContext.CurrentContext.Test.MethodName;
                    string FilePath = CurrentAppdirectory + "//Defect_Screenshots//" + ScreenShot_Title + ".png";
                    string screenShotPath = TakeScreenShot(WebDriverForExtentReport, ScreenShot_Title);
                    test.Log(logstatus, "Test ended with " + logstatus + " – " + errorMessage);
                    test.Log(logstatus, "Snapshot below: " + test.AddScreenCapture(FilePath));
                    TestContext.AddTestAttachment(screenShotPath, ScreenShot_Title);
                    break;
                case TestStatus.Skipped:
                    logstatus = LogStatus.Skip;
                    test.Log(logstatus, "Test ended with " + logstatus);
                    break;
                default:
                    logstatus = LogStatus.Pass;
                    test.Log(logstatus, "Test ended with " + logstatus);
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
