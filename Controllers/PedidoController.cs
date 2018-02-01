using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LojaWebEF.Dados;
using LojaWebEF.Models;

namespace LojaWebEF.Controllers
{
     [Route("api/[controller]")]
    public class PedidoController : Controller
    {
        Pedido pedido = new Pedido();

        /*disponibilizar o contexto, sem setar nada para dentro dele, sem atribuir valores a ele
        será usado com uma CONSTANTE, por isso p readonly (somente leitura)*/
        readonly LojaContexto contexto;

        public PedidoController(LojaContexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        //o retorno será de vários clientes, por isso o IEnumerable
        public IEnumerable<Pedido> Listar()
        {
            //o equivalente ao SELECT do banco de dados
            return contexto.Pedido.ToList();
        }

        [HttpGet("{id}")]
        //o retorno será apenas 1 cliente
        public Pedido Listar(int id)
        {
            //especificando 1 individuo, por isso o FirstOrDefault
            return contexto.Pedido.Where(x => x.IdPedido == id).FirstOrDefault();
        }

        [HttpPost]
        //usando void, para nao ter retorno, normalmente podemos usar IActionResult
        public void Cadastrar([FromBody]Pedido pedido)
        {
            //adicionando
            contexto.Pedido.Add(pedido);
            //salvando
            contexto.SaveChanges();
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody]Pedido pedido)
        {

            if (pedido == null || pedido.IdPedido != id)
            {
                return BadRequest();
            }

            var ped = contexto.Pedido.FirstOrDefault(x=>x.IdPedido==id);
            if(ped==null)
            return NotFound();

            ped.IdPedido = pedido.IdPedido;
            ped.IdProduto = pedido.IdProduto;
            ped.IdCliente = pedido.IdCliente;
            ped.Produto = pedido.Produto;
            ped.Quantidade = pedido.Quantidade;
            
            contexto.Pedido.Update(ped);
            int rs = contexto.SaveChanges();

            if(rs>0)
            return Ok();
            else
            return BadRequest();

        }

        [HttpDelete("{id}")]
        public IActionResult Apagar(int id){
            var pedido = contexto.Pedido.Where(x=>x.IdPedido==id).FirstOrDefault();
            if(pedido== null){
                return NotFound();
            }
            contexto.Pedido.Remove(pedido);
            int rs = contexto.SaveChanges();
            if(rs>0)
            return Ok();
            else
            return BadRequest();
        }

    }
}