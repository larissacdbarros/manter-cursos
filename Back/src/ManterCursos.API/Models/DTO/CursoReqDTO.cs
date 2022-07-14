using System;

namespace ManterCursos.API.Models.DTO
{
    public class CursoReqDTO
    {

        public string Descricao{ get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public int? QtdAlunosTurma { get; set; }
        public int CategoriaCursoId { get; set; }
    }
}