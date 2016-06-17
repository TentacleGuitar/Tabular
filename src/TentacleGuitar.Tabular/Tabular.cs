using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TentacleGuitar.Tabular
{
    public class Tabular
    {
        public int BPM { get; set; }

        public int Capo { get; set; }

        public List<Staff> Staff { get; set; }

        public Dictionary<long, List<Note>> Notes { get; set; }
    }
}
