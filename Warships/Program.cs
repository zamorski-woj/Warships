using static Warships.WarshipsGame;

namespace Warships
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            app.MapFallbackToFile("index.html");

            RunConsoleVersion();
            app.Run();
        }

        private static void RunConsoleVersion()
        {
            Console.WriteLine("What to call the first player?");
            string name1 = Console.ReadLine() ?? "Ziutek";
            Console.WriteLine("What to call the second player?");
            string name2 = Console.ReadLine() ?? "Ewaryst";
            Tuple<Player, Player> bothPlayers = CreateTwoPlayers(name1, name2, 10);
            Player p1 = bothPlayers.Item1;
            Player p2 = bothPlayers.Item2;
            p1.GenerateFleet();
            p2.GenerateFleet();

            while (p1.FleetStillAlive() && p2.FleetStillAlive())
            {
                p1.PlayOneTurn(p2);
                p2.PlayOneTurn(p1);
            }

        }
    }
}