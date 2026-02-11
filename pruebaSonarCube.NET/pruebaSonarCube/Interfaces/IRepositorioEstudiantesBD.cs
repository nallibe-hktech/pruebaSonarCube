using ProyectoAnalisis.Models;

namespace ProyectoAnalisis.Interfaces
{
    public interface IRepositorioEstudiantesBD
    {
        void GuardarEstudiante(Estudiante estudiante);
        void EliminarEstudiante(int id);
    }
}