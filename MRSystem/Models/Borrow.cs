using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRSystem.Models
{
    public class Borrow
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int CustomerId { get; set; }
        public string MovieTitle { get; set; } = null!;
        public int UserCardNumber { get; set; }

        public DateTime MovieLend { get; set; }
        public DateTime? MovieReturn { get; set; }
    }
}
