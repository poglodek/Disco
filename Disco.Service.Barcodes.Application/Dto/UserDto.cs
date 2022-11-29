using Disco.Shared.Rabbit.Messages.Consumer;
using MediatR;

namespace Disco.Service.Barcodes.Application.Dto;

public record UserDto(Guid Id, string Email,string Nick,bool IsVerified, DateTime CreatedDate) : INotification;