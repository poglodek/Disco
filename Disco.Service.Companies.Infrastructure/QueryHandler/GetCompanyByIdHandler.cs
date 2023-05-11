using Disco.Service.Companies.Application.Dtos;
using Disco.Service.Companies.Application.Query;
using Disco.Service.Companies.Infrastructure.Exceptions;
using Disco.Service.Core.Repositories;
using MediatR;

namespace Disco.Service.Companies.Infrastructure.QueryHandler;

public class GetCompanyByIdHandler : IRequestHandler<GetCompanyById,CompanyDto>
{
    private readonly ICompanyRepository _companyRepository;

    public GetCompanyByIdHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }
    
    public async Task<CompanyDto> Handle(GetCompanyById request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetAsync(request.Id);
        
        if (company is null)
        {
            throw new CompanyNotFoundException(request.Id);
        }
        
        return  new CompanyDto(company.Id.Value, company.Name.Value, company.Address.Value);
    }
}