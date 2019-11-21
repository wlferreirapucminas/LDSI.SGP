using PUC.LDSI.Domain.QueryResult;
using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Services.Interfaces;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using PUC.LDSI.Domain.Exception;
using PUC.LDSI.Domain.InputData;
using PUC.LDSI.Domain.Entities;

namespace PUC.LDSI.Domain.Services
{
    public class ProvaService : IProvaService
    {
        private readonly IPublicacaoRepository _publicacaoRepository;
        private readonly IProvaRepository _provaRepository;
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IAlunoRepository _alunoRepository;
        private readonly IQuestaoRepository _questaoRepository;

        public ProvaService(IPublicacaoRepository publicacaoRepository,
                            IProvaRepository provaRepository,
                            IAvaliacaoRepository avaliacaoRepository,
                            IAlunoRepository alunoRepository,
                            IQuestaoRepository questaoRepository)
        {
            _provaRepository = provaRepository;
            _alunoRepository = alunoRepository;
            _avaliacaoRepository = avaliacaoRepository;
            _publicacaoRepository = publicacaoRepository;
            _questaoRepository = questaoRepository;
        }

        public async Task<List<AvaliacaoPublicadaQueryResult>> ListarAvaliacoesPublicadasAsync(string login)
        {
            var aluno = _alunoRepository.ObterPorLogin(login);

            if (aluno == null) throw new DomainException("O aluno não foi localizado!");

            var publicacoes = await _publicacaoRepository.ListarPublicacoesDoAlunoAsync(login);

            var retorno = new List<AvaliacaoPublicadaQueryResult>();

            publicacoes.ForEach(x => {
                var prova = x.Avaliacao.Provas.FirstOrDefault(a => a.AlunoId == aluno.Id);

                decimal nota = 0;
                int q = 0;
                if (prova != null)
                {
                    foreach (var questao in prova.QuestoesProva)
                    {
                        nota = nota + questao.Nota;
                        q++;
                    }
                }
                if (q == 0) q = 1;

                retorno.Add(new AvaliacaoPublicadaQueryResult()
                {
                    AlunoId = aluno.Id,
                    AvaliacaoId = x.AvaliacaoId,
                    DataFim = x.DataFim,
                    DataInicio = x.DataInicio,
                    DataPublicacao = x.DataPublicacao,
                    Descricao = x.Avaliacao.Descricao,
                    Disciplina = x.Avaliacao.Disciplina,
                    Materia = x.Avaliacao.Materia,
                    ValorProva = x.ValorProva,
                    PublicacaoId = x.Id,
                    ProvaId = prova == null ? (int?)null : prova.Id,
                    DataRealizacao = prova == null ? (DateTime?)null : prova.DataProva,
                    NotaObtida = x.ValorProva / q * nota//null //TODO - Calcular a nota obtida e retornar nesse atributo.
                });
            });

            return retorno;
        }

        public async Task<ProvaQueryResult> ObterProvaAsync(int publicacaoId, string login)
        {
            var aluno = _alunoRepository.ObterPorLogin(login);

            if (aluno == null) throw new DomainException("O aluno não foi localizado!");

            var publicacao = await _publicacaoRepository.ObterAsync(publicacaoId);

            if (publicacao == null) throw new DomainException("A publicacao não foi localizada!");

            var avaliacaoCompleta = await _avaliacaoRepository.ObterComQuestoresAsync(publicacao.AvaliacaoId);

            var provaCompleta = await _provaRepository.ObterProvaDoAlunoAsync(publicacao.AvaliacaoId, aluno.Id);

            var retorno = new ProvaQueryResult()
            {
                AvaliacaoId = publicacao.AvaliacaoId,
                PublicacaoId = publicacao.Id,
                Questoes = avaliacaoCompleta.Questoes.Select(x => new QuestaoProvaQueryResult()
                {
                    QuestaoId = x.Id,
                    Enunciado = x.Enunciado,
                    Tipo = x.Tipo,
                    Opcoes = x.Opcoes.Select(y => new OpcaoProvaQueryResult()
                    {
                        OpcaoAvaliacaoId = y.Id,
                        QuestaoId = y.QuestaoId,
                        Descricao = y.Descricao
                    }).ToList()
                }).ToList()
            };

            retorno.Questoes.SelectMany(x => x.Opcoes).ToList().ForEach(x => {
                x.Resposta = provaCompleta?.QuestoesProva?
                    .SelectMany(y => y.OpcoesProva)
                    .FirstOrDefault(y => y.OpcaoAvaliacaoId == x.OpcaoAvaliacaoId)?.Resposta ?? false;
            });

            return retorno;
        }

        public async Task<int> AdicionarProvaAsync(ProvaInputData provaInputData, string login)
        {
            var aluno = _alunoRepository.ObterPorLogin(login);
            var avaliacao = await _avaliacaoRepository.ObterComQuestoresAsync(provaInputData.AvaliacaoId);

            Prova prova = new Prova();
            prova.AlunoId = aluno.Id;
            prova.AvaliacaoId = provaInputData.AvaliacaoId;
            prova.DataProva = DateTime.Now;
            prova.Aluno = aluno;
            prova.Avaliacao = avaliacao;

            foreach (var questao in provaInputData.Questoes)
            {
                var questaoProva = new QuestaoProva() { QuestaoId = questao.QuestaoId };
                prova.QuestoesProva.Add(questaoProva);

                foreach (var opcao in questao.Opcoes)
                {
                    var opcaoProva = new OpcaoProva() { OpcaoAvaliacaoId = opcao.OpcaoAvaliacaoId, Resposta = opcao.Resposta };
                    questaoProva.OpcoesProva.Add(opcaoProva);
                }
            }

            //var avaliacao = await _avaliacaoRepository.ObterComQuestoresAsync(provaInputData.AvaliacaoId);

            foreach (var questao in prova.QuestoesProva)
            {
                int count = 0;
                int nota = 0;
                var questaoAvaliacao = avaliacao.Questoes.Find(y => y.Id == questao.QuestaoId);
                if (questaoAvaliacao.Tipo == 1)
                {
                    var idVerdadeira = questaoAvaliacao.Opcoes.Find(x => x.Verdadeira).Id;
                    questao.Nota = questao.OpcoesProva.Find(y => y.OpcaoAvaliacaoId == idVerdadeira && y.Resposta) == null ? 0 : 1;
                }
                if (questaoAvaliacao.Tipo == 2)
                {
                    foreach (var opcoes in questaoAvaliacao.Opcoes)
                    {
                        count++;
                        var resposta1 = questaoAvaliacao.Opcoes[count].Verdadeira;
                        var resposta2 = questao.OpcoesProva[count].Resposta;
                        if (resposta1 == resposta2) { nota++; }
                    }
                    questao.Nota = 1 / count * nota;
                }
            }

            _provaRepository.Adicionar(prova);

            await _provaRepository.SaveChangesAsync();

            return prova.Id;
        }
    }
}
