using DOS.Core.Enums;
using DOS.Estoque.Application.Kafka.Eventos;
using DOS.Estoque.Domain;
using System;

namespace DOS.Estoque.Application.Kafka.EventosHandlers
{
    public class DoacaoFinalizadaEventHandler
    {
        private readonly IEstoqueRepository _repository;

        public DoacaoFinalizadaEventHandler(IEstoqueRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> HandleAsync(DoacaoFinalizadaEvent evento)
        {
            var tipoSanguineo = (TipoSanguineo)Enum.Parse(typeof(TipoSanguineo),
                evento.TipoSanguineo, ignoreCase: true);

            var estoque =  await _repository.ObterPorTipoAsync(tipoSanguineo);

            if (estoque == null)
            {
                estoque = new EstoqueSanguineo(tipoSanguineo);
                estoque.RegistrarDoacao();
                _repository.Adicionar(estoque);
            }
            else
            {
                estoque.RegistrarDoacao();

            }
            return await _repository.UnitOfWork.Commit();

        }


    }
}
