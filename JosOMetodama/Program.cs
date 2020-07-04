using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JosOMetodama
{
	class Program
	{
		static List<Artikal> Artikli = new List<Artikal>();

		static void Main(string[] args)
		{
			//Artikal a = new Artikal();
			//a.Kolicina = 0;

			//checked     -- Stiti od overflowa :) 
			//{
			//	a.Kolicina -= 20;
			//}
			string unos;
			do
			{
				Meni();
				unos = Console.ReadKey().KeyChar.ToString();
				Console.WriteLine();
				switch (unos)
				{
					case "1":
						Unos();
						break;
					case "5":
						Console.WriteLine("Bye :) ");
						Console.ReadKey();
						break;
				}
			} while (unos != "5");
		}

		static void Unos()
		{
			//TODO Za domaci napraviti unos da je kulturan :) tj, da korisnika ne izbacuje na glavni meni
			//nego da mu kaze da je pogresio i ponudi mu da ponovo unese istu stvar
			Console.Write("Unesite novu sifru: ");
			string sifra = Console.ReadLine();

			foreach (Artikal a in Artikli)
			{
				if (a.Sifra == sifra)
				{
					Console.WriteLine("Jok :(");
					return;
				}
			}

			Console.Write("Unesite novu naziv: ");
			string naziv = Console.ReadLine();

			foreach (Artikal a in Artikli)
			{
				if (a.Naziv == naziv)
				{
					Console.WriteLine("Jok :(");
					return;
				}
			}

			//TODO cena i marza ne mogu da budu negativne, niti 0

			Console.WriteLine("Unesite ulaznu cenu: ");
			decimal ucena = decimal.Parse(Console.ReadLine());

			Console.WriteLine("Unesite marzu: ");
			int marza = int.Parse(Console.ReadLine());

			Artikli.Add(new Artikal(sifra, naziv, ucena, marza));
		}

		static void Meni()
		{
			Console.WriteLine("1 - Dodavanje");
			Console.WriteLine("2 - Stampanje");
			Console.WriteLine("3 - Brisanje");
			Console.WriteLine("4 - Izmena"); // TODO Domaci :P 
			Console.WriteLine("5 - Izlaz");
			Console.WriteLine("------------------");
			Console.Write("Izbor: ");
		}
	}

	class Artikal
	{
		public string Sifra;
		public string Naziv;
		public decimal Ucena; 
		public int MarzaProc;
		public int Kolicina;
		                                
		public void IzracunajIzlaznu()
		{
			Console.WriteLine($"Izlazna cena je {Ucena * (1 + ((decimal)MarzaProc / 100))}");
		}

		public Artikal(string s, string n, decimal c, int m)
		{
			Sifra = s;
			Naziv = n;
			Ucena = c;
			MarzaProc = m;
		}
	}
}
