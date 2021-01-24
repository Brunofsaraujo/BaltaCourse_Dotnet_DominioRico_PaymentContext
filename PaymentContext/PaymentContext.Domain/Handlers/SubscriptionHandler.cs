using System;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler :
        Notifiable,
        IHandler<CreateBoletoSubscriptionCommand>,
        IHandler<CreatePaypalSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(
            IStudentRepository repository,
            IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            // Fail fast validations

            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }

            //Verificar se Documento já está cadastrado

            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");

            //Verificar se E-mail já está cadastrado

            if (_repository.DocumentExists(command.Document))
                AddNotification("Email", "Este E-mail já está em uso");

            // Gerar os VOs

            var name = new Name(
                firstName: command.FirstName,
                lastName: command.LastName);

            var document = new Document(
                    number: command.Document,
                    type: EDocumentType.CPF
                );

            var email = new Email(
                address: command.Email);

            var address = new Address(
                street: command.Street,
                number: command.Number,
                neignborhood: command.Neignborhood,
                city: command.City,
                state: command.State,
                country: command.Country,
                zipCode: command.ZipCode
            );

            // Gerar as entidades

            var student = new Student(
                name: name,
                document: document,
                email: email
            );

            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.BarCode,
                command.BoletoNumber,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(
                    command.PayerDocument,
                    command.PayerDocumentType
                ),
                address,
                email);

            // Relacionamentos

            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as validações

            AddNotifications(name, document, email, address, student, subscription, payment);

            // Checar as notificações

            if (Invalid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura");

            //Salvar as informações

            _repository.CreateSubscription(student);

            //Enviar email de boas vindas

            _emailService.Send(
                student.Name.ToString(),
                student.Email.Address,
                "Bem vindo ao site",
                "Sua assinatura foi criada");

            //Retornar informações

            return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreatePaypalSubscriptionCommand command)
        {
            //Verificar se Documento já está cadastrado

            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");

            //Verificar se E-mail já está cadastrado

            if (_repository.DocumentExists(command.Document))
                AddNotification("Email", "Este E-mail já está em uso");

            // Gerar os VOs

            var name = new Name(
                firstName: command.FirstName,
                lastName: command.LastName);

            var document = new Document(
                    number: command.Document,
                    type: EDocumentType.CPF
                );

            var email = new Email(
                address: command.Email);

            var address = new Address(
                street: command.Street,
                number: command.Number,
                neignborhood: command.Neignborhood,
                city: command.City,
                state: command.State,
                country: command.Country,
                zipCode: command.ZipCode
            );

            // Gerar as entidades

            var student = new Student(
                name: name,
                document: document,
                email: email
            );

            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PaypalPayment(
                command.TransactionCode,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(
                    command.PayerDocument,
                    command.PayerDocumentType
                ),
                address,
                email);

            // Relacionamentos

            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as validações

            AddNotifications(name, document, email, address, student, subscription, payment);

            // Checar as notificações

            if (Invalid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura");

            //Salvar as informações

            _repository.CreateSubscription(student);

            //Enviar email de boas vindas

            _emailService.Send(
                student.Name.ToString(),
                student.Email.Address,
                "Bem vindo ao site",
                "Sua assinatura foi criada");

            //Retornar informações

            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }
}