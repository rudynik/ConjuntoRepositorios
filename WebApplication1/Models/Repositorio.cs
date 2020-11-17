using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Repositorio
    {
		public int Id { get; set; }
        [Display(Name ="Nome")]        
        public string name { get; set; }
        [Display(Name = "Ultima Atualização")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public string updated_at { get; set; }
        [Display(Name = "Nome Completo")]
        public string full_name { get; set; }
        [Display(Name = "Descrição")]
        public string description { get; set; }
        [Display(Name = "Linguagem")]
        public string language { get; set; }
        [Display(Name = "Git Url")]
        public string git_url { get; set; }
        [Display(Name = "Branch")]
        public string default_branch { get; set; }
        [Display(Name = "Adicionar aos Favoritos")]
        public bool Favorito { get; set; }

        public Owner owner { get; set; }
    }

    public class Owner
    {
		public int Id { get; set; }
        [Display(Name = "Proprietário")]
        public string login { get; set; }       
        
        public int RepositorioId { get; set; }
		public Repositorio Repositorio { get; set; }
	}
}


