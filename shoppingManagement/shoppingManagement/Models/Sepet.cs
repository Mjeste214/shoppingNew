using System;
using System.Collections.Generic;

namespace shoppingManagement.Models
{
    public partial class Sepet
    {
        public Sepet()
        {
            SepetUrun = new HashSet<SepetUrun>();
        }

        public int Id { get; set; }
        public int? MusteriId { get; set; }

        public Musteri Musteri { get; set; }
        public ICollection<SepetUrun> SepetUrun { get; set; }
    }
}
