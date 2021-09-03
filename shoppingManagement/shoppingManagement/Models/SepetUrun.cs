using System;
using System.Collections.Generic;

namespace shoppingManagement.Models
{
    public partial class SepetUrun
    {
        public int Id { get; set; }
        public int? SepetId { get; set; }
        public decimal Tutar { get; set; }
        public string Aciklama { get; set; }

        public Sepet Sepet { get; set; }
    }
}
