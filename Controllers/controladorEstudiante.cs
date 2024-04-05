using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AcademicoAPI.Models;

namespace AcademicoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class controladorEstudiante : ControllerBase
    {
        public readonly BdAcademicoContext _bdAcademico;

        public controladorEstudiante(BdAcademicoContext _contex) { 

            _bdAcademico = _contex;
        }
        [HttpGet]
        [Route("Listar")]
        public IActionResult Listar() { 

            List<Estudiante> lista = new List<Estudiante>();
            try
            {
                lista = _bdAcademico.Estudiantes.ToList();
                return Ok(lista);
            }catch (Exception ex) { 

                return StatusCode(StatusCodes.Status200OK, new {msj= ex.Message,Response=lista});
            }
        }
        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Estudiante obj)
        {
            try
            {
                _bdAcademico.Estudiantes.Add(obj);
                _bdAcademico.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { msj = "Se ha guardado el estudiante"});
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { msj = ex.Message});
            }

        }

        [HttpDelete]
        [Route("Eliminar/{cedula:int}")]
        public IActionResult Eliminar(int cedula)
        {
            Estudiante drop = _bdAcademico.Estudiantes.Find(cedula);

            if (drop == null) {

                return BadRequest("No se encontro la cedula");
            }
            try {
                _bdAcademico.Estudiantes.Remove(drop);
                _bdAcademico.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { msj = "Se ha eliminado el estudiante" });

            }
            catch (Exception ex) {

                return StatusCode(StatusCodes.Status200OK, new { msj = ex.Message });

            }
        }
        [HttpPut]
        [Route("Actualizar/{cedula:int}")]

        public IActionResult Actualizar([FromBody] Estudiante obj) {

            Estudiante put = _bdAcademico.Estudiantes.Find(obj.Cedula);

            if (put == null)
            {
                return BadRequest("No se encontro la cedula");
            }
            try
            {
                put.Nombre = obj.Nombre is null ? put.Nombre : obj. Nombre;
                put.Apellido = obj.Apellido is null ? put.Apellido : obj.Apellido;
                _bdAcademico.Estudiantes.Update(put);
                _bdAcademico.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { msj = "Se ha actualizado el estudiante" });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { msj = ex.Message });

            }
        }

    }
}
