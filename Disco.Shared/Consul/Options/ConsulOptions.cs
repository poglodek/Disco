namespace Disco.Shared.Consul.Options;

public class ConsulOptions
{
    public string Url { get; set; }
    public string Name { get; set; }
    public string ServiceAddress { get; set; }
    public List<string> Tags { get; set; }
    public int Port { get; set; }
    
}