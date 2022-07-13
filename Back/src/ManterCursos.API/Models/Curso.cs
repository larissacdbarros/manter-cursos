using System;
using ManterCursos.API.Models.DTO;

namespace ManterCursos.API.Models
{
    public class Curso
    {
    
        public int CursoId { get; set; }
        public string Descricao{ get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public int? QtdAlunosTurma { get; set; }
        public int CategoriaCursoId { get; set; }
        public CategoriaCurso CategoriaCurso { get; set; }
        public bool status { get; set; }

        public Curso(){}

        public Curso(CursoReqDTO dto)
        {
            this.Descricao = dto.Descricao;
            this.DataInicio = dto.DataInicio;
            this.DataTermino = dto.DataTermino;
            this.QtdAlunosTurma = dto.QtdAlunosTurma;
            this.CategoriaCursoId = dto.CategoriaCursoId;
        }

    }

}