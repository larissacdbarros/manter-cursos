using ManterCursos.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ManterCursos.API.Data
{
    public class DataContext
    {
        public DbSet<Curso> Cursos {get; set;}
        public DbSet<Log> Logs {get; set;}
        public DbSet<CategoriaCurso> CategoriasCursos {get; set;}
        public DbSet<Usuario> Cursos {get; set;}
    }
}