using System.Linq;
using LojaWebEF.Models;

namespace LojaWebEF.Dados
{
    public class IniciarBanco
    {
        public static void Inicializar(LojaContexto contexto)
        {
            //verifica se o banco de dados foi criado
            contexto.Database.EnsureCreated();

            /*Any verifica se tem algo dentro, ele retorna
            Se não tem, ele cria uma linha de dado que você precisa inserir
            */
            if (contexto.Cliente.Any())
            {
                return;
            }

            //inserindo dados na tabela Cliente
            var cliente = new Cliente()
            { Nome = "Pedro", Email = "pedro@terra.com.br", Idade = 23 };
            contexto.Cliente.Add(cliente);

            /*inserindo dados na tabela Produto - usamos 20.23M, pois tratá-se de decimal*/
            var produto = new Produto()
            {
                NomeProduto = "mouse",
                Descricao = "mouse Microsoft",
                Preco = 20.62M,
                Quantidade = 10
            };
            contexto.Produto.Add(produto);

            //inserindo dados no tabela Pedido
            var pedido = new Pedido()
            {
                //setando apenas a quantidade, pois os outros dados são "herdados" das outras tabelas PFK
                IdCliente = cliente.IdCliente,IdProduto = produto.IdProduto,Quantidade = 2
            };
            contexto.Pedido.Add(pedido);

            //salvando as modificações
            contexto.SaveChanges();

        }
    }
}