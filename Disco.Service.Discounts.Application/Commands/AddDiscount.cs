using MediatR;

namespace Disco.Service.Discounts.Application.Commands;

public record AddDiscount : IRequest<Unit>
{
    public AddDiscount(Guid comapnyId, int percent, int points, string name, DateTime statedDate, DateTime endingDate)
    {
        ComapnyId = comapnyId;
        Percent = percent;
        Points = points;
        Name = name;
        StatedDate = statedDate;
        EndingDate = endingDate;
    }

    public Guid ComapnyId { get; }
    public int Percent { get; }
    public int Points { get; }
    public string Name { get; }
    public DateTime StatedDate { get; }
    public DateTime EndingDate { get; }
}