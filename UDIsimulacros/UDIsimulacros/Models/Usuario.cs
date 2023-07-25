using System;
using System.Collections.Generic;

namespace UDIsimulacros.models;

public partial class Usuario
{
    public int Idusuario { get; set; }

    public string NumeroDocumento { get; set; }

    public string NombreCompleto { get; set; }

    public string Correo { get; set; }

    public string Rol { get; set; }

    public string Contraseña { get; set; }

    public string NumeroCelular { get; set; }

    public int Semestre { get; set; }

    public string Estado { get; set; }

    public int IdCarrera { get; set; }

    public int? NivelIngles { get; set; }

    public int? NivelLectura { get; set; }

    public int? NivelCuantitativo { get; set; }

    public int? NivelCiudadanas { get; set; }

    public int? NivelEscrita { get; set; }

    public virtual Carrera IdCarreraNavigation { get; set; }

    public virtual ICollection<Informepueba> Informepuebas { get; set; } = new List<Informepueba>();
}
