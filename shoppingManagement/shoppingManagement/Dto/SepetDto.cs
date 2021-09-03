using shoppingManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shoppingManagement.Dto
{
    public class SepetDto
    {
        public int Id { get; set; }
        public int? MusteriId { get; set; }
        public Musteri Musteri { get; set; }
        public ICollection<SepetUrun> SepetUrun { get; set; }
    }
}

