using Microsoft.VisualStudio.TestTools.UnitTesting;
using ActorRepositoryLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorRepositoryLib.Tests
{
    [TestClass()]
    public class ActorsRepositoryTests
    {
        [TestMethod()]
        public void GetTest()
        {
          ActorsRepository repository = new ActorsRepository();
           // Getall Test
            List<Actor> actors= repository.Get();
            Assert.IsNotNull(actors);
            Assert.AreEqual(4, actors.Count);

            //birthdayBeforce Test
            IEnumerable<Actor> birthdayBeforce = repository.Get(birthdayBeforce:1990);
              Assert.IsNotNull(birthdayBeforce);
              Assert.AreEqual(1988,birthdayBeforce.First().BirthYear);

            //birthdayAfter Test
            IEnumerable<Actor> birthdayAfter = repository.Get(birthdayAfter: 1990);
            Assert.IsNotNull(birthdayAfter);
            Assert.AreEqual(1998, birthdayAfter.First().BirthYear);

            //sortByName test
            IEnumerable<Actor> sortByName = repository.Get(sortBy:"name") ;
            Assert.IsNotNull (sortByName);
            Assert.AreEqual("alex", sortByName.First().Name);

            //sortById
            IEnumerable<Actor> sortById = repository.Get(sortBy: "Id");
            Assert.IsNotNull(sortById);
            Assert.AreEqual(1, sortById.First().Id);

        }
        [TestMethod()]
        public void GetbyIdTest()
        {
            ActorsRepository repository = new ActorsRepository();
            Actor? a1 = repository.GetById(1);
            
            Assert.AreEqual("michel", a1.Name);
        }

        [TestMethod()]
        public void AddTest()
        {
            ActorsRepository a1 = new ActorsRepository();
            Actor a2 = new Actor()
            {
                Name = "xiao",
                BirthYear = 1999
            };

            Actor? actor =a1.Add(a2);
            Assert.IsNotNull(actor);
            Assert.AreEqual(5, actor.Id);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            ActorsRepository a1 = new ActorsRepository();
             int idtomove = 2;

            Actor? actor= a1.Delete(2);
            Assert.IsNull(a1.actors.Find(actor => actor.Id == idtomove));
        }

        [TestMethod()]
        public void UpdateTest()
        {
            ActorsRepository a1 = new ActorsRepository();
            Actor a2 = new Actor()
            {
                Name = "xiao",
                BirthYear = 1999
            };
            Actor? actor2 =a1.Update(1, a2);
            Assert.AreEqual(a2.Name, actor2.Name);

        }

        [TestMethod()]
        public void toStringTest()
        {
            ActorsRepository a1 = new ActorsRepository();
              string tostring = a1.ToString();

            Assert.IsNotNull(tostring);
            Assert.IsTrue(tostring.Contains("michel"));
            

        }

    }
}