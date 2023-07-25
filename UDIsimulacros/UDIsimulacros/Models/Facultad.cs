using System;
using System.Collections.Generic;

namespace UDIsimulacros.models;

public partial class Facultad
{
    public int Idfacultad { get; set; }

    public string Nombre { get; set; }

    public virtual ICollection<Carrera> Carreras { get; set; } = new List<Carrera>();
}
