using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManterCursos.API.Data;
using ManterCursos.API.Models;
using ManterCursos.API.Models.DTO;
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
            return await _context.Cursos
            .Include(curso => curso.CategoriaCurso)
            .ToListAsync();
        }

        [HttpGet("statusValido")]
        public async Task<ActionResult<IEnumerable<Curso>>> GetAllStatusValido()
        {
            return await _context.Cursos
            .Include(curso => curso.CategoriaCurso)
            .Where(curso => curso.status == true)
            .ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetById(int id)
        {
            Curso curso = await _context.Cursos.FindAsync(id);

            CategoriaCurso categoriaCurso = await _context.CategoriasCursos.FindAsync(curso.CategoriaCursoId);
            curso.CategoriaCurso = categoriaCurso;

            if (curso == null)
            {
                return NotFound();
            }
            return curso;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Curso body)
        {
            //Ao atualizar o curso o log deverá adicionar que teve uma alteração.
            //reforçar categoria curso

            Curso curso = await _context.Cursos.FindAsync(id);
            
            if(curso == null){
                return NotFound();
            }

            body.CursoId = id ;
            body.status = true;

            bool isDataValida = VerificaData(body);

            CategoriaCurso categoriaCurso = await _context.CategoriasCursos.FindAsync(body.CategoriaCursoId);
            curso.CategoriaCurso = categoriaCurso;

            if(isDataValida){

                // Ao alterar o curso o log é atualizado
                var log = _context.Logs
                        .Where(log => log.CursoId == id)
                        .FirstOrDefault();  

                if(log == null){
                    return NotFound();
                } 

                log.DataAtualizacao = DateTime.Now;

                _context.Entry<Curso>(curso).State = EntityState.Detached;
                _context.Entry<Curso>(body).State = EntityState.Modified;

                _context.Entry<Log>(log).State = EntityState.Detached;
                _context.Entry<Log>(log).State = EntityState.Modified;

                _context.Cursos.Update(body);
                _context.Logs.Update(log);
                await _context.SaveChangesAsync();

                return Ok(body);

            }else{
                return  Problem(null, null, 422, "Data inválida!", null);
            }

        }

        [HttpPost]
        public async Task<ActionResult<Curso>> Create(CursoReqDTO body)
        {
            Curso curso = new Curso(body);

            curso.status = true;

            CategoriaCurso categoriaCurso = await _context.CategoriasCursos.FindAsync(curso.CategoriaCursoId);
            curso.CategoriaCurso = categoriaCurso;

            //Inclusão de cursos no mesmo período não é permitida
            // Não é permitida a inclsão de cursos com data de início menor que a data atual
            bool isDataValida = VerificaData(curso);

            if(isDataValida){
                await _context.Cursos.AddAsync(curso);
                await _context.SaveChangesAsync();

                // Ao incluir um novo curso o log é criado

                Log log = new Log();

                Usuario usuario = await _context.Usuarios.FindAsync(1);//admin
                log.UsuarioId = 1;
                log.Usuario = usuario;
                
                log.DataInclusao = DateTime.Now;
                log.CursoId = curso.CursoId;
                log.Curso = curso;

                await _context.Logs.AddAsync(log);
                await _context.SaveChangesAsync();

                return Ok(curso);

            }else{
                return  Problem(null, null, 422, "Data inválida!", null); 
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // alterar aqui para deleção logica, na verdade será um PUT

            //VALIDAÇÕES
            //Cursos já realizados não poderão ser deletados    

            // se um curso for 

            Curso result = await _context.Cursos.FindAsync(id);
            if (result == null ){
                return NotFound (); 
            }
            _context.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();

        }

        [HttpPut("delete/{id}")]
        public async Task<IActionResult> DelecaoLogica(int id)
        {
            Curso curso = await _context.Cursos.FindAsync(id);
            
            if(curso == null){
                return NotFound();
            }

            // não permite a deleção de cursos que já estão ocorrendo e que já acabaram
            if (curso.DataInicio > DateTime.Now.Date)
            {
                curso.status = false;
         
                CategoriaCurso categoriaCurso = await _context.CategoriasCursos.FindAsync(curso.CategoriaCursoId);
                curso.CategoriaCurso = categoriaCurso;


                // Ao alterar o curso o log é atualizado
                var log = _context.Logs
                        .Where(log => log.CursoId == id)
                        .FirstOrDefault();  

                if(log == null){
                    return NotFound();
                } 

                log.DataAtualizacao = DateTime.Now;

                _context.Entry<Curso>(curso).State = EntityState.Detached;
                _context.Entry<Curso>(curso).State = EntityState.Modified;

                _context.Entry<Log>(log).State = EntityState.Detached;
                _context.Entry<Log>(log).State = EntityState.Modified;

                _context.Cursos.Update(curso);
                _context.Logs.Update(log);
                await _context.SaveChangesAsync();

                return Ok(curso);
                
            }else{
                return  Problem(null, null, 422, "Não é possível deletar este curso!", null); 
            }

            
        }

        private bool VerificaData(Curso curso){
            //buscar se o curso que está sendo agendado é para o mesmo período
            var result =  _context.Cursos
                    .Where(c => curso.CursoId != c.CursoId 
                            && ((curso.DataInicio >= c.DataInicio && curso.DataTermino <= c.DataTermino) 
                            || (curso.DataInicio <= c.DataTermino && curso.DataTermino >= c.DataTermino)
                            || (curso.DataInicio <= c.DataInicio && curso.DataTermino >= c.DataInicio)))
                    .FirstOrDefault();

            if(result != null){
                return false;
            }

            DateTime dataAtual = DateTime.Now.Date;

            // validação para data de início menor que a data atual
            // validação para data de término maior que a data de início 
            return curso.DataInicio.Date > dataAtual && curso.DataTermino.Date > curso.DataInicio.Date;
        }
    }
}