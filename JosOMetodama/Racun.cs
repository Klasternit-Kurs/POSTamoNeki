using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JosOMetodama
{
	class Racun
	{
		public uint Rbr;

		public Dictionary<Artikal, int> ArtikliKolicine = new Dictionary<Artikal, int>();

		public decimal DajTotal()
		{
			decimal total = 0;
			foreach(Artikal a in ArtikliKolicine.Keys)
			{
				total += a.IzracunajIzlaznu() * ArtikliKolicine[a];
			}
			return total;
		}
	}
}
