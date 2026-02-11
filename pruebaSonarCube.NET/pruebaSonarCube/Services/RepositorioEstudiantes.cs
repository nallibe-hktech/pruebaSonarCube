using ProyectoAnalisis.Interfaces;
using ProyectoAnalisis.Models;
using System;
using System.Collections.Generic;

namespace ProyectoAnalisis.Services
{
    // SOLID: Single Responsibility - Solo gestiona la colección de estudiantes
    public class RepositorioEstudiantes : IRepositorioEstudiantes
    {
        private readonly List<Estudiante> estudiantes = new List<Estudiante>();

        public void Agregar(Estudiante estudiante)
        {
            ArgumentNullException.ThrowIfNull(estudiante);
            
            estudiantes.Add(estudiante);
        }

        public IReadOnlyList<Estudiante> ObtenerTodos()
        {
            return estudiantes.AsReadOnly();
        }

        public int Contar()
        {
            return estudiantes.Count;
        }
    }
}