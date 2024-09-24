using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntitiesDTOs
{
    public class UpdateBookImageDTO
    {
        public int BookId { get; set; }
        public string? Image { get; set; }
    }
}
