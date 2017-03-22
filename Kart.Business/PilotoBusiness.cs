using System;
using System.Collections.Generic;
using System.Linq;
using Kart.Dao;
using Kart.Repository;

namespace Kart.Business
{
    public class PilotoBusiness
    {
        private PilotoRepository pilotoRepository;

        public PilotoBusiness()
        {
            pilotoRepository = new PilotoRepository();
        }

        /// <summary>
        /// Lista os pilotos baseados no arquivo passado por parâmetro
        /// </summary>
        /// <param name="arquivo">arquivo de rank</param>
        /// <returns>lista de pilotos</returns>
        public List<PilotoDao> Listar(List<string> arquivo)
        {
            List<PilotoDao> pilotosDao = new List<PilotoDao>();

            // pula a 1ª linha do cabeçalho
            foreach (var arquivoLinha in arquivo.Skip(1))
            {
                var linha = arquivoLinha.Split("\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                // remove os espaços em branco
                var l = linha.Where(x => !string.IsNullOrEmpty(x.Trim())).ToArray();

                VoltaDao voltaDao = new VoltaDao();
                PilotoDao pilotoDao = new PilotoDao();

                voltaDao.Hora = Convert.ToDateTime(l[0]);
                voltaDao.Numero = Convert.ToInt32(l[2]);
                voltaDao.Tempo = new TimeSpan(0, 0, Convert.ToInt32(l[3].Substring(0, 1)), Convert.ToInt32(l[3].Substring(2, 2)), Convert.ToInt32(l[3].Substring(5, 3)));
                voltaDao.VelocidadeMedia = Convert.ToDouble(l[4]);

                var piloto = l[1].Split("–".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                pilotoDao.Codigo = Convert.ToInt32(piloto[0]);
                pilotoDao.Nome = piloto[1].Trim();

                pilotoDao.VoltasDao.Add(voltaDao);

                // se não existir piloto, adiciona-o
                // se existir, adiciona somente a volta
                var p = pilotosDao.FirstOrDefault(x => x.Codigo == pilotoDao.Codigo);
                if (p == null)
                {
                    pilotosDao.Add(pilotoDao);
                }
                else
                {
                    p.VoltasDao.Add(voltaDao);
                }
            }

            // agrupa a lista de acordo com o tempo de corrida
            return pilotosDao
                    .GroupBy(x => new PilotoDao
                    {
                        Codigo = x.Codigo,
                        Nome = x.Nome,
                        TempoTotal = x.VoltasDao.Sum(w => w.Tempo.Ticks),
                        Voltas = x.VoltasDao.Count(),
                        VelocidadeMedia = x.VoltasDao.Sum(o => o.VelocidadeMedia / x.VoltasDao.Count()),
                        MelhorVolta = x.VoltasDao.Min(p => p.Tempo)
                    })
                    .Select(g => g.Key)
                    .OrderByDescending(t => t.Voltas)
                    .ThenBy(y => y.TempoTotal)
                    .ToList();
        }
    }
}
