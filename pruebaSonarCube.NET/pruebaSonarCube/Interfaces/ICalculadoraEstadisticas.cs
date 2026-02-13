using ProyectoAnalisis.Models;
using System.Collections.Generic;

namespace ProyectoAnalisis.Interfaces
{
    public interface ICalculadoraEstadisticas
    {
        double CalcularPromedioEdad(IEnumerable<Estudiante> estudiantes);
        int CalcularSumaEdades(IEnumerable<Estudiante> estudiantes);
        IEnumerable<Estudiante> FiltrarMayoresDeEdad(IEnumerable<Estudiante> estudiantes);
    }
}