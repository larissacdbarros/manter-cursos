using ManterCursos.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ManterCursos.API.Data
{
    public class DataContext
    {
        public DbSet<Curso> Cursos {get; set;}
    }
}