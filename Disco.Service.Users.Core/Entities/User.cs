using Disco.Service.Users.Core.Events;
using Disco.Service.Users.Core.Exceptions;
using Disco.Service.Users.Core.ValueObjects;
using Disco.Shared.Mongo;

namespace Disco.Service.Users.Core.Entities;

public sealed class User : AggregateRoot
{
    public Email Email { get; private set; }

    public PasswordHash PasswordHash { get; private set; }
    public Nick Nick { get; private set; }
    public Verified Verified { get; private set; }
    public IsDeleted IsDeleted { get; private set; }
    public CreatedDate CreatedDate { get; private set; }


    public User(AggregateId id, string email,string nick ,bool verified, DateTime createdDate, bool isDeleted = false)
    {
        Id = id;
        Nick = nick;
        Email = email;
        Verified = verified;
        CreatedDate = createdDate;
        IsDeleted = isDeleted;
    }
    
    public void SetNewPasswordHash(string passwordHash, string? password = null)
    {
        if (password is not null)
        {
            PasswordHash.ValidatePassword(password);
        }
        
        PasswordHash.ValidatePassword(passwordHash);
        
        PasswordHash = passwordHash;
    }
    

    
    public static User Create(AggregateId id, string email,string nick ,bool verify, DateTime createdDate, bool isDeleted = false)
    {
        var user = new User(id, email,  nick,verify,createdDate,isDeleted);
        user.AddEvent(new UserCreated(user.Id.Value));
        return user;
    }

    public void VerifyUser()
    {
        if (Verified)
            throw new UserAlreadyVerifiedException(Id.Value);

        Verified = true;
        Version++;
        
        AddEvent(new UserVerified(Id.Value));
    }
    
    public void SoftDeleteUser()
    {
        if (IsDeleted)
            throw new UserAlreadyDeletedException(Id.Value);

        IsDeleted = true;
        Version++;
       AddEvent(new UserDeleted(Id.Value));
    }
    
}