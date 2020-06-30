using EventStore.ClientAPI;
using FluentMediator;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Payment.Acquiring;
using Payment.Api.Configuration;
using Payment.Api.Configuration.Model;
using Payment.Api.Controllers.V1;
using Payment.Api.Controllers.V1.RetrievePaymentDetail;
using Payment.Application;
using Payment.Application.Events.Handlers;
using Payment.Application.Port;
using Payment.Application.UseCases;
using Payment.Domain;
using Payment.Domain.Events;
using Payment.EventStore;
using Payment.Infrastructure.DataAccess.InMemory;
using Payment.Infrastructure.EventSourcing;
using System;

namespace Payment.Api
{
    public static class DependencyRegister
    {
        internal static IServiceCollection AddPaymentApplication(this IServiceCollection services, EventSourcingConfigurationModel eventSourcingConfigurationModel)
        {
            services.AddScoped<IUseCase<ProcessPaymentInput>, ProcessCardPayment>();
            services.AddScoped<IUseCase<RetrievePaymentInput>, RetrievePaymentDetail>();
           
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IBankClient, BankClient>();
            services.AddScoped<IEventSourcingHandler, EventSourcing>();
            services.AddScoped<IEventSourcing, EventStore.EventStore>();
            services.AddScoped<IBankHttpClientFactory, BankHttpClientFactory>();
            services.AddSingleton<IEventStoreConnection>(EventStoreConnection.Create(new Uri(eventSourcingConfigurationModel.ConnectionString)));
            services.AddScoped<INotificationHandler<OrderPaymentCreated>, OrderPaymentEventHandler>();
            services.AddScoped<INotificationHandler<OrderPaymentStatusChanged>, OrderPaymentEventHandler>();
            services.AddMediatR(typeof(Startup));

            services.AddFluentMediator(
            builder =>
            {
                builder.On<ProcessPaymentInput>().PipelineAsync()
                    .Call<IUseCase<ProcessPaymentInput>>((handler, request) => handler.Execute(request));

                builder.On<RetrievePaymentInput>().PipelineAsync()
                    .Call<IUseCase<RetrievePaymentInput>>((handler, request) => handler.Execute(request));

            });

            return services;
        }

        internal static IServiceCollection AddPaymentPresenterV1(this IServiceCollection services)
        {
            services.AddScoped<RetrievePaymentPresenter, RetrievePaymentPresenter>();
            services.AddScoped<IRetriePaymentOutputPort>(x => x.GetRequiredService<RetrievePaymentPresenter>());

            services.AddScoped<ProcessPaymentPresenter, ProcessPaymentPresenter>();
            services.AddScoped<IProcessPaymentOutputPort>(x => x.GetRequiredService<ProcessPaymentPresenter>());

            return services;
        }

        internal static IServiceCollection AddInMemoryDatabase(this IServiceCollection services)
        {
            services.AddScoped<IPaymentWriteRepository, PaymentWriteRepository>();
            services.AddScoped<IPaymentReadRepository, PaymentReadRepository>();
            services.AddSingleton<IDatabase, InMemoryDatabase>();

            return services;
        }
    }
}
