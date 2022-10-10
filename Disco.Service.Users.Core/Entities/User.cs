using Disco.Service.Users.Core.Events;
using Disco.Service.Users.Core.Exceptions;

namespace Disco.Service.Users.Core.Entities;

public sealed class User : AggregateRoot
{
    public string Email { get; private set; }

    public string Password { get; private set; }
    public bool Verified { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedDate { get; private set; }


    public User(AggregateId id, string email, string password, bool verified, DateTime createdDate, bool isDeleted = false)
    {
        ValidateEmail(email);
        ValidatePassword(password);
        Id = id;
        Email = email;
        Password = password;
        Verified = verified;
        CreatedDate = createdDate;
        IsDeleted = isDeleted;
    }

    private void ValidatePassword(string password)
    {
        if (password.Length < 7 || password.Length > 40)
            throw new InvalidUserPasswordException(password);
        
    }

    private void ValidateEmail(string email)
    {
        if (email.Length < 7 || email.Length > 30)
            throw new InvalidUserEmailException(email);
        
        if(!email.Contains("@"))
            throw new InvalidUserEmailException(email);
    }

    public static User Create(AggregateId id, string email, string password, bool verify, DateTime createdDate, bool isDeleted = false)
    {
        var user = new User(id, email, password, verify,createdDate,isDeleted);
        user.AddEvent(new UserCreated(user));
        return user;
    }

    public void VerifyUser()
    {
        if (Verified)
            throw new UserAlreadyVerifiedException(Id.Value);

        Verified = true;
        
        AddEvent(new UserVerified(this));
    }
    
    public void SoftDeleteUser()
    {
        if (IsDeleted)
            throw new UserAlreadyDeletedException(Id.Value);

        IsDeleted = true;
        
       AddEvent(new UserDeleted(this));
    }
}