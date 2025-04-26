using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class FromResult
    {
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; } = [];
    }
}
