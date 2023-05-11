using Disco.Service.Companies.Application.Dtos;
using MediatR;

namespace Disco.Service.Companies.Application.Commands;

public class CreateCompany : IRequest<CompanyDto>
{
    public string Name { get; }
    public string Address { get; }

    public CreateCompany(string name, string address)
    {
        Name = name;
        Address = address;
    }
}