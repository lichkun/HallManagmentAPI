using System.Collections.Generic;

namespace BackendTZ.Core.Models
{
    public class Service
    {
        public const int MAX_NAME_LENGTH = 250;
        public Service() { }
        public Service(Guid id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public Guid Id { get;  }
        public string Name { get;  } = string.Empty;
        public decimal Price { get;}
        public static (Service Service, string Error) Create(Guid id, string name, decimal price)
        {
            var error = string.Empty;
            if(string.IsNullOrEmpty(name) || name.Length > MAX_NAME_LENGTH)
            {
                error = "Name cannot be empty or longer then 250 symbols";
            }
            var service = new Service(id, name, price);
            return (service, error);
        }
    }
}
