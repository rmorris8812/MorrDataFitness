using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace Fitness.Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        [TestCategory("Unit")]
        [Owner("Roy Morris")]
        public void TestBase64Encoding_Sucess()
        {
            var encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes("palm88!2"));
            encoded.Should().NotBeNullOrEmpty();
        }
    }
}
