using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void TestMethod()
        {
            var name = new Name(
                firstName: "Bruno",
                lastName: "Araujo"
            );

            var student = new Student(
                name: name,
                document: new Document("123.123.123-54", EDocumentType.CPF),
                email: new Email("Teste@hotmail.com"));
        }

        [TestMethod]
        public void AdicionarAssinatura()
        {
            // var name = new Name("Teste", "Teste");
            // foreach (var not in name.Notifications)
            // {
            //     not.Message;
            // }
        }
    }
}