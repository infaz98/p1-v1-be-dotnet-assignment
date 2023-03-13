using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.OrderAggregate
{
    public class Passenger : Entity
    {
        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Email { get; private set; }

        public Passenger() { }

        public Passenger(string name, int age, string email)
        {
            Name = name;
            Age = age; 
            Email = email;
        }
    }
}
