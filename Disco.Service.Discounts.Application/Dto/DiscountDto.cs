namespace Disco.Service.Discounts.Application.Dto;

public record DiscountDto(Guid Id, Guid CompanyId, int Percent, int Points, DateTime StartingDate, DateTime EndingDate, string Name);