using System;
using System.Collections.Generic;

namespace AcademicoAPI.Models;

public partial class Materium
{
    public int IdMateria { get; set; }

    public string? Nombre { get; set; }

    public int? Creditos { get; set; }

    public int? IdDepartamento { get; set; }

    public virtual Departamento? IdDepartamentoNavigation { get; set; }

    public virtual ICollection<Inscripcion> Inscripcions { get; set; } = new List<Inscripcion>();
}
