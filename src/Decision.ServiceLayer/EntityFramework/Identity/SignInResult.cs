namespace Decision.ServiceLayer.EntityFramework.Identity
{
    public enum SignInResult
    {
        Success = 0,
        LockedOut = 1,
        RequiresVerification = 2,
        Failure = 3,
        Passive = 4,
        RquiresConfirmation = 5
    }
}