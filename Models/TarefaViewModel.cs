using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarefasMvc.Models.Enuns;

namespace TarefasMvc.Models
{
    public class TarefaViewModel
    {
       public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;  
        public string Categoria { get; set; } = string.Empty;     
        public string Descricao { get; set; } = string.Empty;    
        public DateTime DataCriacao  { get; set; }
        public DateTime DataExpiracao  { get; set; }
        public TipoPrioridadeEnum Prioridade { get; set; }
        public TipoStatusEnum Status { get; set; }
        public byte[]? FotoTarefa { get; set; }               
       
    }
}