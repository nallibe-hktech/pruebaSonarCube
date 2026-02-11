using ProyectoAnalisis.Interfaces;
using ProyectoAnalisis.Models;
using System;

namespace ProyectoAnalisis.Services
{
    // SOLID: Interface Segregation - Implementa solo las interfaces que necesita
    // SOLID: Dependency Inversion - Implementación concreta que puede ser inyectada
    public class GestorMySql : IConexionBaseDatos, IRepositorioEstudiantesBD, IGeneradorReportes, IServicioNotificaciones
    {
        public void Conectar()
        {
            Console.WriteLine("Conectando a MySQL");
        }

        public void Desconectar()
        {
            Console.WriteLine("Desconectando de MySQL");
        }

        public void GuardarEstudiante(Estudiante estudiante)
        {
            ArgumentNullException.ThrowIfNull(estudiante);

            Console.WriteLine($"Guardando {estudiante.Nombre}");
        }

        public void EliminarEstudiante(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que 0", nameof(id));

            Console.WriteLine($"Eliminando estudiante {id}");
        }

        public void GenerarReporte()
        {
            Console.WriteLine("Generando reporte de estudiantes (simulado)");
        }

        public void EnviarEmail()
        {
            Console.WriteLine("Enviando email (simulado)");
        }
    }
}