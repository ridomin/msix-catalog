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
using OpenQA.Selenium.Remote;
using System;

namespace CalculatorTest
{
    public class CalculatorSession
    {
        // Note: append /wd/hub to the URL if you're directing the test at Appium
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        private const string AppId = "18656RidoMin.MSIXCatalog_vzj0fd0atvkjr!App";

        protected static WindowsDriver<WindowsElement> session;
        protected static WindowsDriver<WindowsElement> DesktopSession;


        public static void Setup(TestContext context)
        {
            // Launch Calculator application if it is not yet launched
            if (session == null)
            {
                // Create a new session to bring up an instance of the Calculator application
                // Note: Multiple calculator windows (instances) share the same process Id
                DesiredCapabilities appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("deviceName", "WindowsPC");
                DesktopSession = null;
                try
                {
                    appCapabilities.SetCapability("app", AppId);
                    DesktopSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);

                }
                catch
                {

                }

                appCapabilities.SetCapability("app", "Root");
                DesktopSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);

                var CortanaWindow = DesktopSession.FindElementByName("MSIX Catalog - 0.1.1822.0");
                var CortanaTopLevelWindowHandle = CortanaWindow.GetAttribute("NativeWindowHandle");
                CortanaTopLevelWindowHandle = (int.Parse(CortanaTopLevelWindowHandle)).ToString("x"); // Convert to Hex

                appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("appTopLevelWindow", CortanaTopLevelWindowHandle);
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                Assert.IsNotNull(session);

                // Set implicit timeout to 1.5 seconds to make element search to retry every 500 ms for at most three times
                session.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1.5));
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
    }
}
