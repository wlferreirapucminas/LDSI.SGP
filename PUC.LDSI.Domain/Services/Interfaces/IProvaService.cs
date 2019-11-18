﻿using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.InputData;
using PUC.LDSI.Domain.QueryResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services.Interfaces
{
    public interface IProvaService
    {
        Task<List<AvaliacaoPublicadaQueryResult>> ListarAvaliacoesPublicadasAsync(string login);
        Task<ProvaQueryResult> ObterProvaAsync(int publicacaoId, string login);
        Task<int> AdicionarProvaAsync(ProvaInputData provaInputData, string login);
    }
}
