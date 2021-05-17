using System;
using WebTutorialsApp.Domain.Models;
using WebTutorialsApp.Persistence.Data;
using WebTutorialsApp.Domain.ValueObjects;

namespace CRUDTestsAndDataBaseCreation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Database Creation");
            WebTutorialsAppDbContext context = new();

            using (context)
            {
                WebTutorialsAppDbInitializer.Initialize(context);
            }

            var firstUser = new UserModel(
                new Name("Admin", "Administrator"),
                new Email("admin@mail.com"),
                new Password("admin12345"),
                new Role("Manager")                
                );

            if (firstUser.IsModelValid())
            {
                using (context)
                {
                    context.Users.Add(firstUser.ToEntity());
                    context.SaveChanges();
                }                
            }

        }
    }
}
