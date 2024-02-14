using Microsoft.VisualStudio.TestTools.UnitTesting;
using ActorRepositoryLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorRepositoryLib.Tests
{
    [TestClass()]
    public class ActorTests
    {
        [TestMethod()]
        public void validateNameTest()
        {
            Actor a1 = new Actor()
            {
                Id = 2,
                Name = "xiaoyu"
            };
            a1.validateName();

            Actor a2 = new Actor()
            {
                Id = 1,
                Name = null   
            };

            Assert.ThrowsException<ArgumentNullException>(() => a2.validateName());

            Actor a3 = new Actor()
            {
                Id = 2,
                Name = ""
            };

            Assert.ThrowsException<ArgumentException>(() => a3.validateName());
        }

        [TestMethod()]
        public void validateBirthYearTest()
        {
            Actor a4 = new Actor()
            {
                Id = 2,
                Name = "xiao",
                BirthYear= 1819
            };
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => a4.validateBirthYear());
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Actor a5 = new Actor()
            {
                Id = 2,
                Name = "xiao",
                BirthYear = 1819
            };
            string str =a5.ToString();
            Assert.AreEqual("2,xiao,1819", str);
        }
    }
}