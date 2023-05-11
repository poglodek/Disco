using Disco.Service.Companies.Application.Dtos;
using MediatR;

namespace Disco.Service.Companies.Application.Query;

public class GetCompanyById : IRequest<CompanyDto>
{
    public Guid Id { get; }

    public GetCompanyById(Guid id)
    {
        Id = id;
    }
}