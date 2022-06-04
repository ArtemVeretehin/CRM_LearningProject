using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrmBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrmBL.Model.Tests
{
    [TestClass()]
    public class ShopComputerModel_ForThreadsTests
    {
        

        [TestMethod()]
        public void StartTest()
        {
            var model = new ShopComputerModel_ForThreads();
            model.Start();
            Thread.Sleep(10000);
        }

        
    }
}