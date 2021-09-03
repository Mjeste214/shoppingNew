using shoppingManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shoppingManagement.Dto
{
    public class SepetUrunDto
    {
        public int Id { get; set; }
        public int? SepetId { get; set; }
        public decimal? Tutar { get; set; }
        public string Aciklama { get; set; }
        public Sepet Sepet { get; set; }
    }
}
