using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections;
using System.Collections.Generic;
namespace FinalExamJesus
{
    [TestFixture]
    public class Class1
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Url = "https://candidatex:qa-is-cool@qa-task.backbasecloud.com/";
        }

        public void SignIn()
        {
            string email = "cnew@gmail.com";
            string password = "qweqwe";
            driver.FindElement(By.XPath("//a[@routerlink='/login']")).Click();
            driver.FindElement(By.XPath("//input[@formcontrolname='email']")).SendKeys(email);
            driver.FindElement(By.XPath("//input[@formcontrolname='password']")).SendKeys(password);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        [Test, Order (1)]
        [Category("Smoke Test")]
        public void SignInTest()
        {
            IWebElement profileName = driver.FindElement(By.XPath("//a[contains(text(),'cnew')]"));
            //Console.Write(profileName.Text);
            Assert.AreEqual(profileName.Text,"cnew");
        }

        [Test, Order(2)]
        [Category("Smoke Test")]
        public void CreateArticle()
        {
            SignIn();
            driver.FindElement(By.XPath("//a[@routerlink='/editor']")).Click();
            driver.FindElement(By.XPath("//input[@formcontrolname='title']")).SendKeys("This is a Title");
            driver.FindElement(By.XPath("//input[@formcontrolname='description']")).SendKeys("This is a Description");
            driver.FindElement(By.XPath("//textarea[@formcontrolname='body']")).SendKeys("Body of the Article");
            driver.FindElement(By.XPath("//input[@placeholder='Enter tags']")).SendKeys("hello");
            driver.FindElement(By.XPath("//input[@placeholder='Enter tags']")).SendKeys(Keys.Enter);
            driver.FindElement(By.XPath("//button[@type='button']")).Click();
            IWebElement title = driver.FindElement(By.XPath("//h1[contains(text(),'Title')]"));
            Assert.AreEqual(title.Text, "This is a Title");
            IWebElement author = driver.FindElement(By.ClassName("author"));
            Assert.AreEqual(author.Text, "cnew");
            //Console.WriteLine(title.Text);
            //Console.WriteLine(author.Text);
        }

        [Test, Order(3)]
        [Category("Smoke Test")]
        public void FollowUser()
        {
            SignIn();
            driver.FindElement(By.XPath("//a[contains(text(),'tag?')]")).Click();
            driver.FindElement(By.XPath("//a[contains(text(),'artem')]")).Click();
            driver.FindElement(By.XPath("//button[@class='btn btn-sm action-btn btn-outline-secondary']")).Click();
            IWebElement unfollow = driver.FindElement(By.XPath("//button[@class='btn btn-sm action-btn btn-secondary']"));
            Assert.AreEqual(unfollow.Text.Trim(), "Unfollow artem");
         }

        [Test, Order(4)]
        [Category("Smoke Test")]
        public void addComment()
        {
            SignIn();
            driver.FindElement(By.XPath("//a[contains(text(),'Fawkes')]")).Click();
            driver.FindElement(By.XPath("//a[@class='preview-link']")).Click();
            driver.FindElement(By.XPath("//textarea[@placeholder='Write a comment...']")).SendKeys("This is a test comment");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            IWebElement comment = driver.FindElement(By.XPath("//p[@class='card-text']"));
            Assert.AreEqual(comment.Text, "This is a test comment");
        }

        [Test, Order(5)]
        [Category("Smoke Test")]
        public void PostList()
        {
            SignIn();
            driver.FindElement(By.XPath("//a[contains(text(),'Global Feed')]")).Click();
            var posts = driver.FindElements(By.TagName("h1"));
            foreach (IWebElement post in posts)
            {
                Console.WriteLine(post.Text);
            }
            Assert.AreEqual(posts.Count, 10);
            
            string hola = DateTime.Now.ToString();
            Console.WriteLine(hola);
        }

        [TearDown]
        public void TearDown()
        {
           driver.Quit();
        }
    }
}