using Core.Contracts.Repos;

namespace Core.Contracts
{
    public interface IUOW
    {
        IAuthRepo AuthRepo { get; }
    }
}