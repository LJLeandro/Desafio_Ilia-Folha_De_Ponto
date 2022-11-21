using Ilia.FolhaDePonto.Api.Data.ValueObjects;
using Ilia.FolhaDePonto.Api.Utils;
using Xunit;

namespace Ilia.FolhaDePonto.Test
{
    public class FolhaDePontoUtilsTest
    {
        [Fact]
        public void TestaHoraSemFormatacao()
        {
            var valorEsperado = "69:35:5";
            var valorDeTeste = "PT69H35M5S";

            var resultado = FolhaDePontoUtils.ExcluiFormatacaoDaHora(valorDeTeste);

            Assert.Equal(valorEsperado, resultado);
        }

        [Fact]
        public void ValidaHoraDeAlmoco()
        {
            double valorEsperado = 3480;
            var horaDeSaidaParaAlmoco = "13:00:00";
            var horaDeVoltaDoAlmoco = "13:58:00";

            var resultado = FolhaDePontoUtils.CalculaHoraAlmoco(horaDeSaidaParaAlmoco, horaDeVoltaDoAlmoco);

            Assert.Equal(valorEsperado, resultado);
        }

        [Fact]
        public void ValidaTempoDeJornada()
        {
            double valorEsperado = 9 * 60 * 60;
            var horaDeEntrada = "09:00:00";
            var horaDeSaida = "18:00:00";

            var resultado = FolhaDePontoUtils.CalculaJornadaDeTrabalhoDeDia(horaDeEntrada, horaDeSaida);

            Assert.Equal(valorEsperado, resultado);
        }

        [Fact]
        public void ValidaTempoTrabalhoNoMes()
        {
            double valorEsperado = 8 * 60 * 60;

            RegistroVO registroDia = new()
            {
                Dia = "",
                Horarios = new List<string>()
                {
                    "09:00:00",
                    "12:00:00",
                    "13:00:00",
                    "18:00:00"
                }
            };

            var resultado = FolhaDePontoUtils.CalculaTempoTrabalhado(registroDia);

            Assert.Equal(valorEsperado, resultado);
        }

        [Fact]
        public void ValidaTempoTrabalhoSemHoraDeAlmoco()
        {
            string valorEsperado = "32:00:00";

            RegistroVO registroDia1 = new()
            {
                Dia = "",
                Horarios = new List<string>()
                {
                    "09:00:00",
                    "12:00:00",
                    "13:00:00",
                    "18:00:00"
                }
            };
            RegistroVO registroDia2 = new()
            {
                Dia = "",
                Horarios = new List<string>()
                {
                    "09:00:00",
                    "12:00:00",
                    "13:00:00",
                    "18:00:00"
                }
            };
            RegistroVO registroDia3 = new()
            {
                Dia = "",
                Horarios = new List<string>()
                {
                    "09:00:00",
                    "12:00:00",
                    "13:00:00",
                    "18:00:00"
                }
            };
            RegistroVO registroDia4 = new()
            {
                Dia = "",
                Horarios = new List<string>()
                {
                    "09:00:00",
                    "12:00:00",
                    "13:00:00",
                    "18:00:00"
                }
            };

            List<RegistroVO> registros = new List<RegistroVO>()
            {
                registroDia1, registroDia2, registroDia3, registroDia4
            };

            var resultado = FolhaDePontoUtils.CalculaHorasTrabalhadasNoMes(registros);

            Assert.Equal(valorEsperado, resultado);
        }

        [Fact]
        public void TestaHoraComFormatacao()
        {
            string valorEsperado = "PT32H00M00S";
            string horaSemFormatacao = "32:00:00";

            var resultado = FolhaDePontoUtils.IncluiFormatacaoDaHora(horaSemFormatacao);

            Assert.Equal(valorEsperado, resultado);
        }

        [Fact]
        public void TestaCalculoDeHorasAlocadasNoMes()
        {
            var resultadoEsperado = "25:34:00";

            var alocacoes = new List<AlocacaoVO>()
            {
                new AlocacaoVO()
                {
                    Tempo = "PT11H34M00S"
                },
                new AlocacaoVO()
                {
                    Tempo = "PT14H00M00S"
                }
            };

            var resultadoObtido = FolhaDePontoUtils.CalculaHorasAlocadasNoMes(alocacoes);

            Assert.Equal(resultadoEsperado, resultadoObtido);

        }

        [Fact]
        public void TestaCalculoDeHorasExcedentes ()
        {
            var resultadoEsperado = "PT01H34M00S";

            RegistroVO registroDia1 = new()
            {
                Dia = "",
                Horarios = new List<string>()
                {
                    "09:00:00",
                    "12:00:00",
                    "13:00:00",
                    "18:00:00"
                }
            };
            RegistroVO registroDia2 = new()
            {
                Dia = "",
                Horarios = new List<string>()
                {
                    "09:00:00",
                    "12:00:00",
                    "13:00:00",
                    "18:00:00"
                }
            };
            RegistroVO registroDia3 = new()
            {
                Dia = "",
                Horarios = new List<string>()
                {
                    "09:00:00",
                    "12:00:00",
                    "13:00:00",
                    "18:00:00"
                }
            };
            RegistroVO registroDia4 = new()
            {
                Dia = "",
                Horarios = new List<string>()
                {
                    "09:00:00",
                    "12:00:00",
                    "13:00:00",
                    "18:00:00"
                }
            };

            List<RegistroVO> registros = new List<RegistroVO>()
            {
                registroDia1, registroDia2, registroDia3, registroDia4
            };

            var alocacoes = new List<AlocacaoVO>()
            {
                new AlocacaoVO()
                {
                    Tempo = "PT11H34M00S"
                },
                new AlocacaoVO()
                {
                    Tempo = "Pt22H00M00S"
                }
            };

            var resultadoObtido = FolhaDePontoUtils.CalculaHorasExcedentesNoMes(alocacoes, registros);

            Assert.Equal(resultadoEsperado, resultadoObtido);
        }

        [Fact]
        public void TestaCalculoDeHorasDevidas()
        {
            var resultadoEsperado = "PT02H05M00S";

            RegistroVO registroDia1 = new()
            {
                Dia = "",
                Horarios = new List<string>()
                {
                    "09:00:00",
                    "12:00:00",
                    "13:00:00",
                    "18:00:00"
                }
            };
            RegistroVO registroDia2 = new()
            {
                Dia = "",
                Horarios = new List<string>()
                {
                    "09:00:00",
                    "12:00:00",
                    "13:00:00",
                    "18:00:00"
                }
            };
            RegistroVO registroDia3 = new()
            {
                Dia = "",
                Horarios = new List<string>()
                {
                    "09:00:00",
                    "12:00:00",
                    "13:00:00",
                    "18:00:00"
                }
            };
            RegistroVO registroDia4 = new()
            {
                Dia = "",
                Horarios = new List<string>()
                {
                    "09:00:00",
                    "12:00:00",
                    "13:00:00",
                    "18:00:00"
                }
            };

            List<RegistroVO> registros = new List<RegistroVO>()
            {
                registroDia1, registroDia2, registroDia3, registroDia4
            };

            var alocacoes = new List<AlocacaoVO>()
            {
                new AlocacaoVO()
                {
                    Tempo = "PT09H55M00S"
                },
                new AlocacaoVO()
                {
                    Tempo = "Pt20H00M00S"
                }
            };

            var resultadoObtido = FolhaDePontoUtils.CalculaHorasDevidasNoMes(alocacoes, registros);

            Assert.Equal(resultadoEsperado, resultadoObtido);
        }
    }
}