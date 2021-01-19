using System;

namespace PaymentContext.Domain.Entities
{
    public class CreditCardPayment : Payment
    {
        public CreditCardPayment(
            string cardHolderName,
            string cardNumber,
            string lastTransctionNumber,
            DateTime paidDate,
            DateTime expireDate,
            decimal total,
            decimal totalPaid,
            string payer,
            string document,
            string adress,
            string email) : base(
                paidDate,
                expireDate,
                total,
                totalPaid,
                payer,
                document,
                adress,
                email)
        {
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            LastTransctionNumber = lastTransctionNumber;
        }

        public string CardHolderName { get; private set; }
        public string CardNumber { get; private set; }
        public string LastTransctionNumber { get; private set; }
    }
}