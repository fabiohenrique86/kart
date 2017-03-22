using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kart.Dao
{
    public class PilotoDao
    {
        public PilotoDao()
        {
            VoltasDao = new List<VoltaDao>();
        }

        public int Codigo { get; set; }
        public string Nome { get; set; }
        public List<VoltaDao> VoltasDao { get; set; }
        public long TempoTotal { get; set; }
        public int Voltas { get; set; }
        public double VelocidadeMedia { get; set; }
        public TimeSpan MelhorVolta { get; set; }
    }
}
