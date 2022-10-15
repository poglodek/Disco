using System;
using System.Linq;
using Disco.Service.Users.Core.Entities;
using Disco.Service.Users.Core.Events;
using Disco.Service.Users.Core.Exceptions;
using Shouldly;
using Xunit;

namespace Disco.Service.Users.Unit.Domain;

public class UserTest
{
    private User Act(AggregateId id, string email, string password, bool verified, DateTime createdDate,
        bool isDeleted = false)
    {
        return User.Create(id, email, verified, createdDate, isDeleted);
    }


    [Fact]
    public void CreateUserWithValidDate_ReturnsUser()
    {
        var id = new AggregateId();
        var dt = DateTime.Now;
        var user = Act(id, "test@email.com", "secretP4ssword", false, dt);

        user.ShouldNotBeNull();
        user.Id.Value.ShouldNotBe(Guid.Empty);
        user.Verified.ShouldBeFalse();
        user.Email.ShouldBe("test@email.com");
        user.CreatedDate.ShouldBe(dt);

        var @event = user.Events.Single();
        @event.ShouldBeOfType<UserCreated>();
        ((UserCreated) @event).User.ShouldBe(user);
    }

    [Fact]
    public void Clearing_Events_AfterUserCreatedEvent()
    {
        var id = new AggregateId();
        var dt = DateTime.Now;
        var user = Act(id, "test@email.com", "secretP4ssword", false, dt);

        user.Events.Count().ShouldBe(1);
        user.ClearEvents();
        user.Events.Count().ShouldBe(0);
    }

    [Fact]
    public void CreateUserWithNotValidEmail_Should_ThrowAnException()
    {
        var id = new AggregateId();

        var exception = Record.Exception(() => Act(id, "testemail.com", "secretP4ssword", false, DateTime.Now));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidUserEmailException>();
        ((InvalidUserEmailException) exception).Code.ShouldBe("invalid_user_email");
    }

    [Fact]
    public void CreateUserWithNotValidEmail_LenghtToShort_Should_ThrowAnException()
    {
        var id = new AggregateId();

        var exception = Record.Exception(() => Act(id, "2@a.pl", "secretP4ssword", false, DateTime.Now));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidUserEmailException>();
        ((InvalidUserEmailException) exception).Code.ShouldBe("invalid_user_email");
    }

    [Fact]
    public void CreateUserWithNotValidEmail_LenghtToLong_Should_ThrowAnException()
    {
        var id = new AggregateId();

        var exception = Record.Exception(() => Act(id,
            "veryFancyLongAndSuperEmail@SuperHostNameWith.edu.com.pl.org.gov.ing", "secretP4ssword", false,
            DateTime.Now));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidUserEmailException>();
        ((InvalidUserEmailException) exception).Code.ShouldBe("invalid_user_email");
    }

    [Fact]
    public void CreateUserWithNotValidPassword_LenghtToLong_Should_ThrowAnException()
    {
        var id = new AggregateId();

        var exception = Record.Exception(() => Act(id, "test@email.com",
            "password99password99password99password99password99222!", false, DateTime.Now));

        exception.ShouldBeNull();
    }

    [Fact]
    public void CreateUserWithNotValidPassword_LenghtToShort_Should_ThrowAnException()
    {
        var id = new AggregateId();

        var exception = Record.Exception(() => Act(id, "test@email.com", "pass", false, DateTime.Now));

        exception.ShouldBeNull();
        
    }

    [Fact]
    public void Delete_User_Return_NewIEvent()
    {
        var user = ReturnNewUser();

        user.IsDeleted.ShouldBeFalse();

        user.SoftDeleteUser();

        user.IsDeleted.ShouldBeTrue();
        var @event = user.Events.Single();

        @event.ShouldBeOfType<UserDeleted>();
        ((UserDeleted) @event).User.ShouldBe(user);
    }

    [Fact]
    public void Delete_User_Who_Is_Already_Deleted()
    {
        var user = ReturnNewUser();

        user.IsDeleted.ShouldBeFalse();

        user.SoftDeleteUser();

        user.IsDeleted.ShouldBeTrue();
        var @event = user.Events.Single();

        @event.ShouldBeOfType<UserDeleted>();
        ((UserDeleted) @event).User.ShouldBe(user);

        var ex = Record.Exception(() => user.SoftDeleteUser());

        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<UserAlreadyDeletedException>();

        ((UserAlreadyDeletedException) ex).Code.ShouldBe("user_already_deleted");
    }

    [Fact]
    public void Verify_User_Return_NewIEvent()
    {
        var user = ReturnNewUser();

        user.Verified.ShouldBeFalse();

        user.VerifyUser();

        user.Verified.ShouldBeTrue();
        var @event = user.Events.Single();

        @event.ShouldBeOfType<UserVerified>();
        ((UserVerified) @event).User.ShouldBe(user);
    }

    [Fact]
    public void Verify_User_Who_Is_Already_Verified()
    {
        var user = ReturnNewUser();

        user.Verified.ShouldBeFalse();

        user.VerifyUser();

        user.Verified.ShouldBeTrue();
        var @event = user.Events.Single();

        @event.ShouldBeOfType<UserVerified>();
        ((UserVerified) @event).User.ShouldBe(user);

        var ex = Record.Exception(() => user.VerifyUser());

        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<UserAlreadyVerifiedException>();

        ((UserAlreadyVerifiedException) ex).Code.ShouldBe("user_already_verified");
    }

    [Fact]
    public void User_SetPassword_ValidHashAndPassword_ShouldNotThrowAnException()
    {
        var user = ReturnNewUser();

        var password = "Super6Fancy9Pa55w0rd!>";
        var passwordHash = "AQAAAAEAACcQAAAAEH7stu/BhwMIwsRL3vW+6iFlCXW5LqU4cYJl5evsjf8QtAk6IVtMpcV2EChfcmKVUA==";
        
        var ex = Record.Exception(()=> user.SetNewPasswordHash(passwordHash,password));
        
        ex.ShouldBeNull();
    }
    [Fact]
    public void User_SetPassword_ValidHash_ShouldNotThrowAnException()
    {
        var user = ReturnNewUser();
        
        var passwordHash = "AQAAAAEAACcQAAAAEH7stu/BhwMIwsRL3vW+6iFlCXW5LqU4cYJl5evsjf8QtAk6IVtMpcV2EChfcmKVUA==";
        
        var ex = Record.Exception(()=> user.SetNewPasswordHash(passwordHash));
        
        ex.ShouldBeNull();
    }
    [Fact]
    public void User_SetPassword_NullHash_ShouldNotThrowAnException()
    {
        var user = ReturnNewUser();

        string? passwordHash = null;
        
        var ex = Record.Exception(()=> user.SetNewPasswordHash(passwordHash));
        
        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<InvalidUserPasswordException>();
        ((InvalidUserPasswordException) ex).Code.ShouldBe("invalid_user_password");
    }
    [Fact]
    public void User_SetPassword_InvalidHash_ShouldNotThrowAnException()
    {
        var user = ReturnNewUser();

        string? passwordHash = "null";
        
        var ex = Record.Exception(()=> user.SetNewPasswordHash(passwordHash));
        
        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<InvalidUserPasswordException>();
        ((InvalidUserPasswordException) ex).Code.ShouldBe("invalid_user_password");
    }
    
    
    private User ReturnNewUser()
    {
        return new User(new AggregateId(), "test@email.com",  false, DateTime.Now);
    }
}