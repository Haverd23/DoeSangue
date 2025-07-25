﻿using DOS.Agenda.Application.Commands;
using DOS.Agenda.Domain;
using DOS.Core.Exceptions;
using DOS.Core.Exceptions.DOS.Core.Exceptions;
using DOS.Core.Mediator.Commands;

namespace DOS.Agenda.Application.CommandsHandlers
{
    public class AgendaCriadaCommandHandler : ICommandHandler<AgendaCriadaCommand, Guid>
    {
        private readonly IHorarioRepository _context;

        public AgendaCriadaCommandHandler(IHorarioRepository context)
        {
            _context = context;
        }
        public async Task<Guid> HandleAsync(AgendaCriadaCommand command)
        {
            var horario = new Horario(command.DataHora,command.VagasTotais);
           await _context.Adicionar(horario);
            var sucesso = await _context.UnitOfWork.Commit();
            if (!sucesso)
            {
                throw new AppException("Erro ao salvar o horário", 500);
            }
            return horario.Id;

        }
    }
}
