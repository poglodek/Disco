using Disco.Service.Users.Infrastructure.Exceptions;

namespace Disco.Service.Points.Infrastructure.Exceptions;

public class PointsNotFoundExceptions : InfrastructureException
{
    public PointsNotFoundExceptions(Guid id) : base($"Points for user id {id} wasn't found")
    {
    }

    public override string Code => "points_not_found";
}