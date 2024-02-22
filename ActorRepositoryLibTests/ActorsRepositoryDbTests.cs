using Microsoft.VisualStudio.TestTools.UnitTesting;
using ActorRepositoryLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ActorRepositoryLib.Tests
{
    [TestClass()]
    public class ActorsRepositoryDbTests
    {
        private const bool useDatabase=true;
        private static IActorsRepository _repo;
        private static ActorDbContext _dbContext;
        public string connectionString = "Server=mssql16.unoeuro.com;Database=luckfish_dk_db_the_database;User ID=luckfish_dk;Password=Ady9aGrFbthx4km5wceD;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
        [ClassInitialize]
        public static void InitOnce(TestContext context)
        {
            if (useDatabase)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ActorDbContext>();
                optionsBuilder.UseSqlServer(Secrets._connectionString);
                // connection string structure
                //   "Data Source=mssql7.unoeuro.com;Initial Catalog=FROM simply.com;Persist Security Info=True;User ID=FROM simply.com;Password=DB PASSWORD FROM simply.com;TrustServerCertificate=True"
                _dbContext = new ActorDbContext(optionsBuilder.Options);
                // clean database table: remove all rows
                _dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.actors");
                _repo = new ActorsRepositoryDb(_dbContext);
            }
        }
        [TestInitialize]
        public void Init()
        {
            if (useDatabase)
            {
                _dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.actors");
                _repo = new ActorsRepositoryDb(_dbContext);
            }
            else { _repo = new ActorsRepository(); }

            _repo.Add(new Actor { Name = "xiao", BirthYear = 1885 });
            _repo.Add(new Actor { Name = "xiao", BirthYear = 1885 });
            _repo.Add(new Actor { Name = "xiao", BirthYear = 1885 });
            _repo.Add(new Actor { Name = "xiao", BirthYear = 1885 });
        }



        [TestMethod()]
        public void AddTest()
        {
            _repo.Add(new Actor { Name = "xiao", BirthYear = 1885 });
            Actor? actor2 = _repo.Add(new Actor { Name = "alex", BirthYear = 1890 });
            Assert.IsTrue(actor2.Id >= 0);
            IEnumerable<Actor> all = _repo.Get();
            Assert.AreEqual(2, all.Count());
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Actor? actor =_repo.Add(new Actor { Name = "alex", BirthYear = 2016 });
            Actor? actor2=_repo.Delete(actor.Id);
           Assert.AreEqual("alex", actor2.Name);
           Assert.IsNull(_repo.GetById(actor2.Id));

        }

        [TestMethod()]
        public void GetAllTest()
        {
      
            _repo.Add(new Actor { Name = "xiao", BirthYear = 1885 });
            _repo.Add(new Actor { Name = "alex", BirthYear = 1885 });
            List<Actor> actors = _repo.Get();
            Assert.IsNotNull(actors);
            Assert.AreEqual(2, actors.Count);
        }

        [TestMethod()]
        public void GetTest1()
        {
            _repo.Add(new Actor { Name = "xiao", BirthYear = 1885 });
            _repo.Add(new Actor { Name = "alex", BirthYear = 2016 });
            _repo.Add(new Actor { Name = "mei", BirthYear = 1990 });
            _repo.Add(new Actor { Name = "zenk", BirthYear = 1885 });
            IEnumerable<Actor> birthdayBeforce = _repo.Get(birthdayBeforce: 1990);
            Assert.IsNotNull(birthdayBeforce);
            Assert.AreEqual(1885, birthdayBeforce.First().BirthYear);


            IEnumerable<Actor> birthday = _repo.Get(birthdayAfter: 1990);
            Assert.IsNotNull(birthday);
            Assert.AreEqual(2016, birthday.First().BirthYear);

            IEnumerable<Actor> sortByName = _repo.Get(sortBy: "name");
            Assert.IsNotNull(sortByName);
            Assert.AreEqual("alex", sortByName.First().Name);

            IEnumerable<Actor> sortByid = _repo.Get(sortBy: "Id");
            Assert.IsNotNull(sortByid);
            Assert.AreEqual(1, sortByid.First().Id);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            Actor? actor1 = _repo.Add(new Actor { Name = "xiao", BirthYear = 1885 });
            Actor? actor2 =_repo.GetById(actor1.Id);
            Assert.IsNotNull(actor2);
            Assert.AreEqual("xiao", actor2.Name);


        }

        [TestMethod()]
        public void UpdateTest()
        {
            Actor? actor1 = _repo.Add(new Actor { Name = "xiao", BirthYear = 1885 });
            Actor? actor2 = _repo.Update(actor1.Id, new Actor { Name = "up", BirthYear = 1999 });
            Assert.IsNotNull(actor2);
            Actor? actor3 = _repo.GetById(actor1.Id);
            Assert.AreEqual("up",actor2.Name);
          
        }
    }
}