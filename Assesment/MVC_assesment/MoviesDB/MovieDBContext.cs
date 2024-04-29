    using System;
    using System.Data.Entity;
    using MoviesDB;
    namespace MoviesDB
    {
        public class MovieDBContext : DbContext
        {
            public MovieDBContext() : base("connectstr")
            { }
            public DbSet<Movies> Movies { get; set; }

            internal void SaveChanges()
            {
                throw new NotImplementedException();
            }
        }
    }
