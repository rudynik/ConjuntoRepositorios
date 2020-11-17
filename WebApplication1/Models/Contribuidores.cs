using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GitHub.Models
{
    public class Contribuidores
    {
        [Display(Name = "Proprietário")]
        public string login { get; set; }
        [Display(Name = "Tipo")]
        public string type { get; set; }
        [Display(Name = "Contribuições")]
        public int contributions { get; set; }
    }
}
