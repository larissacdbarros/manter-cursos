using ManterCursos.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace ManterCursos.API.Controllers
{
    [ApiController]
    [Route("api/usurios")]
    public class Usuarios : ControllerBase
    {
        private readonly DataContext _context;
        public int MyProperty { get; set; }
    }
}