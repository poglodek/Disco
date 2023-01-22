namespace Disco.Shared.Consul.Models;

public class ServiceCheck
{
    private readonly string _address;
    private readonly int _port;

    public ServiceCheck(string address, int port)
    {
        _address = address;
        _port = port;
    }

    public string DeregisterCriticalServiceAfter => "10s";
    public string Http => $"{_address}:{_port}/ping/";
    public string Interval => "10s";
    public string Method => "GET";
    public string Timeout => "5s";
}