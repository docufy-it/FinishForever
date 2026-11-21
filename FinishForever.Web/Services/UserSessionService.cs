namespace FinishForever.Web.Services;

public class UserSessionService
{
    private Guid? _currentUserId;

    public Guid? GetCurrentUserId()
    {
        return _currentUserId;
    }

    public void SignIn(Guid userId)
    {
        _currentUserId = userId;
    }

    public void SignOut()
    {
        _currentUserId = null;
    }
}