using Domain.Exceptions;
using Domain.SeedWork;

namespace Domain.Aggregates.AirportAggregate
{
    public class Airport : Entity, IAggregateRoot
    {
        public string Code { get; private set; }

        public string Name { get; private set; }

        public Airport() { }
        
        public Airport(string code, string name) : this()
        {
            if (code.Length != 3)
            {
                throw new AirportDomainException("The Airport code must be three characters.");
            }
            
            Code = code;
            Name = name;
        }
    }
}