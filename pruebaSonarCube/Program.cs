using System;
using ProyectoAnalisis.Interfaces;
using ProyectoAnalisis.Models;
using ProyectoAnalisis.Services;

namespace ProyectoAnalisis
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Proyecto de Análisis de Código ===\n");

            // SOLID: Dependency Injection - Configurar servicios
            // Nota: Usamos var para inferir el tipo concreto, pero el diseño permite
            // inyección de dependencias a través de constructores que reciben interfaces
            var repositorio = new RepositorioEstudiantes();
            var calculadora = new CalculadoraEstadisticas();
            var procesador = new ProcesadorEstudiantes(repositorio, calculadora);
            var validador = new ValidadorDatos();

            // Agregar estudiantes
            repositorio.Agregar(new Estudiante { Nombre = "Juan", Edad = 18 });
            repositorio.Agregar(new Estudiante { Nombre = "María", Edad = 22 });
            repositorio.Agregar(new Estudiante { Nombre = "Luis", Edad = 17 });

            // Calcular estadísticas
            var estudiantes = repositorio.ObtenerTodos();
            double promedio = calculadora.CalcularPromedioEdad(estudiantes);
            Console.WriteLine($"Promedio de edad: {promedio:F2}");

            // Procesar estudiantes
            procesador.Procesar();

            // Validar datos
            var resultado = validador.ValidarNumero(5);
            Console.WriteLine($"Validación: {resultado.Mensaje}");

            // Usar reporte con inyección de dependencias
            IGeneradorReportes gestorMySql = new GestorMySql();
            var reporte = new ReporteEstudiantes(gestorMySql);
            reporte.Generar();

            Console.WriteLine("\nFin del programa");
        }
    }
}
