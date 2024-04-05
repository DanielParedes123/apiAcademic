using AcademicoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcademicoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class controladorDepartamento : ControllerBase
    {
        public readonly BdAcademicoContext _bdAcademico;

        public controladorDepartamento(BdAcademicoContext _contex)
        {

            _bdAcademico = _contex;
        }

        [HttpGet]
    [Route("Listar")]
    public IActionResult Listar()
    {

        List<Departamento> lista = new List<Departamento>();
        try
        {
            lista = _bdAcademico.Departamentos.ToList();
            return Ok(lista);
        }
        catch (Exception ex)
        {

            return StatusCode(StatusCodes.Status200OK, new { msj = ex.Message, Response = lista });
        }
    }
    [HttpPost]
    [Route("Guardar")]
    public IActionResult Guardar([FromBody] Departamento obj)
    {
        try
        {
            _bdAcademico.Departamentos.Add(obj);
            _bdAcademico.SaveChanges();
            return StatusCode(StatusCodes.Status200OK, new { msj = "Se ha guardado el departamento" });
        }
        catch (Exception ex)
        {

            return StatusCode(StatusCodes.Status200OK, new { msj = ex.Message });
        }

    }

    [HttpDelete]
    [Route("Eliminar/{id:int}")]
    public IActionResult Eliminar(int id)
    {
        Departamento drop = _bdAcademico.Departamentos.Find(id);

        if (drop == null)
        {

            return BadRequest("No se encontro el id");
        }
        try
        {
            _bdAcademico.Departamentos.Remove(drop);
            _bdAcademico.SaveChanges();
            return StatusCode(StatusCodes.Status200OK, new { msj = "Se ha eliminado el departamento" });

        }
        catch (Exception ex)
        {

            return StatusCode(StatusCodes.Status200OK, new { msj = ex.Message });

        }
    }
    [HttpPut]
    [Route("Actualizar/{id:int}")]

    public IActionResult Actualizar([FromBody] Departamento obj)
    {

        Departamento put = _bdAcademico.Departamentos.Find(obj.Id);

        if (put == null)
        {
            return BadRequest("No se encontro el id");
        }
        try
        {
            put.Nombre = obj.Nombre is null ? put.Nombre : obj.Nombre;
            _bdAcademico.Departamentos.Update(put);
            _bdAcademico.SaveChanges();
            return StatusCode(StatusCodes.Status200OK, new { msj = "Se ha actualizado el departamento" });

        }
        catch (Exception ex)
        {

            return StatusCode(StatusCodes.Status200OK, new { msj = ex.Message });

        }
    }
}
}
