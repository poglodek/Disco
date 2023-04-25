using Disco.Service.Discounts.Core.Exceptions;
using Disco.Service.Discounts.Core.ValueObjects;
using Disco.Service.Points.Core.Events;

namespace Disco.Service.Discounts.Core.Entities;

public class Discount : AggregateRoot
{
    public CompanyId Company { get; private set; }
    public Percent Percent { get; private set; }
    public ValueObjects.Points Points { get; private set; }
    public StartedDate StartedDate { get; private set; }
    public EndingDate EndingDate { get; private set; }
    
    public Name Name { get; private set; }

    public Discount(Guid id,Guid company, int percent, int points ,DateOnly startedDate, DateOnly endingDate, string name)
    {
        if (company == Guid.Empty)
        {
            throw new InvalidCompanyIdException(company);
        }

        if (percent is < 0 or > 100)
        {
            throw new InvalidPercentValueException(percent);
        }

        if (points < 0)
        {
            throw new InvalidPointsValueException(points);
        }

        if (startedDate > endingDate)
        {
            throw new InvalidDatesException(startedDate, endingDate);
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidNameException(name);
        }
        
        Id = new AggregateId(id);
        Company = new CompanyId(company);
        Percent = new Percent(percent);
        StartedDate = new StartedDate(startedDate);
        EndingDate = new EndingDate(endingDate);
        Points = new ValueObjects.Points(points);
        Name = new Name(name);

    }
    
    public static Discount Create(Guid id,Guid company, int percent, int points ,DateOnly startedDate, DateOnly endingDate, string name)
    {
        var point = new Discount(id,company, percent,points,startedDate,endingDate, name);
        
        point.AddEvent(new DiscountCreated(id));
        
        return point;
    }

    public void SetPoints(int points)
    {
        if (points < 0)
        {
            throw new InvalidPointsValueException(points);
        }

        Points = new ValueObjects.Points(points);
    }
    
    public void SetPercent(int percent)
    {
        if (percent is < 0 or > 100)
        {
            throw new InvalidPercentValueException(percent);
        }

        Percent = new Percent(percent);
    }

    public void SetStartedDate(DateOnly date)
    {
        if (date > EndingDate.Value)
        {
            throw new InvalidDatesException(date, EndingDate.Value);
        }

        StartedDate = new StartedDate(date);
    }
    
    public void SetEndingDate(DateOnly date)
    {
        if (StartedDate.Value > date)
        {
            throw new InvalidDatesException(StartedDate.Value, date);
        }

        EndingDate = new EndingDate(date);
    }
    
}