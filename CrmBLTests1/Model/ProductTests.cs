using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrmBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmBL.Model.Tests
{
    [TestClass()]
    public class ProductTests
    {
        [TestMethod()]
        public void ProductTest()
        {
            //arrange
            //Начальные данные
            Product product = new Product() { ProductID = 1, Name = "Камера для велосипеда"};
            bool AreProductEqual = false;
            //Ожидаемые
            Product result_product = new Product() { ProductID = 1, Name = "Камера для велосипеда" };
            
            //act
            Product result_product_created = new Product() { ProductID = product.GetHashCode(), Name = product.ToString() };
            if (result_product == result_product_created) AreProductEqual = true;

            //assert
            //Assert.AreEqual(AreProductEqual, true);
            Assert.AreEqual(result_product_created, result_product);
            //Assert.Fail();
        }

        /*
        [TestMethod()]
        public void GetHashCodeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Assert.Fail();
        }
        */
    }
}