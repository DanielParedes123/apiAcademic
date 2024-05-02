using AcademicoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcademicoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class controladorInscripcion : ControllerBase
    {
        public readonly BdAcademicoContext _bdAcademico;

        public controladorInscripcion(BdAcademicoContext _contex)
        {

            _bdAcademico = _contex;
        }

        [HttpGet]
        [Route("listar")]
        public IActionResult Listar()
        {
            try
            {
                var lista = _bdAcademico.Inscripcions.ToList();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("guardar")]
        public IActionResult Guardar([FromBody] Inscripcion objeto)
        {
            try
            {
                _bdAcademico.Inscripcions.Add(objeto);
                _bdAcademico.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Se ha guardado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Actualizar/{id:int}")]
        public IActionResult Editar([FromBody] Inscripcion objeto)
        {
            Inscripcion oinscripcion = _bdAcademico.Inscripcions.Find(objeto.IdInscripcion);

            if (oinscripcion == null)
            {
                return BadRequest("Id no encontrado");

            }
            try
            {
                if (objeto.Año != 0)
                {
                    oinscripcion.Año = objeto.Año;
                }

                if (objeto.Nota != 0)
                {
                    oinscripcion.Nota = objeto.Nota;
                }

                if (objeto.FkIdMateria.HasValue)
                {
                    oinscripcion.FkIdMateria = objeto.FkIdMateria;
                }

                if (objeto.FkIdEstudiante.HasValue)
                {
                    oinscripcion.FkIdEstudiante = objeto.FkIdEstudiante;
                }




                _bdAcademico.Inscripcions.Update(oinscripcion);
                _bdAcademico.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Se ha editado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }


        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public IActionResult Eliminar(int id)
        {
            try
            {
                var inscripcion = _bdAcademico.Inscripcions.Find(id);
                if (inscripcion == null)
                    return NotFound("Inscripción no encontrada");

                _bdAcademico.Inscripcions.Remove(inscripcion);
                _bdAcademico.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }
    }
}
