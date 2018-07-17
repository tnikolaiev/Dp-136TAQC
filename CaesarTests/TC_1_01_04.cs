﻿using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace CaesarTests
{
    [TestFixture]
    class TC_1_01_04
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        WebDriverWait wait;

        [SetUp]
        public void Initialize()
        {
            driver.Url = @"http://localhost:3000/logout";
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            wait.Until((d) => LoginPage.IsLoginPage(d));
            loginPageInstance = new LoginPage(driver);
        }

        [Test]
        public void ExecuteTest_EmptyFields_LoginButtonClick_NoChanges()
        {
            Acts.Click(loginPageInstance.LoginButton);
            Assert.IsTrue(LoginPage.IsLoginPage(driver));
        }

        [Test]
        public void ExecuteTest_EmptyFields_EnterPressed_NoChanges()
        {
            loginPageInstance.LoginField.SendKeys(Keys.Enter);
            Assert.IsTrue(LoginPage.IsLoginPage(driver));
        }

        [Test]
        public void ExecuteTest_EmptyLoginField_LoginButtonUnactive()
        {
            Acts.InputValue(loginPageInstance.PasswordField, "pass");
            Assert.IsFalse(loginPageInstance.LoginButton.Enabled);
        }

        [Test]
        public void ExecuteTest_EmptyPassField_LoginButtonUnactive()
        {
            Acts.InputValue(loginPageInstance.LoginField, "log1");
            Assert.IsFalse(loginPageInstance.LoginButton.Enabled);
        }

        [Test]
        public void ExecuteTest_InvalidValues_ErrorMessage()
        {
            List<String> logins = new List<String>() { "login1", "#$%^#${}", "4f4&4]3", "login", "1234", "12" };
            List<String> passwords = new List<String>() { "$%^&#*", "pass2", "#@#@#@", "pa$$word", "12", "1234" };
            int i = 0;
            while (i < logins.Count)
            {
                loginPageInstance.LogIn(logins[i], passwords[i]);

                Assert.AreEqual(String.Empty, Acts.GetAttribute(loginPageInstance.PasswordField, "value"));
                Assert.AreEqual(logins[i], Acts.GetAttribute(loginPageInstance.LoginField, "value"));
                String expectedMessage = "Incorrect login or password. Please, try again";
                Assert.AreEqual(expectedMessage, loginPageInstance.MessageField.Text);

                loginPageInstance.LoginField.SendKeys(Keys.Escape);
                i++;
            }
        }

        [Test]
        public void ExecuteTest_15lettersLoginField_Contains10()
        {
            Acts.InputValue(loginPageInstance.LoginField, "abcdefghijklmno");
            Assert.AreEqual("abcdefghij", Acts.GetAttribute(loginPageInstance.LoginField, "value"));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
        }
    }
}
