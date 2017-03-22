using System.Collections.Generic;

namespace Kart.Dao
{
    public class RankDao
    {
        public RankDao()
        {
            Arquivo = new List<string>();
        }

        public List<string> Arquivo { get; set; }
    }
}
