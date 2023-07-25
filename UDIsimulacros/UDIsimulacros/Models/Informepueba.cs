using System;
using System.Collections.Generic;

namespace UDIsimulacros.models;

public partial class Informepueba
{
    public int IdInforme { get; set; }

    public int Calificacion { get; set; }

    public DateTime FechaHora { get; set; }

    public int IdUsuario { get; set; }

    public string Categoria { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; }

    public virtual ICollection<Preguntum> IdPregunta { get; set; } = new List<Preguntum>();
}
