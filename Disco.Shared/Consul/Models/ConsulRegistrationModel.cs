namespace Disco.Shared.Consul.Models;

public class ConsulRegistrationModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Address { get; init; }
    public int Port { get; init; }
    public List<string> Tags { get; init; }
    public ServiceCheck Check => new (Address,Port);



}