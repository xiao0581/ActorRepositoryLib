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
        private const bool useDatabase = true;
        private static IActorsRepository _repo;
        private static ActorDbContext _dbContext;


        [ClassInitialize]
        public static void InitOnce(TestContext context)
        {
            if (useDatabase)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ActorDbContext>();
                optionsBuilder.UseSqlServer(Secrets._connectionString);
                 _dbContext = new ActorDbContext(optionsBuilder.Options);
                _dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.actors");
                _repo = new ActorsRepositoryDb(_dbContext);
                _repo.Add(new Actor { Name = "xiao", BirthYear = 1992 });
                _repo.Add(new Actor { Name = "alex", BirthYear = 2016 });
                _repo.Add(new Actor { Name = "mei", BirthYear = 1990 });
                _repo.Add(new Actor { Name = "morten", BirthYear = 1980 });

            }
            else { _repo = new ActorsRepository(); }
        }
        [TestMethod()]
        public void AddTest()
        {

            Actor a2 = new Actor { Name = "hehe", BirthYear = 1999 };
            Actor? actor5 = _repo.Add(a2);
            Assert.AreEqual(a2.Name, actor5.Name);
        }

        [TestMethod()]
        public void GetAllTest()
        {

          
            List<Actor> actors = _repo.Get();
            Assert.IsNotNull(actors);
            Assert.AreEqual(5, actors.Count);
        }

        [TestMethod()]
        public void GetTest1()
        {

            IEnumerable<Actor> birthdayBe = _repo.Get(birthdayBeforce: 1990);
            Assert.IsNotNull(birthdayBe);
            Assert.AreEqual(1980, birthdayBe.First().BirthYear);


            IEnumerable<Actor> birthday = _repo.Get(birthdayAfter: 1990);
            Assert.IsNotNull(birthday);
            Assert.AreEqual(1992, birthday.First().BirthYear);

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

            Actor? actor2 = _repo.GetById(1);
            Assert.IsNotNull(actor2);
            Assert.AreEqual("xiao", actor2.Name);


        }
       
      

        [TestMethod()]
        public void DeleteTest()
        {
            Actor? actor5 = _repo.Add(new Actor { Name = "hello", BirthYear = 1980 });
            Actor? deletActor = _repo.Delete(actor5.Id);
            Assert.AreEqual("hello", deletActor.Name);
            Assert.IsNull(_repo.GetById(deletActor.Id));

        }
        [TestMethod()]
        public void UpdateTest()
        {

            Actor? actor2 = _repo.Update(1, new Actor { Name = "up", BirthYear = 1999 });
            Assert.IsNotNull(actor2);
            Actor? actor3 = _repo.GetById(1);
            Assert.AreEqual("up", actor2.Name);

            //}
        }
    }
}