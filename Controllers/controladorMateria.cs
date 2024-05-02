using AcademicoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcademicoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class controladorMateria : ControllerBase
    {
        public readonly BdAcademicoContext _bdAcademico;

        public controladorMateria(BdAcademicoContext _contex)
        {
            _bdAcademico = _contex;
        }

        [HttpGet]
        [Route("Listar")]
        public IActionResult Listar()
        {
            try
            {
                var lista = _bdAcademico.Materia.ToList();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Materium Materia)
        {
            try
            {
                var programaAcademico = _bdAcademico.Departamentos.Find(Materia.IdDepartamento);
                if (programaAcademico == null)
                {
                    return BadRequest("El departamento especificado no existe");
                }

                Materia.IdDepartamentoNavigation = programaAcademico;

                _bdAcademico.Materia.Add(Materia);
                _bdAcademico.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Se ha Guardado Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al guardar la materia.", detalle = ex.InnerException.Message });
            }
        }

        [HttpPut]
        [Route("Actualizar/{id:int}")]
        public IActionResult Editar([FromBody] Materium materia)
        {
            try
            {
              
                var materiaToUpdate = _bdAcademico.Materia.Find(materia.IdMateria);

                if (materiaToUpdate == null)
                {
                    return NotFound("Materia no encontrada");
                }

                
                materiaToUpdate.Nombre = materia.Nombre ?? materiaToUpdate.Nombre;
                if (materia.Creditos != null)
                {
                    materiaToUpdate.Creditos = materia.Creditos;
                }

                
                if (materia.IdDepartamento != materiaToUpdate.IdDepartamento)
                {
                    var nuevoPrograma = _bdAcademico.Departamentos.Find(materia.IdDepartamento);
                    if (nuevoPrograma == null)
                    {
                        return NotFound("El programa académico especificado no existe");
                    }

                    materiaToUpdate.IdDepartamento = materia.IdDepartamento;
                    materiaToUpdate.IdDepartamentoNavigation = nuevoPrograma;
                }

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
                var materia = _bdAcademico.Materia.Find(id);
                if (materia == null)
                {
                    return NotFound("Materia no encontrada");
                }

                _bdAcademico.Materia.Remove(materia);
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
