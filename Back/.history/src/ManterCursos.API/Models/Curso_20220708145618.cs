using System;

namespace ManterCursos.API.Models
{
    public class Curso
    {
        public int CursoId { get; set; }
        public string Descricao{ get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public int? QtdAlunosTurma { get; set; }
        public CategoriaCurso CatgeoriaCursoId { get; set; }
        public CategoriaCurso CatgeoriaCurso { get; set; }
        public bool status { get; set; }
    }
}