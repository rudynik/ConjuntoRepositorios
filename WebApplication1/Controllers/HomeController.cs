using GitHub.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly GitApiClient _client;
		private readonly ApplicationDbContext _context;

		public HomeController(ILogger<HomeController> logger, GitApiClient client, ApplicationDbContext context)
		{
			_logger = logger;
			_client = client;
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<JsonResult> Git()
		{
			var repositorios = await _client.GetRepositoriosAsync();

			repositorios.ToList().ForEach(repositorio =>
				repositorio.git_url = repositorio.git_url.Substring(6));

			return Json(repositorios.OrderByDescending(x => x.name));
		}

		public async Task<IActionResult> Repo(string nome)
		{
			var repo = _context.Repositorios.Include(x => x.owner)
											.ToList()
											.Where(x => x.name == nome)
											.FirstOrDefault();

			var repositorio = await _client.GetRepoAsync(nome);
			repositorio.updated_at = repositorio.updated_at.Substring(0, 10);

			if (repo != null)
			{
				repo.updated_at = repositorio.updated_at;
				return View(repo);
			}

			return View(repositorio);
		}

		public async Task<JsonResult> Contribuicoes(string nome)
		{
			var contribuidores = await _client.GetContribuidoresAsync(nome);

			return Json(contribuidores.ToList());
		}

		public async Task Favorito(string nome, bool favorito)
		{
			var repositorio = _context.Repositorios.ToList()
												   .Where(x => x.name == nome)
												   .FirstOrDefault();
			try
			{
				if (repositorio == null)
				{
					var novoRepositorio = await _client.GetRepoAsync(nome);


					novoRepositorio.Favorito = favorito;
					novoRepositorio.updated_at = novoRepositorio.updated_at.Substring(0, 10);
					_context.Repositorios.Add(novoRepositorio);					
					await _context.SaveChangesAsync();
				}
				else
				{
					repositorio.Favorito = favorito;
					_context.Repositorios.Update(repositorio);
					await _context.SaveChangesAsync();
				}
			}
			catch (Exception e)
			{
				var teste = e.Message;
			}
		}

		public IActionResult Favoritos()
		{
			var repositorios = _context.Repositorios.Include(x => x.owner)
													.ToList()
													.Where(x => x.Favorito)
													.OrderBy(x => x.name);

			return View(repositorios);
		}

		public IActionResult Delete(int id)
		{
			var repositorio = _context.Repositorios.ToList()
												   .Where(x => x.Id == id)
												   .FirstOrDefault();
			if (repositorio != null)
			{
				repositorio.Favorito = false;
				_context.Repositorios.Update(repositorio);
				_context.SaveChanges();
			}

			return RedirectToAction("Favoritos");
		}
	}
}
