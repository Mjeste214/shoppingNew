using System;
using System.Collections.Generic;

namespace shoppingManagement.Models
{
    public partial class Musteri
    {
        public Musteri()
        {
            Sepet = new HashSet<Sepet>();
        }

        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Sehir { get; set; }

        public ICollection<Sepet> Sepet { get; set; }
    }
}
