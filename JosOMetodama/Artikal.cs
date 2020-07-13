using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JosOMetodama
{
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

		public Artikal(){}
	}
}
