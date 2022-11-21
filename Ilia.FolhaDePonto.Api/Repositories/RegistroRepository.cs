using AutoMapper;
using Ilia.FolhaDePonto.Api.Data.ValueObjects;
using Ilia.FolhaDePonto.Api.Models.Base;
using Ilia.FolhaDePonto.Api.Models.Context;
using Ilia.FolhaDePonto.Api.Utils;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;

namespace Ilia.FolhaDePonto.Api.Repositories
{
    public class RegistroRepository : IRegistroRepository
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public RegistroRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> QuantidadeDeRegistroPorDia(string dia)
        {
            var registrosDoDia = await _context.Registros.Where(x => x.Dia == dia).FirstOrDefaultAsync();

            if (registrosDoDia == null)
                return 0;

            var horariosRegistrados = registrosDoDia.Horarios.Split(";").Length;

            return horariosRegistrados;
        }

        public async Task<RegistroVO> RecuperaTodosOsRegistrosPorDiaAsync(string dia)
        {
            var registrosDoDia = await _context.Registros.Where(x => x.Dia == dia).FirstOrDefaultAsync();

            return MapUtils.RegistroEntityParaRegistroVO(registrosDoDia);
        }

        public async Task<RegistroVO> SalvarNovoRegistro(MomentoVO momentoVO)
        {
            var registro = await _context.Registros.Where(x => x.Dia == momentoVO.DataHora.ToString("yyyy-MM-dd")).FirstOrDefaultAsync();

            if (registro == null)
            {
                var registroASerCriado = new RegistroEntity()
                {
                    Dia = momentoVO.DataHora.ToString("yyyy-MM-dd"),
                    Horarios = momentoVO.DataHora.ToString("HH:mm:ss")
                };

                await _context.Registros.AddAsync(registroASerCriado);

                _context.SaveChanges();

                return await RecuperaTodosOsRegistrosPorDiaAsync(momentoVO.DataHora.ToString("yyyy-MM-dd"));
            }
            else
            {
                return await AtualizarRegistroAsync(momentoVO);
            }
        }

        public async Task<RegistroVO> AtualizarRegistroAsync(MomentoVO momentoVO)
        {
            var registro = await _context.Registros.Where(x => x.Dia == momentoVO.DataHora.ToString("yyyy-MM-dd")).FirstOrDefaultAsync();

            registro.Horarios += $";{momentoVO.DataHora.ToString("HH:mm:ss")}";

            registro.Horarios = RegistroUtils.ExtraiHorariosParaString(registro.Horarios);

            _context.Update(registro);
            await _context.SaveChangesAsync();

            return await RecuperaTodosOsRegistrosPorDiaAsync(momentoVO.DataHora.ToString("yyyy-MM-dd"));
        }

        public async Task<bool> VerificaRegistroDuplicado(MomentoVO momentoVO)
        {
            var registroDuplicado = await _context.Registros
                .Where(x => x.Dia == momentoVO.DataHora.ToString("yyyy-MM-dd")
                    && x.Horarios.Contains(momentoVO.DataHora.ToString("HH:mm:ss")))
                .FirstOrDefaultAsync();

            if (registroDuplicado == null)
                return false;

            return true;
        }

        public async Task<List<RegistroVO>> BuscaRegistroPorMes(string mes)
        {
            var registrosEntity = await _context.Registros.Where(x => x.Dia.Contains(mes)).ToListAsync();

            List<RegistroVO> registrosVO = new();

            foreach (var registro in registrosEntity)
                registrosVO.Add(MapUtils.RegistroEntityParaRegistroVO(registro));

            return registrosVO;
        }
    }
}
