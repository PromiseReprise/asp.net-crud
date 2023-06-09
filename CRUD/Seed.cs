using CRUD.Data;
using CRUD.Models;

namespace CRUD
{
    public class Seed
    {
        public static void SeedDataContext(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var dataContext = serviceScope.ServiceProvider.GetService<DataContext>();
                dataContext.Database.EnsureCreated();

                if (!dataContext.Darbuotojai.Any())
                {
                    //Seed Darbuotoju data
                    dataContext.Darbuotojai.AddRange(new Darbuotojas()
                    {
                        Vardas = "Andrius",
                        Pavarde = "Maslovas",
                        GimimoData = new DateTime(1994, 7, 2),
                        Adresas = "Sodu g. Vilnius",
                        Pareigos = new List<Pareiga>()
                    {
                        new Pareiga {Pareigos = "Daktaras"},
                        new Pareiga {Pareigos = "Vyr. Daktaras"}
                    },
                        Statusas = 1
                    },
                    new Darbuotojas()
                    {
                        Vardas = "Lukas",
                        Pavarde = "Lovas",
                        GimimoData = new DateTime(1996, 11, 12),
                        Adresas = "Petru g. Kaunas",
                        Pareigos = new List<Pareiga>()
                        {
                        new Pareiga {Pareigos = "Slaugytojas"}
                        },
                        Statusas = 1
                    }
                    );
                    dataContext.SaveChanges();
                }
            }
        }

    }
}
