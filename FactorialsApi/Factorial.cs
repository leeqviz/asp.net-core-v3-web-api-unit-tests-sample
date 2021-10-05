using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FactorialsApi
{
    public class Factorial
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? Value { get; set; }
        public long? Result { get; set; }
    }
}
