using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Email _email;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Student _student;
        private readonly Subscription _subscription;

        public StudentTests()
        {
            _name = new Name(
                firstName: "Bruce",
                lastName: "Wayne");

            _document = new Document(
                    number: "12345678912",
                    type: EDocumentType.CPF
                );

            _email = new Email(
                address: "batman@dc.com");

            _address = new Address(
                street: "Rua 1",
                number: "1234",
                neignborhood: "Bairro legal",
                city: "Gotham",
                state: "New York",
                country: "US",
                zipCode: "123"
            );

            _student = new Student(
                name: _name,
                document: _document,
                email: _email
            );

            _subscription = new Subscription(null);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            var payment = new PaypalPayment(
                transactionCode: "12345678",
                paidDate: DateTime.Now,
                expireDate: DateTime.Now.AddDays(5),
                total: 10,
                totalPaid: 10,
                payer: "Wayne Corp",
                document: _document,
                adress: _address,
                email: _email);

            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
        {
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription()
        {
            var payment = new PaypalPayment(
                transactionCode: "12345678",
                paidDate: DateTime.Now,
                expireDate: DateTime.Now.AddDays(5),
                total: 10,
                totalPaid: 10,
                payer: "Wayne Corp",
                document: _document,
                adress: _address,
                email: _email);

            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Valid);
        }
    }
}