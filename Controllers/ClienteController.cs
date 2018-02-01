using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LojaWebEF.Dados;
using LojaWebEF.Models;

namespace LojaWebEF.Controllers
{
    [Route("api/[controller]")]
    public class ClienteController : Controller
    {
        Cliente cliente = new Cliente();

        /*disponibilizar o contexto, sem setar nada para dentro dele, sem atribuir valores a ele
        será usado com uma CONSTANTE, por isso p readonly (somente leitura)*/
        readonly LojaContexto contexto;

        public ClienteController(LojaContexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        //o retorno será de vários clientes, por isso o IEnumerable
        public IEnumerable<Cliente> Listar()
        {
            //o equivalente ao SELECT do banco de dados
            return contexto.Cliente.ToList();
        }

        [HttpGet("{id}")]
        //o retorno será apenas 1 cliente
        public Cliente Listar(int id)
        {
            //especificando 1 individuo, por isso o FirstOrDefault
            return contexto.Cliente.Where(x => x.IdCliente == id).FirstOrDefault();
        }

        [HttpPost]
        //usando void, para nao ter retorno, normalmente podemos usar IActionResult
        public void Cadastrar([FromBody]Cliente cli)
        {
            //adicionando
            contexto.Cliente.Add(cli);
            //salvando
            contexto.SaveChanges();
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody]Cliente cliente)
        {

            if (cliente == null || cliente.IdCliente != id)
            {
                return BadRequest();
            }

            var cli = contexto.Cliente.FirstOrDefault(x=>x.IdCliente==id);
            if(cli==null)
            return NotFound();

            cli.IdCliente = cliente.IdCliente;
            cli.Nome = cliente.Nome;
            cli.Email = cliente.Email;
            cli.Idade = cliente.Idade;

            /*Não colocar pedido, pois é PFK 
            cli.Pedido = cliente.Pedido;*/

            contexto.Cliente.Update(cli);
            int rs = contexto.SaveChanges();

            if(rs>0)
            return Ok();
            else
            return BadRequest();

        }

        [HttpDelete("{id}")]
        public IActionResult Apagar(int id){
            var Cliente = contexto.Cliente.Where(x=>x.IdCliente==id).FirstOrDefault();
            if(cliente == null){
                return NotFound();
            }
            contexto.Cliente.Remove(cliente);
            int rs = contexto.SaveChanges();
            if(rs>0)
            return Ok();
            else
            return BadRequest();
        }

    }
}