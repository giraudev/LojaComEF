using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LojaWebEF.Dados;
using LojaWebEF.Models;

namespace LojaWebEF.Controllers
{
    [Route("api/[controller]")]
    public class ProdutoController:Controller
    {
        Produto produto = new Produto();

        readonly LojaContexto contexto;

        public ProdutoController (LojaContexto contexto){
            this.contexto = contexto;
        }

        [HttpGet]
        //o retorno será de vários produtos, por isso o IEnumerable
        public IEnumerable<Produto> Listar()
        {
            //o equivalente ao SELECT do banco de dados
            return contexto.Produto.ToList();
        }

        [HttpGet("{id}")]
        //o retorno será apenas 1 produto
        public Produto Listar(int id)
        {
            //especificando 1 individuo, por isso o FirstOrDefault
            return contexto.Produto.Where(x => x.IdProduto == id).FirstOrDefault();
        }

         [HttpPost]
        //usando void, para nao ter retorno, normalmente podemos usar IActionResult
        public void Cadastrar([FromBody]Produto produto)
        {
            //adicionando
            contexto.Produto.Add(produto);
            //salvando
            contexto.SaveChanges();
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody]Produto produto)
        {

            if (produto == null || produto.IdProduto != id)
            {
                return BadRequest();
            }

            var prod = contexto.Produto.FirstOrDefault(x=>x.IdProduto==id);
            if(prod==null)
            return NotFound();

            prod.NomeProduto = produto.NomeProduto;
            prod.Descricao = produto.Descricao;
            prod.Preco = produto.Preco;
            produto.Quantidade = produto.Quantidade;

                   
            contexto.Produto.Update(prod);
            int rs = contexto.SaveChanges();

            if(rs>0)
            return Ok();
            else
            return BadRequest();

        }

        
    }
}