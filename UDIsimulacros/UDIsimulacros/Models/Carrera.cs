using System;
using System.Collections.Generic;

namespace UDIsimulacros.models;

public partial class Carrera
{
    public int Idcarrera { get; set; }

    public string Nombre { get; set; }

    public int IdFacultad { get; set; }

    public virtual Facultad IdFacultadNavigation { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
