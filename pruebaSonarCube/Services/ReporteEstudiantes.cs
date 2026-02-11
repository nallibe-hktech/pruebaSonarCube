using ProyectoAnalisis.Interfaces;
using System;

namespace ProyectoAnalisis.Services
{
    // SOLID: Dependency Inversion - Depende de abstracción (IGeneradorReportes)
    // SOLID: Single Responsibility - Solo genera reportes
    public class ReporteEstudiantes
    {
        private readonly IGeneradorReportes generador;

        public ReporteEstudiantes(IGeneradorReportes generador)
        {
            ArgumentNullException.ThrowIfNull(generador);
            this.generador = generador;
        }

        public void Generar()
        {
            generador.GenerarReporte();
        }
    }
}