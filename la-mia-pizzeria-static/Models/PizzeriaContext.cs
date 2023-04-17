using Azure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace la_mia_pizzeria_static.Models
{
    public class PizzeriaContext : IdentityDbContext<IdentityUser>
    {
		public PizzeriaContext(DbContextOptions<PizzeriaContext> options) : base(options) { }
		public DbSet<Pizza> Pizze { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Ingrediente> Ingredienti { get; set; }


		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//    optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=PizzeriaDb;Integrated Security=True;Pooling=False;TrustServerCertificate=True");
		//}

		public void Seed()
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
			if (!Pizze.Any())
			{
				Pizze.AddRange(pizze);
			}

			if (!Categories.Any())
			{
				var seed = new Category[]
				{
					new()
					{
						Name = "Bianca",
					},
					new()
					{
						Name = "Rossa",
					},
					new()
					{
						Name = "Vegana",
					},
					new()
					{
						Name = "Generic",
						Pizze = pizze
					}
				};

				Categories.AddRange(seed);
			}
			if (!Ingredienti.Any())
			{
				var seed = new Ingrediente[]
				{
					new()
					{
						Name = "pomodoro"
					},
					new()
					{
						Name = "farina",
						Pizze = pizze
					},
					new()
					{
						Name = "mozzarella"
					}
				};

				Ingredienti.AddRange(seed);
			}
            if (!Roles.Any())
            {
                var seed = new IdentityRole[]
                {
                    new("Admin"),
                    new("User")
                };

                Roles.AddRange(seed);
            }

            if (Users.Any(u => u.Email == "admin@dev.com" || u.Email == "user@dev.com")
                && !UserRoles.Any())
            {
                var admin = Users.First(u => u.Email == "admin@dev.com");
                var user = Users.First(u => u.Email == "user@dev.com");

                var adminRole = Roles.First(r => r.Name == "Admin");
                var userRole = Roles.First(r => r.Name == "User");

                var seed = new IdentityUserRole<string>[]
                {
                    new()
                    {
                        UserId = admin.Id,
                        RoleId = adminRole.Id
                    },
                    new()
                    {
                        UserId = user.Id,
                        RoleId = userRole.Id
                    }
                };

                UserRoles.AddRange(seed);
            }

            SaveChanges();
        }

    }
}
