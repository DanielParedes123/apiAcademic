using System;
using System.Collections.Generic;

namespace AcademicoAPI.Models;

public partial class Inscripcion
{
    public int IdInscripcion { get; set; }

    public int? Año { get; set; }

    public int? Semestre { get; set; }

    public decimal? Nota { get; set; }

    public int? FkIdMateria { get; set; }

    public int? FkIdEstudiante { get; set; }

    public virtual Estudiante? FkIdEstudianteNavigation { get; set; }

    public virtual Materium? FkIdMateriaNavigation { get; set; }
}
