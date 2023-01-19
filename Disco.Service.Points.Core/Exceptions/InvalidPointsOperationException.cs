namespace Disco.Service.Points.Core.Exceptions;

public class InvalidPointsOperationException : DomainException
{
    public InvalidPointsOperationException(Guid id) : base($"Operation is wrong {id}")
    {
    }

    public override string Code => "wrong_operation";
}