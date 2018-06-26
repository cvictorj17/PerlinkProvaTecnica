using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerlinkProvaTecnica.Models;

namespace PerlinkProvaTecnica.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult VerificaNumFelizSortudo(IFormCollection form)
        {
            var numero = form["txtNumero"];

            string msg = string.Empty;

            if (NumeroSortudo(numero))
                msg = "Número Sortudo ";
            else
                msg = "Número Não-Sortudo ";

            if (NumeroFeliz(int.Parse(numero)))
                msg += "e Feliz.";
            else
                msg += "e Não-Feliz.";

            ViewData["Resultado"] = numero + " - " + msg;

            return View("Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Calculo Numeros Felizes Sortudos

        public static bool NumeroSortudo(string numero)
        {

            IList<int> NumerosSortudos = ObtemNumerosSortudos();

            if (NumerosSortudos.IndexOf(int.Parse(numero)) != -1)
                return true;
            else
                return false;

        }

        public static IList<int> ObtemNumerosSortudos()
        {
            IList<int> lstInteiros = new List<int>();

            for (int i = 1; i <= 100; i++)
                lstInteiros.Add(i);

            //Remove números pares
            foreach (var item in lstInteiros.ToList())
            {
                if ((item % 2) == 0)
                    lstInteiros.Remove(item);
            }

            IList<int> lstInteirosImpares = new List<int>();
            for (int i = 0; i < lstInteiros.Count; i++)
                lstInteirosImpares.Add(lstInteiros[i]);


            IList<int> NumerosSortudos = new List<int>();
            NumerosSortudos = lstInteirosImpares;

            int posicao = 1;
            for (int i = 0; i < NumerosSortudos.Count && posicao < NumerosSortudos.Count; i++)
            {
                NumerosSortudos = RemovePosicao(NumerosSortudos[posicao], NumerosSortudos);

                posicao += 1;

            }

            return NumerosSortudos;
        }

        public static IList<int> RemovePosicao(int posicao, IList<int> listNumerosSortudos)
        {
            IList<int> listaRemove = new List<int>();
            for (int i = 0; i < listNumerosSortudos.Count; i++)
                listaRemove.Add(listNumerosSortudos[i]);

            int pos = posicao;

            for (int i = 0; i < listNumerosSortudos.Count; i++)
            {
                if ((i + 1) == pos)
                {
                    listaRemove.Remove(listNumerosSortudos[i]);
                    pos += posicao;
                }
            }

            return listaRemove;
        }

        public static bool NumeroFeliz(int numero)
        {
            bool numeroFeliz = false;
            List<int> listaDigitos = new List<int>();
            listaDigitos = DividirDigitos(numero);
            for (int i = 0; i < 20 && !numeroFeliz; i++)
            {
                int sumaActual = CalcularQuadrados(listaDigitos);
                if (sumaActual != 1)
                    listaDigitos = DividirDigitos(sumaActual);
                else numeroFeliz = true;
            }
            return numeroFeliz;
        }

        public static List<int> DividirDigitos(int digito)
        {
            List<int> digitos = new List<int>();
            while (digito != 0)
            {
                digitos.Add(digito % 10);
                digito = digito / 10;
            }
            return digitos;
        }

        public static int CalcularQuadrados(List<int> listaDigitos)
        {
            int resultado = 0;
            foreach (int elem in listaDigitos) resultado += elem * elem;
            return resultado;
        }
        #endregion
    }
}
