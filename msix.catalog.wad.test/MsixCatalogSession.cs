//******************************************************************************
//
// Copyright (c) 2017 Microsoft Corporation. All rights reserved.
//
// This code is licensed under the MIT License (MIT).
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//******************************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Drawing;
using System.IO;
using System.Threading;

namespace msix.catalog.wad.test
{
    public class MsixCatalogSession
    {
        // Note: append /wd/hub to the URL if you're directing the test at Appium
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        private const string AppId = "18656RidoMin.MSIXCatalogNightly_0z5p9mqqb1pac!App";
        protected static WindowsDriver<WindowsElement> session;
        protected static WindowsDriver<WindowsElement> DesktopSession;
        private TestContext testContextInstance;
        private const int hostedAgentTimer = 45000;

        public static void Setup(TestContext context)
        {
            if (session == null)
            {
                // Create a new session to bring up an instance of the Calculator application
                DesiredCapabilities appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("deviceName", "WindowsPC");
                DesktopSession = null;
                appCapabilities.SetCapability("app", AppId);
                try
                {
                    Console.WriteLine("Trying to Launch App");
                    DesktopSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                }
                catch {
                    Console.WriteLine("Failed to attach to app session (expected).");
                }
                //Setting thread sleep timer. Hosted Agents take approximately 35 seconds to launch app
                Thread.Sleep(hostedAgentTimer);
                appCapabilities.SetCapability("app", "Root");
                DesktopSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                Console.WriteLine("Attaching to MSIXCatalogMainWindow");
                try
                {
                    //Trying to launch app (500 error expected in WAD v1.1)
                    var mainWindow1 = DesktopSession.FindElementByAccessibilityId("MSIXCatalogMainWindow"); // DesktopSession.FindElementByName("MSIX Catalog - 0.1.1822.0");
                }
                catch
                {
                    Console.WriteLine("Switching to Desktop session.");

                }
                var mainWindow = DesktopSession.FindElementByAccessibilityId("MSIXCatalogMainWindow");
                Console.WriteLine("Getting Window Handle");
                var mainWindowHandle = mainWindow.GetAttribute("NativeWindowHandle");
                mainWindowHandle = (int.Parse(mainWindowHandle)).ToString("x"); // Convert to Hex
                appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("appTopLevelWindow", mainWindowHandle);
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                Assert.IsNotNull(session);
                 // Set implicit timeout to 1.5 seconds to make element search to retry every 500 ms for at most three times
                 //   session.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(3.5));
            }
        }

        public static void TearDown()
        {
            // Close the application and delete the session
            if (session != null)
            {
                session.FindElementByAccessibilityId("PART_Close").Click();
            
                session.Quit();
                session = null;

            }
        }

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
    }
}
