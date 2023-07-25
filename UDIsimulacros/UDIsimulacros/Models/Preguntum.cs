using System;
using System.Collections.Generic;

namespace UDIsimulacros.models;

public partial class Preguntum
{
    public int IdPregunta { get; set; }

    public string Descripcion { get; set; }

    public int NivelDeDificultad { get; set; }

    public string Categoria { get; set; }

    public string RespuestaUno { get; set; }

    public string RespuestaDos { get; set; }

    public string RespuestaTres { get; set; }

    public string RespuestaCorrecta { get; set; }

    public virtual ICollection<Informepueba> IdinfoPruebs { get; set; } = new List<Informepueba>();
}
