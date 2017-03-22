using System.Collections.Generic;
using Kart.Repository;

namespace Kart.Business
{
    public class RankBusiness
    {
        private RankRepository rankRepository;

        public RankBusiness()
        {
            rankRepository = new RankRepository();
        }

        /// <summary>
        /// Obtém o arquivo de rank
        /// </summary>
        /// <param name="caminho">caminho físico do arquivo</param>
        /// <returns></returns>
        public List<string> Obter(string caminho)
        {
            return rankRepository.Obter(caminho);
        }        
    }
}
