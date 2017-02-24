using Decision.Common.SignalRToolkit;

namespace Decision.Common.Domain
{
    public interface IHasRowLevelSecurity
    {
        long UserId { get; set; }
    }
}