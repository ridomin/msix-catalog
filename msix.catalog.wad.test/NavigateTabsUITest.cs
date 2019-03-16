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
using System.Threading;
using System;

namespace msix.catalog.wad.test
{
    [TestClass]
    public class NavigateTabsUITest : MsixCatalogSession
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Setup(context);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }

        [TestInitialize]
        public void Clear()
        {

        }

        [TestMethod]
        public void NavigateMainTabs()
        {
            
            session.FindElementByName("Home").Click();
            Thread.Sleep(500);
            WindowsElement name = session.FindElementByName("MSIX Catalog");
            Assert.IsNotNull(name);
                    
            var storeMenuItem = session.FindElementByName("Store");
            Assert.IsNotNull(storeMenuItem, "Store Menu Item not found");
            storeMenuItem.Click();
            Thread.Sleep(500);
            WindowsElement store = session.FindElementByAccessibilityId("TitleStorePackages");
            Assert.IsNotNull(store, "Store Packages Title not found");

            session.FindElementByName("Sideload").Click();
            Thread.Sleep(500);
            WindowsElement sdload = session.FindElementByAccessibilityId("TitleSideloadPackages");
            Assert.IsNotNull(sdload);
        
            session.FindElementByName("Developer").Click();
            Thread.Sleep(500);
            WindowsElement dvlpr = session.FindElementByAccessibilityId("TitleDeveloperPackages");
            Assert.IsNotNull(dvlpr);
        
            session.FindElementByName("Framework").Click();
            Thread.Sleep(500);
            WindowsElement fx = session.FindElementByAccessibilityId("TitleFrameworkPackages");
            Assert.IsNotNull(fx);
        
            session.FindElementByName("System").Click();
            Thread.Sleep(500);
            WindowsElement sys = session.FindElementByAccessibilityId("TitleSystemPackages");
            Assert.IsNotNull(sys);
        }



        [TestMethod]
        public void About()
        {
            session.FindElementByName("About").Click();
            Thread.Sleep(500);
            WindowsElement name = session.FindElementByAccessibilityId("LabelVersion");
            Assert.IsNotNull(name);
        }
    }
}
