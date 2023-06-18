using CRUD.Data;
using CRUD.Models;

namespace CRUD
{
    public class DuomenuGeneratorius
    {
        public static void GeneruotiDuomenis(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var duomenuKontekstas = serviceScope.ServiceProvider.GetService<DuomenuKontekstas>();
                if (!duomenuKontekstas.Darbuotojai.Any())
                {
                    // Pirminiai daktarų duomenys
                    duomenuKontekstas.Darbuotojai.AddRange(new Darbuotojas()
                    {
                        Vardas = "Andrius",
                        Pavarde = "Lukas",
                        GimimoData = new DateTime(1999, 2, 7),
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
                        Pavarde = "Andrius",
                        GimimoData = new DateTime(1999, 12, 11),
                        Adresas = "Petru g. Kaunas",
                        Pareigos = new List<Pareiga>()
                        {
                        new Pareiga {Pareigos = "Slaugytojas"}
                        },
                        Statusas = 1
                    }
                    );
                    duomenuKontekstas.SaveChanges();
                }
                if (!duomenuKontekstas.Pacientai.Any())
                {
                    // Pirminiai pacientų duomenys
                    duomenuKontekstas.Pacientai.AddRange(new Pacientas()
                    {
                        Vardas = "Lukas",
                        Pavarde = "Andrius",
                        GimimoData = new DateTime(2000, 1, 1)
                    });
                    duomenuKontekstas.SaveChanges();
                }
            }
        }
    }
}