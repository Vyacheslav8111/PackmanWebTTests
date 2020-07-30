using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebPackmanTest
{
    [TestFixture]
    public class LoadFileInProjectTests
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "https://vkozhuro:Saper81113076@test.packman.ru.dhl.com/#/";
            verificationErrors = new StringBuilder();
        }


        [Test]
        public void LoadFileInProjectTest()
        {
            // Open Home Page
            driver.Navigate().GoToUrl(baseURL);
            Thread.Sleep(1000); // Thread.Sleep - временно. Позже указать здесь ожидание появления элемента. qwere
            // Open LoadFileInProjectPage
            driver.FindElement(By.LinkText("Файлы")).Click();
            // Choise Project
            Thread.Sleep(1000); // Thread.Sleep - временно. Позже указать здесь ожидание появления элемента
            driver.FindElement(By.XPath("//select")).Click();
            new SelectElement(driver.FindElement(By.XPath("//select"))).SelectByText("OTP BANK");
            Thread.Sleep(1000); // Thread.Sleep - временно. Позже указать здесь ожидание появления элемента
            driver.FindElement(By.XPath("//select")).Click();
            new SelectElement(driver.FindElement(By.XPath("//div[2]/select"))).SelectByText("OTP BANK");
            // Load FileInProject
            // Находим на странице расположение элемента, который является файловым полем ввода
            By fileInput = By.CssSelector("input[type = file]");

            String filePath = "d:\\work\\Application\\Project\\Packman\\Packman_Project_Example\\From_TestRail\\OTP 05.03.2019_for_test.xlsx";

            // С помощью метода "sendKeys()", пропишем абсолютный путь к файлу 
            driver.FindElement(fileInput).SendKeys(filePath);
            // OpenOrdersPage
            driver.FindElement(By.LinkText("Заказы")).Click();
            Thread.Sleep(1000); // Thread.Sleep - временно. Позже указать здесь ожидание появления элемента.
            //Choise ProjectInOrdersPage
            new SelectElement(driver.FindElement(By.XPath("//select"))).SelectByText("OTP BANK");
            Thread.Sleep(1000);// Thread.Sleep - временно. Позже указать здесь ожидание появления элемента
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}