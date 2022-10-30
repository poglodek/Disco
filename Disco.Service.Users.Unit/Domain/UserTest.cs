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
    private User Act(AggregateId id, string email, string password, string nick,bool verified, DateTime createdDate,
        bool isDeleted = false)
    {
        return User.Create(id, email, nick,verified, createdDate, isDeleted);
    }


    [Fact]
    public void CreateUserWithValidDate_ReturnsUser()
    {
        var id = new AggregateId();
        var dt = DateTime.Now;
        var user = Act(id, "test@email.com", "secretP4ssword", "Paul",false, dt);

        user.ShouldNotBeNull();
        user.Id.Value.ShouldNotBe(Guid.Empty);
        user.Verified.ShouldBeFalse();
        user.Email.ShouldBe("test@email.com");
        user.CreatedDate.ShouldBe(dt);
        user.Nick.ShouldBe("Paul");

        var @event = user.Events.Single();
        @event.ShouldBeOfType<UserCreated>();
        ((UserCreated) @event).UserId.ShouldBe(id.Value);
    }

    [Fact]
    public void SetUserPasswordHash_WithValidPassword()
    {
        var hash = "AAAAAAAAAAAAAAADSA12sadasd214dsaf";
        var user = ReturnNewUser();
        user.SetNewPasswordHash(hash);
        
        user.PasswordHash.ShouldBe(hash);
    }

    [Theory]
    [InlineData("xdddd")]
    [InlineData("12345678900987654321@super.duper.emial.com.pl.org")]
    [InlineData("google.test.mail.com")]
    public void SetUserEmail_WithInValid_ShouldThrowAnException(string mail)
    {
        var ex =  Record.Exception(() => ReturnNewUser(mail));
        ex.ShouldNotBeNull();
        ex.ShouldBeOfType<InvalidUserEmailException>();
        ((InvalidUserEmailException) ex).Code.ShouldBe("invalid_user_email");
    }
    
    
    [Fact]
    public void Clearing_Events_AfterUserCreatedEvent()
    {
        var id = new AggregateId();
        var dt = DateTime.Now;
        var user = Act(id, "test@email.com", "secretP4ssword", "Paul",false, dt);

        user.Events.Count().ShouldBe(1);
        user.ClearEvents();
        user.Events.Count().ShouldBe(0);
    }

    [Fact]
    public void CreateUserWithNotValidEmail_Should_ThrowAnException()
    {
        var id = new AggregateId();

        var exception = Record.Exception(() => Act(id, "testemail.com", "secretP4ssword", "Paul",false, DateTime.Now));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidUserEmailException>();
        ((InvalidUserEmailException) exception).Code.ShouldBe("invalid_user_email");
    }

    [Fact]
    public void CreateUserWithNotValidEmail_LenghtToShort_Should_ThrowAnException()
    {
        var id = new AggregateId();

        var exception = Record.Exception(() => Act(id, "2@a.pl", "secretP4ssword", "Paul",false, DateTime.Now));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidUserEmailException>();
        ((InvalidUserEmailException) exception).Code.ShouldBe("invalid_user_email");
    }

    [Fact]
    public void CreateUserWithNotValidEmail_LenghtToLong_Should_ThrowAnException()
    {
        var id = new AggregateId();

        var exception = Record.Exception(() => Act(id,
            "veryFancyLongAndSuperEmail@SuperHostNameWith.edu.com.pl.org.gov.ing", "secretP4ssword", "Paul",false,
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
            "password99password99password99password99password99222!", "Paul",false, DateTime.Now));

        exception.ShouldBeNull();
    }

    [Fact]
    public void CreateUserWithNotValidPassword_LenghtToShort_Should_ThrowAnException()
    {
        var id = new AggregateId();

        var exception = Record.Exception(() => Act(id, "test@email.com", "pass", "Paul",false, DateTime.Now));

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
        ((UserDeleted) @event).UserId.ShouldBe(user.Id.Value);
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
        ((UserDeleted) @event).UserId.ShouldBe(user.Id.Value);

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
        ((UserVerified) @event).UserId.ShouldBe(user.Id.Value);
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
        ((UserVerified) @event).UserId.ShouldBe(user.Id.Value);

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
    
    
    private User ReturnNewUser(string email = "test@email.com")
    {
        return new User(new AggregateId(),email ,  "Paul",false, DateTime.Now);
    }
}