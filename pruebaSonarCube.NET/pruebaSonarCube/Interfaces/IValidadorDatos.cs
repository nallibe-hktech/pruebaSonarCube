using ProyectoAnalisis.Models;

namespace ProyectoAnalisis.Interfaces
{
    public interface IValidadorDatos
    {
        ValidationResult ValidarNumero(int valor);
    }
}