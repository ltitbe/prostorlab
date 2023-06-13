using Microsoft.EntityFrameworkCore;

namespace prostorlab.Models
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

                // Look for any movies.
                if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }

                context.Users.AddRange(
                    new User
                    {
                        FirstName = "Harry",
                        LastName = "Smith"
                    }, 
                    new User
                    {
                        FirstName = "John",
                        LastName = "Ivanov"
                    },
                    new User
                    {
                        FirstName = "Jack",
                        LastName = "Petrov"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
