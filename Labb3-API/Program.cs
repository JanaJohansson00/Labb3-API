
using Labb3_API.Data;
using Labb3_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb3_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<PersonInterestDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddAuthorization();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //Return all persons
            app.MapGet("/persons", async (PersonInterestDbContext context) =>
            {
                var persons = await context.Persons.ToListAsync();
                if (persons == null || !persons.Any())
                {
                    return Results.NotFound("Could not find any persons");
                }
                return Results.Ok(persons);
            });

            //Create a new person
            app.MapPost("/persons", async (Person person, PersonInterestDbContext context) =>
            {
                context.Persons.Add(person);
                await context.SaveChangesAsync();
                return Results.Created($"/persons/{person.PersonId}", person);

            });
           

            //Return all interests
            app.MapGet("/interests", async (PersonInterestDbContext context) =>
            {
                var interests = await context.Interests.ToListAsync();
                if (interests == null || !interests.Any())
                {
                    return Results.NotFound("Could not find any interests");
                }
                return Results.Ok(interests);
            });

            //Create a new interest
            app.MapPost("/interests", async (Interest interest, PersonInterestDbContext context) =>
            {
                context.Interests.Add(interest); 
                await context.SaveChangesAsync();
                return Results.Created($"/interests/{interest.InterestId}", interest); // Uppdatera också URL:en
            });

            // Endpoint för att hämta alla intressen för en specifik person
            app.MapGet("/persons/{personId}/interests", async (int personId, PersonInterestDbContext context) =>
            {
                // Hitta personen med det angivna personId
                var person = await context.Persons.FindAsync(personId);
                if (person == null)
                {
                    return Results.NotFound("Could not find the specified person");
                }

                // Hämta kopplingar mellan personen och intressen
                var personInterests = await context.PersonInterests
                                                    .Where(pi => pi.FkPersonId == personId)
                                                    .ToListAsync();

                // Lista för att lagra intressen
                var interests = new List<Interest>();

                // Loopa igenom kopplingarna och hämta intressen
                foreach (var personInterest in personInterests)
                {
                    var interest = await context.Interests.FindAsync(personInterest.FkInterestId);
                    if (interest != null)
                    {
                        interests.Add(interest);
                    }
                }

                if (!interests.Any())
                {
                    return Results.NotFound("Could not find any interests for the specified person");
                }

                return Results.Ok(interests);
            });

            // Endpoint för att koppla en person till ett nytt intresse
            app.MapPost("/persons/{personId}/interests", async (int personId, Interest newInterest, PersonInterestDbContext context) =>
            {
                // Hitta personen med det angivna personId
                var person = await context.Persons.FindAsync(personId);
                if (person == null)
                {
                    return Results.NotFound("Could not find the specified person");
                }

                // Kolla om det nya intresset redan finns i databasen
                var existingInterest = await context.Interests.FirstOrDefaultAsync(i => i.Title == newInterest.Title);
                if (existingInterest == null)
                {
                    // Om det nya intresset inte finns, lägg till det i databasen
                    context.Interests.Add(newInterest);
                    await context.SaveChangesAsync();
                    existingInterest = newInterest;
                }

                // Skapa en ny post i tabellen PersonInterests för att koppla personen till det nya intresset
                var personInterest = new PersonInterest
                {
                    FkPersonId = personId,
                    FkInterestId = existingInterest.InterestId
                };

                context.PersonInterests.Add(personInterest);
                await context.SaveChangesAsync();

                return Results.Created($"/persons/{personId}/interests/{existingInterest.InterestId}", personInterest);
            });

            // Endpoint för att lägga till ett befintligt intresse på en befintlig person
            app.MapPost("/person/{personId}/interests/{interestId}", async (int personId, int interestId, PersonInterestDbContext context) =>
            {
                // Kontrollera om den angivna personen och intresset finns i databasen
                var person = await context.Persons.FindAsync(personId);
                var interest = await context.Interests.FindAsync(interestId);

                if (person == null || interest == null)
                {
                    return Results.NotFound("Person or interest not found.");
                }

                // Kontrollera om personen redan har det angivna intresset
                var existingInterest = await context.PersonInterests
                                                    .Where(pi => pi.FkPersonId == personId && pi.FkInterestId == interestId)
                                                    .FirstOrDefaultAsync();

                if (existingInterest != null)
                {
                    return Results.Conflict("Person already has this interest.");
                }

                // Skapa en ny post i tabellen PersonInterests för att koppla personen till det angivna intresset
                var personInterest = new PersonInterest
                {
                    FkPersonId = personId,
                    FkInterestId = interestId
                };

                context.PersonInterests.Add(personInterest);
                await context.SaveChangesAsync();

                return Results.Created($"/person/{personId}/interests/{interestId}", personInterest);
            });



            //Return all links
            app.MapGet("/links", async (PersonInterestDbContext context) =>
            {
                var links = await context.Links.ToListAsync();
                if (links == null || !links.Any())
                {
                    return Results.NotFound("Could not find any links");
              }
               return Results.Ok(links);
            });

            ////Create a new link
            app.MapPost("/links", async (Link link, PersonInterestDbContext context) =>
            {
               context.Links.Add(link);
                await context.SaveChangesAsync();
               return Results.Created($"/interests/{link.LinkId}", link); // Uppdatera också URL:en
            });

            // Endpoint för att hämta alla länkar för en specifik person
            app.MapGet("/persons/{personId}/links", async (int personId, PersonInterestDbContext context) =>
            {
                // Hitta personen med det angivna personId
                var person = await context.Persons.FindAsync(personId);
                if (person == null)
                {
                    return Results.NotFound("Could not find the specified person");
                }

                // Hämta personintresseposter för den angivna personen
                var personInterests = await context.PersonInterests
                                                    .Where(pi => pi.FkPersonId == personId)
                                                    .ToListAsync();

                // Lista för att lagra länkar
                var links = new List<Link>();

                // Loopa igenom personintresseposter och hämta länkar
                foreach (var personInterest in personInterests)
                {
                    var interestLinks = await context.Links
                                                    .Where(link => link.FkInterestId == personInterest.FkInterestId)
                                                    .ToListAsync();
                    links.AddRange(interestLinks);
                }

                if (!links.Any())
                {
                    return Results.NotFound("Could not find any links for the specified person");
                }

                return Results.Ok(links);
            });

            //Endpoint för att lägga till ny länk för en specifik person och specifikt intresse
            app.MapPost("/persons/{personId}/interests/{interestId}/links", async (int personId, int interestId, Link link, PersonInterestDbContext context) =>
            {
                // Kontrollera om den angivna personen och intresset finns i databasen
                var person = await context.Persons.FindAsync(personId);
                var interest = await context.Interests.FindAsync(interestId);

                if (person == null || interest == null)
                {
                    return Results.NotFound("Person or interest not found.");
                }

                // Koppla länken till den angivna personen via det angivna intresset
                link.FkInterestId = interestId;

                context.Links.Add(link);
                await context.SaveChangesAsync();

                return Results.Created($"/persons/{personId}/interests/{interestId}/links/{link.LinkId}", link);
            });

            app.Run();
        }
    }
}
