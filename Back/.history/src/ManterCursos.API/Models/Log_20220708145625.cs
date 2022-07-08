using System;

namespace ManterCursos.API.Models
{
    public class Log
    {
         public int LogId { get; set; }
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAtualizacao { get; set; } //nullable
        public int UsuarioId{ get; set; }
        public Usuario Usuario { get; set; }
    }
}