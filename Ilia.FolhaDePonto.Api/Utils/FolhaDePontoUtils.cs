using Ilia.FolhaDePonto.Api.Data.ValueObjects;
using System;

namespace Ilia.FolhaDePonto.Api.Utils
{
    public static class FolhaDePontoUtils
    {
        public static string ExcluiFormatacaoDaHora(string horaFormatada)
        {
            return horaFormatada.ToLower()
                .Replace("pt", "")
                .Replace("h", ":")
                .Replace("m", ":")
                .Replace("s", "");
        }

        public static double CalculaHoraAlmoco(string horaSaida, string horaVolta)
        {
            return (TimeSpan.Parse(horaVolta) - TimeSpan.Parse(horaSaida)).TotalSeconds;
        }

        public static double CalculaJornadaDeTrabalhoDeDia(string horaDeEntradaNoTrabalho, string horaDeSaidaDoTrabalho)
        {
            var tempoTotalDeJornada = (TimeSpan.Parse(horaDeSaidaDoTrabalho) - TimeSpan.Parse(horaDeEntradaNoTrabalho)).TotalSeconds;

            return tempoTotalDeJornada;
        }

        public static double CalculaTempoTrabalhado(RegistroVO registros)
        {
            var tempoDeAlmoco = CalculaHoraAlmoco(registros.Horarios[1], registros.Horarios[2]);
            var tempoTotalDeJornada = CalculaJornadaDeTrabalhoDeDia(registros.Horarios.First(), registros.Horarios.Last());

            return tempoTotalDeJornada - tempoDeAlmoco;
        }

        public static string CalculaHorasTrabalhadasNoMes(List<RegistroVO> registros)
        {
            double totalDeHorasTrabalhadasNoMes = 0;

            foreach (var registro in registros)
                totalDeHorasTrabalhadasNoMes += CalculaTempoTrabalhado(registro);

            return FormataTempoBaseadoEmSegundos(totalDeHorasTrabalhadasNoMes);
        }

        public static string IncluiFormatacaoDaHora(string horaNaoFormatada)
        {
            var dados = horaNaoFormatada.Split(":");

            return $"PT{dados[0]}H{dados[1]}M{dados[2]}S";
        }

        public static string CalculaHorasExcedentesNoMes(List<AlocacaoVO> alocacoes, List<RegistroVO> registros)
        {
            var horasTrabalhadasNoMes = CalculaHorasTrabalhadasNoMes(registros);

            var horasAlocadas = CalculaHorasAlocadasNoMes(alocacoes);

            var horasExcedentesEmSegundos = ConverteValoresComMaisDe24Horas(horasAlocadas).TotalSeconds - ConverteValoresComMaisDe24Horas(horasTrabalhadasNoMes).TotalSeconds;

            var horasExcedentesFormatadas = FormataTempoBaseadoEmSegundos(horasExcedentesEmSegundos);

            return IncluiFormatacaoDaHora(horasExcedentesFormatadas);
        }

        private static TimeSpan ConverteValoresComMaisDe24Horas(string horas)
        {
            return new TimeSpan(int.Parse(horas.Split(':')[0]),
                            int.Parse(horas.Split(':')[1]),
                            int.Parse(horas.Split(':')[2]));

        }

        public static string CalculaHorasAlocadasNoMes(List<AlocacaoVO> alocacoes)
        {
            var horasAlocadas = 0.0;

            foreach (var alocacao in alocacoes)
                horasAlocadas += TimeSpan.Parse(ExcluiFormatacaoDaHora(alocacao.Tempo)).TotalSeconds;

            return FormataTempoBaseadoEmSegundos(TimeSpan.FromSeconds(horasAlocadas).TotalSeconds);
        }

        public static string CalculaHorasDevidasNoMes(List<AlocacaoVO> alocacoes, List<RegistroVO> registros)
        {
            var horasTrabalhadasNoMes = CalculaHorasTrabalhadasNoMes(registros);

            var horasAlocadas = CalculaHorasAlocadasNoMes(alocacoes);

            var horasExcedentesEmSegundos = ConverteValoresComMaisDe24Horas(horasTrabalhadasNoMes).TotalSeconds - ConverteValoresComMaisDe24Horas(horasAlocadas).TotalSeconds;

            if (horasExcedentesEmSegundos < 0)
                return IncluiFormatacaoDaHora(FormataTempoBaseadoEmSegundos(horasExcedentesEmSegundos * -1));
            else
                return IncluiFormatacaoDaHora(FormataTempoBaseadoEmSegundos(0));
        }

        public static string FormataTempoBaseadoEmSegundos(double totalSegundos)
        {
            var ts = TimeSpan.FromSeconds(totalSegundos);

            return $"{RecuperaHoraSemDecimal(ts.TotalHours):00}:{ts.Minutes:00}:{ts.Seconds:00}";
        }

        public static double RecuperaHoraSemDecimal(double horas)
        {
            if (horas.ToString().Contains(",")) 
            { 
                return double.Parse(horas.ToString().Split(',')[0]);
            }
            else
            {
                return horas;
            }
        }
    }
}
