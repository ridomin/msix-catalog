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

namespace CalculatorTest
{
    [TestClass]
    public class ScenarioStandard : CalculatorSession
    {
        private static WindowsElement header;
        private static WindowsElement calculatorResult;

        [TestMethod]
        public void Home()
        {
            
            session.FindElementByName("Home").Click();
            Thread.Sleep(1500);
            WindowsElement name = session.FindElementByName("MSIX Catalog");
            Assert.IsNotNull(name);
        }

        [TestMethod]
        public void Store()
        {
            session.FindElementByName("Store").Click();
            WindowsElement name = session.FindElementByName("Store Packages");
            Thread.Sleep(1000);
            Assert.IsNotNull(name);
        }

        [TestMethod]
        public void Sideload()
        {
            session.FindElementByName("Sideload").Click();
            WindowsElement name = session.FindElementByName("Sideload Packages");
            Thread.Sleep(500);
            Assert.IsNotNull(name);
        }

        [TestMethod]
        public void Developer()
        {
            session.FindElementByName("Developer").Click();
            WindowsElement name = session.FindElementByName("Developer Packages");
            Thread.Sleep(500);
            Assert.IsNotNull(name);
        }

        [TestMethod]
        public void Framework()
        {
            session.FindElementByName("Framework").Click();
            WindowsElement name = session.FindElementByName("Framework Packages");
            Thread.Sleep(500);
            Assert.IsNotNull(name);
        }


        [TestMethod]
        public void System()
        {
            session.FindElementByName("System").Click();
            WindowsElement name = session.FindElementByName("System Packages");
            Thread.Sleep(500);
            Assert.IsNotNull(name);
        }



        [TestMethod]
        public void About()
        {
            session.FindElementByName("About").Click();
            WindowsElement name = session.FindElementByName("MSIX Catalog");
            Thread.Sleep(500);
            Assert.IsNotNull(name);
        }

  
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // Create session to launch a MSIX window
            Setup(context);


            //Assert.IsNotNull(calculatorResult);
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

        private string GetCalculatorResultText()
        {
            return calculatorResult.Text.Replace("Display is", string.Empty).Trim();
        }
    }
}
