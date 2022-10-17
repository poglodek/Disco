namespace Disco.Service.Users.Application.Dto;

public record UserDto(Guid Id, string Email,string Nick,bool IsVerified, DateTime CreatedDate);