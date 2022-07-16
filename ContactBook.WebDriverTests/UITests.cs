using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;

namespace ContactBook.WebDriverTests
{
    public class UITests
    {
        private const string url = "https://contactbook.nakov.repl.co/";
        //private const string url = "http://localhost:8080";
        private WebDriver driver;

        [SetUp]
        public void OpenBrowser()
        {
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }


        [Test]
        public void Test_ListContacts_CheckFirstContact()
        {            
            driver.Navigate().GoToUrl(url);
            driver.FindElement(By.LinkText("Contacts")).Click();
            var firstName = driver.FindElement(By.CssSelector("tr.fname > td"));
            var lastName = driver.FindElement(By.CssSelector("tr.lname > td"));
            Assert.That(firstName.Text, Is.EqualTo("Steve"));
            Assert.That(lastName.Text, Is.EqualTo("Jobs"));
        }

        [Test]
        public void Test_SearchContact_CheckFirstContact()
        {
            driver.Navigate().GoToUrl(url);

            driver.FindElement(By.LinkText("Search")).Click();
            var textBox = driver.FindElement(By.Id("keyword"));
            textBox.SendKeys("albert");

            driver.FindElement(By.Id("Search")).Click();

            var firstName = driver.FindElement(By.CssSelector("tr.fname > td"));
            var lastName = driver.FindElement(By.CssSelector("tr.lname > td"));
            Assert.That(firstName.Text, Is.EqualTo("Albert"));
            Assert.That(lastName.Text, Is.EqualTo("Einstein"));
        }
        [Test]
        public void Test_SearchInvalidContact_CheckResult()
        {
            driver.Navigate().GoToUrl(url);

            driver.FindElement(By.LinkText("Search")).Click();
            var textBox = driver.FindElement(By.Id("keyword"));
            textBox.SendKeys("invalid2635");

            driver.FindElement(By.Id("Search")).Click();

            var error = driver.FindElement(By.Id("searchResult"));
            
            Assert.That(error.Text, Is.EqualTo("No contacts found."));           
            
        }

        [Test]
        public void Test_CreateNewContact_WithInvalidFirstName()
        {
            driver.Navigate().GoToUrl(url);

            driver.FindElement(By.LinkText("Create")).Click();
            var firstNameBox = driver.FindElement(By.Id("firstName"));
            var lastNameBox = driver.FindElement(By.Id("lastName"));
            lastNameBox.SendKeys("Ivan");

            var emailBox = driver.FindElement(By.Id("email"));
            emailBox.SendKeys("ivan@abv.bg");            

            driver.FindElement(By.Id("create")).Click();

            var error = driver.FindElement(By.CssSelector(".err")).Text;

            Assert.That(error, Is.EqualTo("Error: First name cannot be empty!"));
        }

        [Test]
        public void Test_CreateNewContact_WithInvalidLastName()
        {
            driver.Navigate().GoToUrl(url);

            driver.FindElement(By.LinkText("Create")).Click();
            var firstNameBox = driver.FindElement(By.Id("firstName"));
            firstNameBox.SendKeys("Ivan");

            var lastNameBox = driver.FindElement(By.Id("lastName"));

            var emailBox = driver.FindElement(By.Id("email"));
            emailBox.SendKeys("ivan@abv.bg");

            driver.FindElement(By.Id("create")).Click();

            var error = driver.FindElement(By.CssSelector(".err")).Text;

            Assert.That(error, Is.EqualTo("Error: Last name cannot be empty!"));
        }

        [Test]
        public void Test_CreateNewContact_WithInvalidEmail()
        {
            driver.Navigate().GoToUrl(url);

            driver.FindElement(By.LinkText("Create")).Click();
            var firstNameBox = driver.FindElement(By.Id("firstName"));
            firstNameBox.SendKeys("Ivan");

            var lastNameBox = driver.FindElement(By.Id("lastName"));
            lastNameBox.SendKeys("Ivanov");

            var emailBox = driver.FindElement(By.Id("email"));
            
            driver.FindElement(By.Id("create")).Click();

            var error = driver.FindElement(By.CssSelector(".err")).Text;

            Assert.That(error, Is.EqualTo("Error: Invalid email!"));
        }

        [Test]
        public void Test_CreateNewContact_WithValidData()
        {
            driver.Navigate().GoToUrl(url);

            driver.FindElement(By.LinkText("Create")).Click();
            var firstNameBox = driver.FindElement(By.Id("firstName"));
            firstNameBox.SendKeys("Ivan");

            var lastNameBox = driver.FindElement(By.Id("lastName"));
            lastNameBox.SendKeys("Ivanov");

            var emailBox = driver.FindElement(By.Id("email"));
            emailBox.SendKeys("Ivanov@abv.bg");

            driver.FindElement(By.Id("create")).Click();

            var contacts = driver.FindElements(By.CssSelector(".contact-entry"));
            var lastContact = contacts.Last();
            var fname = lastContact.FindElement(By.CssSelector(".fname > td"));
            var lname = lastContact.FindElement(By.CssSelector(".lname > td"));

            Assert.That(fname.Text, Is.EqualTo("Ivan"));
            Assert.That(lname.Text, Is.EqualTo("Ivanov"));
        }

        [TearDown]
        public void CloseBrowser()
        {
            this.driver.Quit();
        }

    }
}