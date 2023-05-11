namespace Disco.Service.Companies.Application.Dtos;

public class CompanyDto
{
    public Guid Id { get; }
    public string Name { get; }
    public string Address { get; }

    public CompanyDto(Guid id, string name, string address)
    {
        Id = id;
        Name = name;
        Address = address;
    }
}