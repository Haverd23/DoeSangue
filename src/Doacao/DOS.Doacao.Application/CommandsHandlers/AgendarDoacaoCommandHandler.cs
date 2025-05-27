using DOS.Core.Mediator.Commands;
using DOS.Doacao.Application.Commands;
using DOS.Doacao.Domain;
using DOS.Usuario.Domain;


namespace DOS.Doacao.Application.CommandsHandlers
{
    public class AgendarDoacaoCommandHandler : ICommandHandler<AgendarDoacaoCommand, Guid>
    {
        private readonly IDoacaoRepository _doacaoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        public AgendarDoacaoCommandHandler(IDoacaoRepository doacaoRepository,
            IUsuarioRepository usuarioRepository)
        {
            _doacaoRepository = doacaoRepository;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<Guid> HandleAsync(AgendarDoacaoCommand command)
        {
            var usuario = await _usuarioRepository.GetById(command.UserId);
            string? tipoSanguineo = usuario?.TipoSanguineo?.ToString();

            var doacao = new DoacaoRegistro(
            command.AgendaId,
            command.UserId,
            tipoSanguineo,
            command.DataHoraAgendada
        );
            await _doacaoRepository.AdicionarAsync(doacao);
            var sucesso = await _doacaoRepository.UnitOfWork.Commit();
            if (!sucesso)
            {
                throw new Exception("Erro ao agendar a doação");
            }
            return doacao.Id;

        }
    }
}
