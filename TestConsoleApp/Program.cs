using DerbyManagement.DAL;
using DerbyManagement.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new NullDatabaseInitializer<DerbyContext>());
            // InsertDerby();
            // InsertDivision();
            // InsertMultipleDivisions();
            // SimpleDivisionQueries();
            // QueryAndUpdateDivision();
            // QueryAndUpdateDivisionDisconnected();
            // RetrieveDataWithFind();
            // RetrieveDataWithSQL();
            // DeleteDivision();
            // DeleteDivisionWithKeyValue();
            //  DeleteDivisionViaStoredProcedure();
            // InsertRacerWithEquipment();
            // SimpleDivisionGraphQuery();
            ProjectionQuery();

            Console.ReadLine();
        }

        private static void ProjectionQuery()
        {
            using (var context = new DerbyContext())
            {
                context.Database.Log = Console.WriteLine;
                var divisions = context.Divisions
                    .Select(d => new { d.Name, d.Racers })
                    .ToList();
            }
        }

        private static void SimpleDivisionGraphQuery()
        {
            using (var context = new DerbyContext())
            {
                context.Database.Log = Console.WriteLine;

                // Eager loading
                //var division = context.Divisions
                //    .Include(d => d.Racers)
                //    .FirstOrDefault(d => d.Name.StartsWith("Anoth"));

                // Explicit loading
                //var division = context.Divisions
                //    .FirstOrDefault(d => d.Name.StartsWith("Anoth"));
                //Console.WriteLine("Division Retrieved: " + division.Name);
                //context.Entry(division)
                //    .Collection(d => d.Racers).Load();

                // Lazy loading
                var division = context.Divisions
                    .FirstOrDefault(d => d.Name.StartsWith("Anoth"));
                Console.WriteLine("Division Retrieved: " + division.Name);
                Console.WriteLine("Division Racer Count: {0}", division.Racers.Count());
                // Racer count will return Zero.  
                // If the List of racers in the division class is declared virtual, 
                // the Racers will be loaded when automatically whenever they are 
                // referenced in code, as in the above example. 
            }
        }

        private static void InsertRacerWithEquipment()
        {
            using (var context = new DerbyContext())
            {
                context.Database.Log = Console.WriteLine;

                var division = new Division
                {
                    DerbyId = 2,
                    Name = "Anothe Test Division",
                    IncludeInChampionship = false,
                    IsChampionship = false
                };

                var racerOne = new Racer
                {
                    CarNumber = 10,
                    CarName = "Car Number Ten",
                    OwnerFirstName = "Owner Number Ten"
                };
                var racerTwo = new Racer
                {
                    CarNumber = 11,
                    CarName = "Car Number 11",
                    OwnerFirstName = "Owner Number 11"
                };
                context.Divisions.Add(division);
                division.Racers.Add(racerOne);
                division.Racers.Add(racerTwo);
                context.SaveChanges();
            }
        }

        private static void DeleteDivisionViaStoredProcedure()
        {
            var keyVal = 37;
            using (var context = new DerbyContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Database.ExecuteSqlCommand(
                    "exec DeleteDivisionViaIdv{0}", keyVal);
            }
        }

        // causes two databse round trips
        // will cause exception if id does not exist in database, so
        // check for null before running remove command
        private static void DeleteDivisionWithKeyValue()
        {
            var keyVal = 37;
            using (var context = new DerbyContext())
            {
                context.Database.Log = Console.WriteLine;
                var division = context.Divisions.Find(keyVal);
                context.Divisions.Remove(division);
                context.SaveChanges();
            }
        }

        private static void DeleteDivision()
        {
            Division division;
            using (var context = new DerbyContext())
            {
                context.Database.Log = Console.WriteLine;
                division = context.Divisions
                    .Where(d => d.DivisionId == 9)
                    .FirstOrDefault();
            }
            // User deletes the division in the application
            using (var context = new DerbyContext())
            {
                context.Database.Log = Console.WriteLine;
                // context.Divisions.Attach(division);
                // context.Divisions.Remove(division);
                context.Entry(division).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        // Can use this pattern to execute stored procedures
        // e.g. ... .SqlQuery("exec SomeProcName");
        private static void RetrieveDataWithSQL()
        {
            using (var context = new DerbyContext())
            {
                context.Database.Log = Console.WriteLine;
                var divisions = context.Divisions
                    .SqlQuery("select * from divisions where divisionid in (3,4)");
                foreach (var division in divisions)
                {
                    Console.WriteLine(division.Name);
                }
            }
        }

        private static void RetrieveDataWithFind()
        {
            var keyVal = 4;
            using (var context = new DerbyContext())
            {
                context.Database.Log = Console.WriteLine;
                var division = context.Divisions.Find(keyVal);
                Console.WriteLine("After Find #1: " + division.Name);

                var anotherDivision = context.Divisions.Find(keyVal);
                Console.WriteLine("After Find #2: " + anotherDivision.Name);
                division = null;
            }
        }

        // pattern for websites and services - disconnected- ie. no 
        // direct connection to the database
        private static void QueryAndUpdateDivisionDisconnected()
        {
            Division division;
            using (var context = new DerbyContext())
            {
                context.Database.Log = Console.WriteLine;
                division = context.Divisions
                    .Where(d => d.DivisionId == 9)
                    .FirstOrDefault();
            }

            division.LogoPath = "over there";

            using (var context = new DerbyContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Divisions.Attach(division);
                context.Entry(division).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        private static void QueryAndUpdateDivision()
        {
            using (var context = new DerbyContext())
            {
                context.Database.Log = Console.WriteLine;
                var division = context.Divisions
                    .Where(d => d.DivisionId == 9)
                    .FirstOrDefault();
                division.IsChampionship = true;
                division.LogoPath = "YaddaYadda";
                division.Sequence = 8;
                context.SaveChanges();
            }
        }

        private static void InsertDerby()
        {
            var derby = new Derby
            {
                Name = "2017 Pack 511 Pinewood Derby",
                Date = new DateTime(2017, 4, 22),
                Lanes = 3,
                HasChampionship = true,
                DivisionPlacesToAdvance = 3,
                ScoringType = ScoringType.Time
            };
            using (var context = new DerbyContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Derbies.Add(derby);
                context.SaveChanges();
            }
        }

        private static void InsertDivision()
        {
            var division = new Division
            {
                DerbyId = 2,
                Sequence = 7,
                Name = "Test Division",
                LogoPath = "BlahBlahBlah",
                IncludeInChampionship = true,
                IsChampionship = false
            };
            using (var context = new DerbyContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Divisions.Add(division);
                context.SaveChanges();
            }
        }

        private static void InsertMultipleDivisions()
        {
            var division1 = new Division
            {
                DerbyId = 2,
                Sequence = 4,
                Name = "Webelos",
                IncludeInChampionship = true,
                IsChampionship = false
            };
            var division2 = new Division
            {
                DerbyId = 2,
                Sequence = 5,
                Name = "Championship",
                IncludeInChampionship = false,
                IsChampionship = true
            };
            var division3 = new Division
            {
                DerbyId = 2,
                Sequence = 6,
                Name = "Open",
                IncludeInChampionship = false,
                IsChampionship = false
            };
            var division4 = new Division
            {
                DerbyId = 2,
                Sequence = 7,
                Name = "Unlimited",
                IncludeInChampionship = false,
                IsChampionship = false
            };
            using (var context = new DerbyContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Divisions.AddRange(new List<Division> { division1, division2, division3, division4 });
                context.SaveChanges();
            }
        }

        private static void SimpleDivisionQueries()
        {
            using (var context = new DerbyContext())
            {
                // var divisions = context.Divisions.ToList();
                // var divisions = context.Divisions.Where(d => d.Name == "Wolves");
                // var divisions = context.Divisions.Where(d => d.Sequence > 1);
                //var division = context.Divisions.Where(d => d.Sequence > 1).FirstOrDefault();
                ////foreach (var division in divisions)
                ////{
                //    Console.WriteLine(division.Name);
                ////}
                var division = context.Divisions.
                    Where(d => d.Sequence > 1)
                    .OrderBy(d => d.Name)
                    //.Skip(1).Take(1)
                    .FirstOrDefault();
                Console.WriteLine(division.Name);
            }
        }

    }
}
