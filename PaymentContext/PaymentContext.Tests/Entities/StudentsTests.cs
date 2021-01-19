using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void TestMethod()
        {
            var student = new Student(
                firstName: "Bruno",
                lastName: "Araujo",
                document: "123.123.123-54",
                email: "Teste@hotmail.com");
        }
    }
}