using System;
using System.Linq;
using System.IO;
using Kart.Business;

namespace Kart
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RankBusiness rankBusiness = new RankBusiness();
                PilotoBusiness pilotoBusiness = new PilotoBusiness();
                var FORMATO_TEMPO_VOLTA = "mm:ss.fff";
                var FORMATO_VELOCIDADE_MEDIDA = "#.000";
                
                var arquivoRank = rankBusiness.Obter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Rank.txt"));
                
                var pilotosDao = pilotoBusiness.Listar(arquivoRank);
                
                Console.WriteLine(string.Format("O campeão da corrida foi: {0}", pilotosDao.FirstOrDefault().Nome));
                Console.WriteLine("");

                int posicao = 1;
                foreach (var pilotoDao in pilotosDao)
                {
                    Console.WriteLine("Posição chegada: {0}, Código Piloto: {1}, Nome Piloto: {2}, Qtde Voltas Completadas: {3}, Tempo Total de Prova: {4}", posicao, pilotoDao.Codigo, pilotoDao.Nome, pilotoDao.Voltas, new DateTime(pilotoDao.TempoTotal).ToString(FORMATO_TEMPO_VOLTA));
                    Console.WriteLine(string.Format("Melhor volta do Piloto {0} foi: {1}", pilotoDao.Nome, new DateTime(pilotoDao.MelhorVolta.Ticks).ToString(FORMATO_TEMPO_VOLTA)));
                    Console.WriteLine(string.Format("Velocidade média do Piloto {0} foi: {1}", pilotoDao.Nome, pilotoDao.VelocidadeMedia.ToString(FORMATO_VELOCIDADE_MEDIDA)));
                    Console.WriteLine("");
                    posicao++;
                }

                Console.WriteLine("Melhor volta da corrida foi: {0}", new DateTime(pilotosDao.Min(x => x.MelhorVolta).Ticks).ToString(FORMATO_TEMPO_VOLTA));                
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
