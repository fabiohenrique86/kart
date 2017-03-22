using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Kart.Repository
{
    public class RankRepository
    {
        public List<string> Obter(string caminho)
        {
            return File.ReadLines(caminho).ToList();
        }
    }
}
