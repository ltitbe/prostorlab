using Microsoft.EntityFrameworkCore;

namespace usersapp.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationContext>>()))
            {
                if (context == null || context.Users == null)
                {
                    throw new ArgumentNullException("Null ApplicationContext");
                }

                // Look for any users
                if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }

                context.Users.AddRange(
                    new User
                    {
                        Id = 1,
                        FirstName = "Harry",
                        LastName = "Smith"
                    }, 
                    new User
                    {
                        Id = 2,
                        FirstName = "John",
                        LastName = "Ivanov"
                    },
                    new User
                    {
                        Id = 3,
                        FirstName = "Jack",
                        LastName = "Petrov"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
