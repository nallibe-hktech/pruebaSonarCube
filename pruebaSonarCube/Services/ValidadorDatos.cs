using ProyectoAnalisis.Interfaces;
using ProyectoAnalisis.Models;

namespace ProyectoAnalisis.Services
{
    // SOLID: Single Responsibility - Solo valida datos
    public class ValidadorDatos : IValidadorDatos
    {
        public ValidationResult ValidarNumero(int valor)
        {
            if (valor <= 0)
                return new ValidationResult { EsValido = false, Mensaje = "Valor debe ser mayor que 0" };

            if (valor >= 10)
                return new ValidationResult { EsValido = false, Mensaje = "Valor debe ser menor que 10" };

            if (valor % 2 != 0)
                return new ValidationResult { EsValido = false, Mensaje = "Valor debe ser par" };

            if (valor == 4)
                return new ValidationResult { EsValido = false, Mensaje = "Valor no puede ser 4" };

            return new ValidationResult { EsValido = true, Mensaje = "Valor válido" };
        }
    }
}