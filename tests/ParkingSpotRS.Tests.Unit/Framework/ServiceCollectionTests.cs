using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace ParkingSpotRS.Tests.Unit.Framework;

public class ServiceCollectionTests
{
    [Fact]
    public void test()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IUser, Admin>();
        serviceCollection.AddSingleton<IUser, Employee>();
        serviceCollection.AddSingleton<IUser, Manager>();
        serviceCollection.AddSingleton(typeof(IMessenger<>), typeof(Messenger<>));

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var users = serviceProvider.GetServices<IUser>();
        users.Count().ShouldBe(3);
    }

    public interface IMessenger<T>
    {
        void Send(T message);
    }

    public class Messenger<T> : IMessenger<T>
    {
        private readonly Guid _id = Guid.NewGuid();

        public void Send(T message)
        {
            Console.WriteLine($"Sending a message... [ID {_id}]");
        }
    }

    public interface IUser
    {
    }

    public class Admin : IUser
    {
        private readonly IMessenger<string> _messenger;

        public Admin(IMessenger<string> messenger)
        {
            _messenger = messenger;
        }
    }

    public class Employee : IUser
    {
    }

    public class Manager : IUser
    {
    }
}