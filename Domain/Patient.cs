using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Patient
    {
        public int Id { get; set; }
        public string Ssn { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Measurement>? Measurements { get; set; }

    }
}
