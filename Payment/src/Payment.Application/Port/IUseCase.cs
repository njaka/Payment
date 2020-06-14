namespace Payment.Application.Port
{
    using System.Threading.Tasks;
    public interface IUseCase<in TUseCaseInput>
    {
        Task Execute(TUseCaseInput input);
    }
}
