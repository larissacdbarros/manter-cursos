using System.Collections.Generic;
using System.Threading.Tasks;
using ManterCursos.API.Data;
using ManterCursos.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManterCursos.API.Controllers
{
    [ApiController]
    [Route("api/cursos")]
    public class CursosController: ControllerBase
    {
        private readonly DataContext _context;
        public CursosController(DataContext context)
        {
            this._context = context; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> GetAll()
        {
            return await _context.Cursos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetById(int id)
        {
            Curso result = await _context.Cursos.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Curso body)
        {
            //Ao atualizar o curso o log deverá adicionar que teve uma alteração.

            Curso result = await _context.Cursos.FindAsync(id);
            if(result == null){
                return NotFound();
            }

            body.CursoId = id ;

            _context.Entry<Curso>(result).State = EntityState.Detached;
            _context.Entry<Curso>(body).State = EntityState.Modified;
            
            _context.Cursos.Update(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpPost]
        public async Task<ActionResult<Curso>> Create(Curso body)
        {

            //VALIDAÇÕES
            //Inclusão de cursos no mesmo período não é permitida
            // Não é permitida a inclsão de cursos com data de início menor que a data atual

            // Ao incluir um novo curso deverá ser criado o log


            await _context.Cursos.AddAsync(body);
            await _context.SaveChangesAsync();

            return Ok(body);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // alterar aqui para deleção logica, na verdade será um PUT

            //VALIDAÇÕES
            //Cursos já realizados não poderão ser deletados    

            Curso result = await _context.Cursos.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}