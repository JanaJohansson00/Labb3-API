﻿using Labb3_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb3_API.Data
{
    public class PersonInterestDbContext : DbContext
    {
        public PersonInterestDbContext(DbContextOptions<PersonInterestDbContext> options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<PersonInterest> PersonInterests { get; set; }

    }
}
