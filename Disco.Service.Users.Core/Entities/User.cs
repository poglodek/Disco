using Disco.Service.Users.Core.Events;
using Disco.Service.Users.Core.Exceptions;
using Disco.Shared.Mongo;

namespace Disco.Service.Users.Core.Entities;

public sealed class User : AggregateRoot
{
    public string Email { get; private set; }

    public string PasswordHash { get; private set; }
    public bool Verified { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedDate { get; private set; }


    public User(AggregateId id, string email, bool verified, DateTime createdDate, bool isDeleted = false)
    {
        ValidateEmail(email);
       
        Id = id;
        Email = email;
        Verified = verified;
        CreatedDate = createdDate;
        IsDeleted = isDeleted;
    }

    public void SetNewPasswordHash(string passwordHash, string? password = null)
    {
        if (password is not null)
        {
            ValidatePassword(password);
        }
        
        ValidatePassword(passwordHash);
        
        PasswordHash = passwordHash;
    }
    private void ValidatePassword(string password)
    {
        if (password is null)
            throw new InvalidUserPasswordException(nameof(password));
        
        if(password.Length < 7)
            throw new InvalidUserPasswordException(password);
        
    }

    private void ValidateEmail(string email)
    {
        if (email.Length is < 7 or > 30)
            throw new InvalidUserEmailException(email);
        
        if(!email.Contains("@"))
            throw new InvalidUserEmailException(email);
    }

    public static User Create(AggregateId id, string email, bool verify, DateTime createdDate, bool isDeleted = false)
    {
        var user = new User(id, email,  verify,createdDate,isDeleted);
        user.AddEvent(new UserCreated(user));
        return user;
    }

    public void VerifyUser()
    {
        if (Verified)
            throw new UserAlreadyVerifiedException(Id.Value);

        Verified = true;
        Version++;
        AddEvent(new UserVerified(this));
    }
    
    public void SoftDeleteUser()
    {
        if (IsDeleted)
            throw new UserAlreadyDeletedException(Id.Value);

        IsDeleted = true;
        Version++;
       AddEvent(new UserDeleted(this));
    }
    
}