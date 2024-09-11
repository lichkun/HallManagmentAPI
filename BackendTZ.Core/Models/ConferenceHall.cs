using System;
using System.Collections.Generic;

namespace BackendTZ.Core.Models
{
    public class ConferenceHall
    {
        public const int MAX_NAME_LENGTH = 250;
        public const int MAX_HALL_CAPACITY = 1250;
        public ConferenceHall()
        {
        }
        public ConferenceHall(Guid id, string name, int capacity, decimal baserate, List<Guid> services) 
        { 
            Id = id;
            Name = name;
            Capacity = capacity;
            BaseRate = baserate;
            ServicesIds = services;
        }
        public Guid Id { get;  }
        public string Name { get;  } = string.Empty;
        public int Capacity { get;  } = 0;
        public decimal BaseRate { get;  } 
        public virtual List<Guid> ServicesIds { get; }
        public static (ConferenceHall hall, string Error) Create(Guid id, string name, int capacity, decimal baserate, List<Guid> services)
        {
            var error = string.Empty;
            if (string.IsNullOrEmpty(name) || name.Length > MAX_NAME_LENGTH)
            {
                error = "Name cannot be empty or longer then 250 symbols";
            }
            if( capacity > MAX_HALL_CAPACITY)
            {
                error += "Capacity cannot be bigger then 1250 places";
            }
            var hall = new ConferenceHall(id, name, capacity, baserate, services);
            return (hall, error);
        }

    }
}
