using platformService.Models;

namespace platformService.Data
{

    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if (!context.Platforms.Any())
            {
                Console.WriteLine("-----> Seeding data...");
                context.AddRange(
                    new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Sql Server Express", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Kubernetees", Publisher = "Cloud native Computing foundation", Cost = "Free" });
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("-----> We already have data");
            }
        }

    }
}