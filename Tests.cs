using System;
using System.Threading;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumProject1
{
    public class Tests
    {
        [Fact]
        [Trait("Category", "Smoke")]
        
        public void LoginAndLogout()
        {
            string email = "seleniumuser@test.com";
            string password = "Testing2022$$";
            string expectedJobsUrl = "https://app.hireologyqa.com/jobs";
            string actualJobsUrl;
            string expectedLoginUrl = "https://app.hireologyqa.com/users/sign_in";
            string actualLoginUrl;
            
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://app.hireologyqa.com/users/sign_in");
                Thread.Sleep(2000);

                //Input email and password
                IWebElement emailField = driver.FindElement(By.Id("user_username"));
                emailField.SendKeys(email);
                //Thread.Sleep(2000);
                
                IWebElement pwField = driver.FindElement(By.Id("user_password"));
                pwField.SendKeys(password);
                Thread.Sleep(2000);
                
                //Click Sign In Button.
                IWebElement signInButton = driver.FindElement(By.ClassName("btn-auth"));
                signInButton.Click();
                Thread.Sleep(2000);

                //Verify the jobs page is displayed after logging in successfully
                actualJobsUrl = driver.Url;
                Assert.Equal(expectedJobsUrl, actualJobsUrl);
                //Thread.Sleep(2000);
 
                //Verify "Hi Selenium" is displayed at the top of the page
                IWebElement hiLabel = driver.FindElement(By.XPath("//*[@data-qa-id='navbar-dropdown-button']"));
                string actualLabelText = hiLabel.Text;
                Assert.Equal("Hi Selenium", actualLabelText);
                hiLabel.Click();
                
                //Sign out of the app
                IWebElement signOutLink = driver.FindElement(By.Id("signout"));
                signOutLink.Click();
                //Thread.Sleep(2000);
                
                //Verify user is taken back to the Login/Signin page
                actualLoginUrl = driver.Url;
                Assert.Equal(expectedLoginUrl, actualLoginUrl);
                //Thread.Sleep(2000);
            }
        }
    }
}