using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentsExists()
        {
            var handler = new SubscriptionHandler(
                new FakeStudentRepository(),
                new FakeEmailService()
            );

            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "Nome";
            command.LastName = "Sobrenome";
            command.Document = "99999999999";
            command.Email = "hello@teste.com";
            command.BarCode = "123456789";
            command.BoletoNumber = "13519159";
            command.PaymentNumber = "165165";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 60;
            command.TotalPaid = 60;
            command.Payer = "Wayne_Corp";
            command.PayerDocument = "165161551615";
            command.PayerDocumentType = EDocumentType.CPF;
            command.PayerEmail = "batman@dc.com";
            command.Street = "Rua 1";
            command.Number = "123";
            command.Neignborhood = "Pq. Chiquinho";
            command.City = "Sorocaba";
            command.State = "SP";
            command.Country = "Brazil";
            command.ZipCode = "18789789";

            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);
        }
    }
}