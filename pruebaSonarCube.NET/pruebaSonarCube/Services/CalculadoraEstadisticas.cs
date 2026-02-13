using ProyectoAnalisis.Interfaces;
using ProyectoAnalisis.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoAnalisis.Services
{
    // SOLID: Single Responsibility - Solo calcula estadísticas
    public class CalculadoraEstadisticas : ICalculadoraEstadisticas
    {
        public double CalcularPromedioEdad(IEnumerable<Estudiante> estudiantes)
        {
            ArgumentNullException.ThrowIfNull(estudiantes);
            
            if (!estudiantes.Any())
                return 0;

            return estudiantes.Average(e => e.Edad);
        }

        public int CalcularSumaEdades(IEnumerable<Estudiante> estudiantes)
        {
            ArgumentNullException.ThrowIfNull(estudiantes);

            return estudiantes.Sum(e => e.Edad);
        }

        public IEnumerable<Estudiante> FiltrarMayoresDeEdad(IEnumerable<Estudiante> estudiantes)
        {
            ArgumentNullException.ThrowIfNull(estudiantes);

            return estudiantes.Where(e => e.EsMayorDeEdad()).ToList();
        }
    }
}