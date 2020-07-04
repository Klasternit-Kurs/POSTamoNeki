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
		static List<Racun> Racuni = new List<Racun>();

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
					case "2":
						Ispis();
						break;
					case "3":
						Brisanje();
						break;
					case "5":
						Izdavanje();
						break;
					case "6":
						IspisRacuna();
						break;
					case "7":
						Console.WriteLine("Bye :) ");
						Console.ReadKey();
						break;
				}
			} while (unos != "7");
		}

		static void Izdavanje()
		{
			//TODO Kao i za unos artikla, ne izbacivati korisnika na meni kada pogresi
			//Takdoje, nemamo nikakvu kontrolu da li uposte ima kolicine koja se trazi
			//Tip: Mozda da, po unosu sifre, prikazemo trenutnu kolicinu odmah? :) 
			Racun r = new Racun();
			r.Rbr = (uint)Racuni.Count + 1;

			Console.Write("Unesite sifru artikla: ");
			string sifra = Console.ReadLine();

			foreach (Artikal a in Artikli)
			{
				if (a.Sifra == sifra)
				{
					r.Art = a;
					Console.Write("Unesite kolicinu: ");
					r.Kolicina = int.Parse(Console.ReadLine());
					a.Kolicina -= r.Kolicina;
					Racuni.Add(r);
					return;
				}
			}
			Console.WriteLine("Nema te sifre :(");
		}

		static void Brisanje()
		{
			Console.Write("Unesite sifru: ");
			string sifra = Console.ReadLine();

			Artikal zaBrisanje = null;
			foreach (Artikal a in Artikli)
			{
				if (a.Sifra == sifra)
				{
					zaBrisanje = a;
					break;
				}
			}

			if (Artikli.Remove(zaBrisanje))
			{
				Console.WriteLine("Uspesno obrisano!");
			} else
			{
				Console.WriteLine("Sifra nije pronadjena!");
			}
		}

		static void Ispis()
		{
			Console.WriteLine("=============================");
			foreach (Artikal a in Artikli)
			{
				Console.WriteLine($"{a.Sifra} -- {a.Naziv} {a.Ucena} {a.MarzaProc}% {a.Kolicina} {a.IzracunajIzlaznu()}");
			}
			Console.WriteLine("=============================");
		}

		static void IspisRacuna()
		{
			Console.WriteLine("=============================");
			foreach (Racun r in Racuni)
			{
				Console.WriteLine($"{r.Rbr} -- {r.Art.Naziv} {r.Kolicina} {r.Art.IzracunajIzlaznu()} {r.DajTotal()}");
			}
			Console.WriteLine("=============================");
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

			Console.Write("Unesite ulaznu cenu: ");
			decimal ucena = decimal.Parse(Console.ReadLine());

			Console.Write("Unesite marzu: ");
			int marza = int.Parse(Console.ReadLine());

			Console.Write("Unesite kolicinu: ");
			int kolicina = int.Parse(Console.ReadLine());

			Artikli.Add(new Artikal(sifra, naziv, ucena, marza, kolicina));
		}

		static void Meni()
		{
			Console.WriteLine("1 - Dodavanje");
			Console.WriteLine("2 - Stampanje");
			Console.WriteLine("3 - Brisanje");
			Console.WriteLine("4 - Izmena"); // TODO Domaci :P 
											 //Napraviti izmenu tako da korisnik unese sifru za artikal. Ako smo ga nasli, ponuditi korisniku
											 //da izmeni naziv, ulaznu cenu, marzu ili kolicinu.
			Console.WriteLine("5 - Racun");
			Console.WriteLine("6 - Izlistaj racune");
			Console.WriteLine("7 - Izlaz");
			Console.WriteLine("------------------");
			Console.Write("Izbor: ");
		}
	}


	class Racun
	{
		public uint Rbr;  

		public Artikal Art; 
		public int Kolicina;
		
		public decimal DajTotal()
		{
			return Art.IzracunajIzlaznu() * Kolicina;
		}
	}

	class Artikal
	{
		public string Sifra;
		public string Naziv;
		public decimal Ucena; 
		public int MarzaProc;
		public int Kolicina;
		                                
		public decimal IzracunajIzlaznu()
		{
			return Ucena * (1 + (decimal)MarzaProc / 100);
		}

		public Artikal(string s, string n, decimal c, int m, int k)
		{
			Sifra = s;
			Naziv = n;
			Ucena = c;
			MarzaProc = m;
			Kolicina = k;
		}
	}
}
