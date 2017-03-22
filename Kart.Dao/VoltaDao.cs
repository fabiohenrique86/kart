using System;

namespace Kart.Dao
{
    public class VoltaDao
    {
        public DateTime Hora { get; set; }
        public int Numero { get; set; }
        public TimeSpan Tempo { get; set; }
        public double VelocidadeMedia { get; set; }
    }
}
