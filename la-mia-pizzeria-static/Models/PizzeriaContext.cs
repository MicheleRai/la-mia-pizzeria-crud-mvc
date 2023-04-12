using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace la_mia_pizzeria_static.Models
{
    public class PizzeriaContext : DbContext
    {
        public DbSet<Pizza> Pizze { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=PizzeriaDb;Integrated Security=True;Pooling=False;TrustServerCertificate=True");
        }

        public void Seed()
        {
            if (!Pizze.Any())
            {
                var pizze = new Pizza[]
                {
                    new Pizza{
                        Foto = "https://picsum.photos/200/300",
                        Name= "Pizza margherita",
                        Description= "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Mollitia quis est adipisci incidunt rem nostrum ipsam fuga ratione tempora eveniet!",
                        Prezzo= 3.50

                    },
                    new Pizza {
                        Foto= "https://picsum.photos/200/300",
                        Name= "Pizza capricciosa",
                        Description= "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Mollitia quis est adipisci incidunt rem nostrum ipsam fuga ratione tempora eveniet!",
                        Prezzo= 4

                    },
                    new Pizza {
                        Foto= "https://picsum.photos/200/300",
                        Name= "Pizza 4 stagioni",
                        Description= "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Mollitia quis est adipisci incidunt rem nostrum ipsam fuga ratione tempora eveniet!",
                        Prezzo= 4.50
                    },
                    new Pizza {
                        Foto= "https://picsum.photos/200/300",
                        Name= "Pizza diavola",
                        Description= "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Mollitia quis est adipisci incidunt rem nostrum ipsam fuga ratione tempora eveniet!",
                        Prezzo= 3.50
                    }
                };

                Pizze.AddRange(pizze);

                SaveChanges();
            }
        }

    }
}
