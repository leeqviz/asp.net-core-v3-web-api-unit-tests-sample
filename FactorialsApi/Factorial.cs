using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FactorialsApi
{
    public class Factorial
    {
        [Key]
        public int? Value { get; set; }
        public long? Result { get; set; }
    }
}
