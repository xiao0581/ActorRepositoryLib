using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorRepositoryLib
{
    public class Actor
    {
       
        public int Id { get; set; }
        public string? Name { get; set; }
        public int BirthYear { get; set; }



        public void validateName()
        {
            if (Name is null)
            {
                throw new ArgumentNullException("name is null");
            }
            if (Name.Length <= 1)
            {
                throw new ArgumentException("name has to be at least one character ");
            }

        }
        public void validateBirthYear()
        {
            if (BirthYear <= 1820)
            {
                throw new ArgumentOutOfRangeException("BirthYear out of boundary");
            }

        }

      

        public override string ToString() =>
            $"{Id},{Name},{BirthYear}";
    }


}
