using ProyectoAnalisis.Interfaces;
using ProyectoAnalisis.Models;
using System;
using System.Linq;

namespace ProyectoAnalisis.Services
{
    // SOLID: Single Responsibility - Procesa y muestra información
    // SOLID: Dependency Inversion - Depende de abstracciones (interfaces)
    public class ProcesadorEstudiantes : IProcesadorEstudiantes
    {
        private readonly IRepositorioEstudiantes repositorio;
        private readonly ICalculadoraEstadisticas calculadora;

        public ProcesadorEstudiantes(
            IRepositorioEstudiantes repositorio, 
            ICalculadoraEstadisticas calculadora)
        {
            ArgumentNullException.ThrowIfNull(repositorio);
            ArgumentNullException.ThrowIfNull(calculadora);
            
            this.repositorio = repositorio;
            this.calculadora = calculadora;
        }

        public void Procesar()
        {
            var estudiantes = repositorio.ObtenerTodos();

            // Agregar estudiante de ejemplo si no hay ninguno
            if (!estudiantes.Any())
            {
                repositorio.Agregar(new Estudiante { Nombre = "Ana", Edad = 20 });
                estudiantes = repositorio.ObtenerTodos();
            }

            // Calcular y mostrar resultados
            int total = calculadora.CalcularSumaEdades(estudiantes);
            var mayores = calculadora.FiltrarMayoresDeEdad(estudiantes);

            Console.WriteLine($"Total estudiantes: {estudiantes.Count}");
            Console.WriteLine($"Suma de edades: {total}");
            Console.WriteLine($"Estudiantes mayores de 18: {mayores.Count()}");
            Console.WriteLine("Guardando datos...");
        }
    }
}