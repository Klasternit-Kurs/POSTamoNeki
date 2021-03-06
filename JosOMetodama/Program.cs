﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JosOMetodama
{

	//TODO napraviti novi imenik koristeci objekte i recnike :) 
	//Mozete da napravite i drugu verziju u kojoj bi osoba jednostavno sadrzavala objekat
	//klase KontaktInformacije, a u tom objektu bi bile sve potrebe informacije za kontakt
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


			if (File.Exists("artikli.txt"))
			{
				foreach(string red in File.ReadLines("artikli.txt"))
				{
					Artikal noviArt = new Artikal();

					string[] artikalIzTeksta = red.Split(';');
					//0    1      2  3    4
					//1; Plazam; 45; 20; 10
					noviArt.Sifra = artikalIzTeksta[0];
					noviArt.Naziv = artikalIzTeksta[1];
					noviArt.Ucena = decimal.Parse(artikalIzTeksta[2]);
					noviArt.MarzaProc = int.Parse(artikalIzTeksta[3]);
					noviArt.Kolicina = int.Parse(artikalIzTeksta[4]);
					Artikli.Add(noviArt);
				}
			}

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
					case "4":
						Izmena();
						break;
					case "5":
						Izdavanje();
						break;
					case "6":
						IspisRacuna();
						break;
					case "7":
						Console.WriteLine("Bye :) ");
						Cuvanje();
						Console.ReadKey();
						break;
				}
			} while (unos != "7");
		}

		static void Cuvanje()
		{
			File.Delete("artikli.txt");
			foreach(Artikal a in Artikli)
			{
				File.AppendAllText("artikli.txt", 
					$"{a.Sifra};{a.Naziv};{a.Ucena};{a.MarzaProc};{a.Kolicina}" + Environment.NewLine);
			}
		}

		static void Izmena()
		{
			Console.Write("Unesite sifru artikla: ");
			string unos = Console.ReadLine();
			Console.WriteLine();
			foreach(Artikal a in Artikli)
			{
				if (a.Sifra == unos)
				{
					Console.Write("Unesite novo ime ili nista ako ga ne menjate: ");
					unos = Console.ReadLine();
					if (unos != "")
					{
						a.Naziv = unos;
					}
					Console.Write("Unesite novu ulaznu cenu ili nista ako ga ne menjate: ");
					unos = Console.ReadLine();
					if (unos != "")
					{
						a.Ucena = decimal.Parse(unos);
					}
					Console.Write("Unesite novu marzu ili nista ako ga ne menjate: ");
					unos = Console.ReadLine();
					if (unos != "")
					{
						a.MarzaProc = int.Parse(unos);
					}
					return;
				}
			}
			Console.WriteLine("Artikal sa tom sifrom ne postoji");
		}

		static void Izdavanje()
		{
			//TODO Popraviti bugove u metodi :D

			//TODO Ako unesemo artikal koji vec postoji na racunu, treba da samo uvecamo kolicinu 
			//vec postojeceg a nikako da dodamo jos jednom

			//TODO Kao i za unos artikla, ne izbacivati korisnika na meni kada pogresi
			//Takdoje, nemamo nikakvu kontrolu da li uposte ima kolicine koja se trazi
			//Tip: Mozda da, po unosu sifre, prikazemo trenutnu kolicinu odmah? :) 
			Racun r = new Racun();
			r.Rbr = (uint)Racuni.Count + 1;
			
			do
			{
				Console.Write("Unesite sifru artikla: ");
				string sifra = Console.ReadLine();
				bool NadjenArtikal = false;
				foreach (Artikal a in Artikli)
				{
					if (a.Sifra == sifra)
					{
						NadjenArtikal = true;
						/*
						int indeksPostojeceg = -1;
						for (int indeks = 0; indeks < r.Art.Count; indeks++)
						{
							if (r.Art[indeks] == a)
							{
								indeksPostojeceg = indeks;
								break;
							}
						}

						if (indeksPostojeceg == -1)
						{
							r.Art.Add(a);
						}*/

						do
						{
							Console.Write($"Unesite kolicinu(na stanju {a.Kolicina}): ");
							int kol = int.Parse(Console.ReadLine());

							if (kol <= a.Kolicina && kol > 0)
							{
								if (r.ArtikliKolicine.ContainsKey(a))
								{
									r.ArtikliKolicine[a] += kol;
								} else
								{
									r.ArtikliKolicine.Add(a, kol);
								}
								/*if (indeksPostojeceg == -1)
								{
									r.Kolicina.Add(kol);
								}else
								{
									r.Kolicina[indeksPostojeceg] += kol;
								}*/
								a.Kolicina -= kol;
								break;
							}
							Console.WriteLine("Pogresna kolicina!");
						} while (true);

						//a.Kolicina = r.Kolicina[r.Kolicina.Count - 1]; Uzimamo zadnju stvar sa liste

						Console.Write("Unosite jos artikala? (d/n): ");
						string unos = Console.ReadKey().KeyChar.ToString();
						Console.WriteLine();
						if (unos == "n")
						{
							Racuni.Add(r);
							return;
						}
					}
				}


				//Inverzija, logicko ne ili NOT !
				//
				//   A   |   !A
				//----------------------
				//   T   |   F
				//   F   |   T
				if (!NadjenArtikal)
				{
					Console.WriteLine("Nema te sifre :(");
				}
			} while (true);  
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
				Console.WriteLine($"Rbr. : {r.Rbr}");
				Console.WriteLine("-------------------------");
				//for (int indeks = 0; indeks < r.Art.Count; indeks++ )
				//{
				//	Console.WriteLine($"|{r.Art[indeks].Sifra}-{r.Art[indeks].Naziv}|{r.Kolicina[indeks]}|{r.Art[indeks].IzracunajIzlaznu() * r.Kolicina[indeks]}");
				//}

				foreach(Artikal a in r.ArtikliKolicine.Keys)
				{
					Console.WriteLine($"|{a.Sifra}-{a.Naziv}|{r.ArtikliKolicine[a]}|{a.IzracunajIzlaznu() * r.ArtikliKolicine[a]}");
				}
				Console.WriteLine("-------------------------");
				Console.WriteLine($"Total: {r.DajTotal()}");
				Console.WriteLine("-------------------------");
			}
			Console.WriteLine("=============================");
		}

		static void Unos()
		{
			//TODO Za domaci napraviti unos da je kulturan :) tj, da korisnika ne izbacuje na glavni meni
			//nego da mu kaze da je pogresio i ponudi mu da ponovo unese istu stvar
			string sifra;
			do
			{
				Console.Write("Unesite novu sifru: ");
				sifra = Console.ReadLine();

				bool PravilnaSifra = true;
				foreach (Artikal a in Artikli)
				{
					if (a.Sifra == sifra)
					{
						Console.WriteLine("Jok :(");
						PravilnaSifra = false;
					}
				}
				if (PravilnaSifra)
				{
					break;
				}
				Console.WriteLine("Sifra vec postoji");

			} while (true);


			Console.Write("Unesite novi naziv: ");
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


	
}
