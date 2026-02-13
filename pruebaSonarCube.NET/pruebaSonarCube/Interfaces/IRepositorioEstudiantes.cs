using ProyectoAnalisis.Models;
using System.Collections.Generic;

namespace ProyectoAnalisis.Interfaces
{
    public interface IRepositorioEstudiantes
    {
        void Agregar(Estudiante estudiante);
        IReadOnlyList<Estudiante> ObtenerTodos();
        int Contar();
    }
}