using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace automation
{
    [TestClass]
    public class TestGmail
    {
        readonly IWebDriver driver = new ChromeDriver();                                                          //Define driver as our webdriver for Chrome Browser
        [TestMethod]
        [TestCategory("Functional")]
        public void TestCase001()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));                              //Explicit wait to Delay the program based on a condition

            driver.Manage().Window.Maximize();                                                                     //Browser to Full Screen
            driver.Navigate().GoToUrl("https://gmail.com");                                                        //Test gets a URl


            driver.FindElement(By.Id("identifierId")).SendKeys("TestAutomation422@gmail.com");                         //Entering Gmail


            driver.FindElement(By.XPath("//*[@id='identifierNext']/div/button/span")).Click();                     //Next Button Click


            IWebElement password = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='password']/div[1]/div/div[1]/input")));  //Using our explicit wait so the textbox can appear
            password.SendKeys("test1234!");


            driver.FindElement(By.XPath("//*[@id='passwordNext']/div/button")).Click();                            //Sign In button Click
            Console.WriteLine("Successfully Login");

            //Verifying the Primary tab is selected
            IWebElement primaryTab = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(":1w")));   //Explicit wait is being used in order for the primary tab to appear after redirecting.
            if (primaryTab.GetAttribute("aria-selected").Equals("false"))
            {

                primaryTab.Click();                                                                                //Clicking the Primary tab only if its not Selected
                Console.WriteLine("Primary tab is now selected");
            }
            Console.WriteLine("PrimaryTab is selected by default");

            IList<IWebElement> numberOfEmails = driver.FindElements(By.XPath("//*[@id=':23']/tbody/tr"));
            Console.WriteLine("Total number of Emails are :" + numberOfEmails.Count);                              // Print Number of Emails



            driver.FindElement(By.XPath("//*[@id=':2e']")).Click();                                                 //click on Email to Open

            //  //*[@id=':2i']    
            GetNameAndSubjectOfEmail();                                                                            //Getting name of send and subject of email (METHOD)
            driver.Close();
            driver.Quit();
            driver.Dispose();
        }
        public void GetNameAndSubjectOfEmail()                                                                     //Method to get Name Of Sender and the Subject.Stored in 2 new values and Printing them to review the results.
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement senderName = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[7]/div[3]/div/div[2]/div[1]/div[2]/div/div/div/div/div[2]/div/div[1]/div/div[2]/div/table/tr/td[1]/div[2]/div[2]/div/div[3]/div/div/div/div/div/div[1]/div[2]/div[1]/table/tbody/tr[1]/td[1]/table/tbody/tr/td/h3/span/span[1]")));
            string name = senderName.Text;
            string subject = driver.FindElement(By.XPath("/html/body/div[7]/div[3]/div/div[2]/div[1]/div[2]/div/div/div/div/div[2]/div/div[1]/div/div[2]/div/table/tr/td[1]/div[2]/div[1]/div[2]/div[1]/h2")).Text;
            Console.WriteLine("Senders Name is " + name);
            Console.WriteLine("Subject of Email is " + subject);
        }
    }
}
