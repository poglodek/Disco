using Disco.Service.Companies.Application.Dtos;
using Disco.Service.Core.Entities;
using Disco.Service.Core.Repositories;
using MediatR;

namespace Disco.Service.Companies.Application.Commands.Handlers;

public class CreateCompanyHandler : IRequestHandler<CreateCompany,CompanyDto>
{
    private readonly ICompanyRepository _companyRepository;

    public CreateCompanyHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }
    
    public async Task<CompanyDto> Handle(CreateCompany request, CancellationToken cancellationToken)
    {
        var company =  Company.Create(Guid.NewGuid(), request.Name, request.Address);
        await _companyRepository.AddAsync(company);
        
        return new CompanyDto(company.Id.Value, company.Name.Value, company.Address.Value);
    }
}