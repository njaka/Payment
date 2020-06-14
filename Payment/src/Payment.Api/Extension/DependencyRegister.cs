using FluentMediator;
using Microsoft.Extensions.DependencyInjection;
using Payment.Acquiring;
using Payment.Api.Controllers.V1;
using Payment.Api.Controllers.V1.RetrievePaymentDetail;
using Payment.Application;
using Payment.Application.Port;
using Payment.Application.UseCases;
using Payment.Domain;
using Payment.Infrastructure;

namespace Payment.Api
{
    public static class DependencyRegister
    {
        internal static IServiceCollection AddPaymentApplication(this IServiceCollection services)
        {
            services.AddScoped<IUseCase<ProcessPaymentInput>, ProcessCardPayment>();
            services.AddScoped<IUseCase<RetrievePaymentInput>, RetrievePaymentDetail>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IBankService, BankService>();

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

    }
}
