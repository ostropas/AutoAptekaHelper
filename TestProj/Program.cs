﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestProj
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var app = new Application("config.json");
            while(true)
            {
                await app.Run();
            }

            //ChromeOptions options = new ChromeOptions();
            //options.AddArguments("--window-size=1920,1080");
            //options.AddArguments("--start-maximized");
            //options.AddArguments("--disable-web-security");
            //options.AddArguments("--disable-javascript");
            //options.AddArguments("--remote-debugging-port=4444");
            //options.AddAdditionalCapability("useAutomationExtension", false);
            //options.AddArguments("--headless");

            //System.Environment.SetEnvironmentVariable("webdriver.chrome.binary", "/usr/bin/chromium-browser");

            //IWebDriver driver = new ChromeDriver("/usr/bin", options);

            //driver.Navigate().GoToUrl("https://www.google.com/");

            //driver.Close();
        }
    }
}
