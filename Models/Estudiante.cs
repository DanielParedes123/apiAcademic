using System;
using System.Collections.Generic;

namespace AcademicoAPI.Models;

public partial class Estudiante
{
    public int Cedula { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;
}
