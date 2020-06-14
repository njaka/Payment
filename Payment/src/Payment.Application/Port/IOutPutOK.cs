namespace Payment.Application
{

    public interface IOutputOK<in TUseCaseOutput>
    {

        void OK(TUseCaseOutput output);
    }
}
