using RedisSimple.Entities;
using ServiceStack.Redis;
using System;

namespace RedisSimple
{
    class Program
    {
        static void Main(string[] args)
        {
            var customer = new Customer()
            {
                Id = Guid.NewGuid(),
                Name = "Paul",
                Address = "Street blue bird, 457",
                Phone = "1234567489"
            };

            // Create
            SaveCustomerOnRedis(customer);

            // Get
            var customer2 = GetCustomerFromRedis(customer.Id.ToString());

            // Update
            customer2.Name = "Joe";
            SaveCustomerOnRedis(customer2);

            // Delete
            DeleteCustomerOnRedis(customer2);
        }

        static void SaveCustomerOnRedis(Customer customer)
        {
            CreateRedisClient().Set<Customer>(customer.Id.ToString(), customer);
            Console.WriteLine($"Customer {customer.Name} saved on redis");
        }

        static Customer GetCustomerFromRedis(string customerId)
        {
            var customer = CreateRedisClient().Get<Customer>(customerId);
            Console.WriteLine($"Customer {customer.Name} get from redis");
            return customer;
        }

        static void DeleteCustomerOnRedis(Customer customer)
        {
            CreateRedisClient().Expire(customer.Id.ToString(), 0);
            Console.WriteLine($"Customer {customer.Name} deleted on redis");
        }

        private static RedisClient CreateRedisClient()
        {
            var host = "localhost:6379";
            return new RedisClient(host);
        }
    }
}
