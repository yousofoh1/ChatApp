using Core.Interfaces.Repos;

namespace Core.Interfaces
{
    public interface IUOW
    {
        IAuthRepo AuthRepo { get; }
    }
}