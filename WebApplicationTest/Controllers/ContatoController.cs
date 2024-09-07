using Microsoft.AspNetCore.Mvc;
using WebApplicationTest.Models;
using WebApplicationTest.Repositorio;

namespace WebApplicationTest.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        public ContatoController(IContatoRepositorio contatoRepositorio) 
        {
            _contatoRepositorio = contatoRepositorio;
        }

        public IActionResult Index()
        {
            var contatos = _contatoRepositorio.BuscarTodos();
            return View(contatos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _contatoRepositorio.Apagar(id);

                if (apagado) 
                {
                    TempData["MensagemSucesso"] = "Contato Apagado com Sucesso";
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, não foi possível apagar o contato.";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex) 
            {
                TempData["MensagemErro"] = $"Ops, não foi possível apagar o contato.\n{ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato Cadastrado com Sucesso";
                    return RedirectToActionPermanent("Index");
                }

                return View(contato);
            }
            catch (Exception ex) 
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o seu contato, tente novamente.\n{ex.Message}";
                return RedirectToActionPermanent("Index");
            }
        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato Atualizado com Sucesso";
                    return RedirectToActionPermanent("Index");
                }

                return View("Editar", contato);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizar o seu contato, tente novamente.\n{ex.Message}";
                return RedirectToActionPermanent("Index");
            }  
        }
    }
}
