namespace TABP.Domain.Exceptions;

public class UserDuplicateException : CustomException
{
    public UserDuplicateException(Guid userId)
        : base($"User with id {userId} already exists")
    {
    }
}