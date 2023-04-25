namespace Disco.Service.Discounts.Infrastructure.Exceptions;

public class PointsNotFoundException : InfrastructureException
{
    public PointsNotFoundException(Guid id) : base($"Points not found with id:{id}")
    {
    }

    public override string Code => "points_not_found";
}