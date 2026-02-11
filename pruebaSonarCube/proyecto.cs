using System;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoAnalisis
{
    // Clase principal del programa - responsabilidad centrada en gestión de estudiantes
    public class GestorEstudiantes
    {
        private List<Estudiante> estudiantes = new List<Estudiante>();
        private string conexionDB = "Server=127.0.0.1";

        private int contador; // Variable declarada pero nunca usada (error)

        // Constructor vacío (sin variables sin usar)
        public GestorEstudiantes()
        {
        }

        // Agregar estudiante (única implementación)
        public void AgregarEstudiante(string nombre, int edad)
        {
            estudiantes.Add(new Estudiante { Nombre = nombre, Edad = edad });
        }

        // Calcula el promedio de edades como entero (protege división por cero)
        public int CalcularPromedioEdad()
        {
            if (estudiantes.Count == 0)
                return 0;

            int total = 0;
            foreach (var est in estudiantes)
            {
                total += est.Edad;
            }

            return total / estudiantes.Count;
        }

        // Código duplicado (error) - método casi igual a CalcularPromedioEdad
        public int CalcularEdadPromedioDuplicado()
        {
            if (estudiantes.Count == 0)
                return 0;

            int total = 0;
            foreach (var est in estudiantes)
            {
                total += est.Edad;
            }

            return total / estudiantes.Count;
        }

        // Calcula el promedio correctamente como decimal
        public double CalcularPromedioCorrecto()
        {
            if (estudiantes.Count == 0)
            {
                return 0; // Esto previene división por cero
            }

            int total = 0;
            foreach (var est in estudiantes)
            {
                total += est.Edad;
            }

            return (double)total / estudiantes.Count;
        }

        // Método que procesa estudiantes (ejemplo simple)
        public void ProcesarEstudiantes()
        {
            // 1. Agregar estudiante de ejemplo si no hay ninguno
            if (!estudiantes.Any())
            {
                estudiantes.Add(new Estudiante { Nombre = "Ana", Edad = 20 });
            }

            // 2. Calcular suma y mostrar resultados
            int total = estudiantes.Sum(e => e.Edad);

            Console.WriteLine($"Total estudiantes: {estudiantes.Count}");
            Console.WriteLine($"Suma de edades: {total}");

            // 3. Filtrar estudiantes mayores de edad
            var mayores = estudiantes.Where(e => e.Edad > 18).ToList();
            Console.WriteLine($"Estudiantes mayores de 18: {mayores.Count}");

            // 4. Simular guardado
            Console.WriteLine("Guardando datos...");

            MetodoComplejo(5); // Llamada al método complejo para aumentar la complejidad ciclomatica
        }

        // Método con complejidad ciclomatica alta (varios if anidados)
        public void MetodoComplejo(int x)
        {
            if (x > 0)
            {
                if (x < 10)
                {
                    if (x % 2 == 0)
                    {
                        if (x != 4)
                        {
                            Console.WriteLine("Complejidad alta");
                        }
                    }
                }
            }
        }

        // Método vacío (error)
        public void MetodoVacio()
        {
        }

        // Método con manejo incorrecto de excepciones
        public void MetodoExcepcion()
        {
            try
            {
                int y = 0;
                int z = 1 / y; // Genera excepción DivideByZeroException
            }
            catch (Exception)
            {
                // Captura y no hace nada (mala práctica)
            }
        }
    }

    // Clase Estudiante
    public class Estudiante
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }

        // Corrige la lógica: mayor o igual a 18
        public bool EsMayorDeEdad()
        {
            return Edad >= 18;
        }
    }

    // Interfaz para gestor de base de datos
    public interface IGestorBaseDatos
    {
        void Conectar();
        void Desconectar();
        void GuardarEstudiante(Estudiante e);
        void EliminarEstudiante(int id);
        void GenerarReporte();
        void EnviarEmail();
    }

    // Implementación concreta (simplificada) de gestor MySQL
    public class GestorMySQL : IGestorBaseDatos
    {
        public void Conectar()
        {
            Console.WriteLine("Conectando a MySQL");
        }

        public void Desconectar()
        {
            Console.WriteLine("Desconectando de MySQL");
        }

        public void GuardarEstudiante(Estudiante e)
        {
            Console.WriteLine($"Guardando {e.Nombre}");
        }

        public void EliminarEstudiante(int id)
        {
            Console.WriteLine($"Eliminando estudiante {id}");
        }

        // Implementaciones simples en lugar de lanzar excepciones
        public void GenerarReporte()
        {
            Console.WriteLine("Generando reporte de estudiantes (simulado)");
        }

        public void EnviarEmail()
        {
            Console.WriteLine("Enviando email (simulado)");
        }
    }

    // Clase ReporteEstudiantes usa la abstracción IGestorBaseDatos (inyección)
    public class ReporteEstudiantes
    {
        private readonly IGestorBaseDatos gestor;

        public ReporteEstudiantes(IGestorBaseDatos gestor = null)
        {
            this.gestor = gestor ?? new GestorMySQL();
        }

        public void Generar()
        {
            gestor.GenerarReporte();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Proyecto de Análisis de Código ===\n");

            // Crear instancia del gestor y añadir algunos estudiantes
            GestorEstudiantes gestor = new GestorEstudiantes();
            gestor.AgregarEstudiante("Juan", 18);
            gestor.AgregarEstudiante("María", 22);
            gestor.AgregarEstudiante("Luis", 17);

            // Usar métodos
            double promedio = gestor.CalcularPromedioCorrecto();
            Console.WriteLine($"Promedio (decimal): {promedio:F2}");

            int promedioEntero = gestor.CalcularPromedioEdad();
            Console.WriteLine($"Promedio (entero): {promedioEntero}");

            // Procesar estudiantes (muestra)
            gestor.ProcesarEstudiantes();

            // Ejecutar método vacío y manejo incorrecto de excepciones para generar errores
            gestor.MetodoVacio();
            gestor.MetodoExcepcion();

            // Usar ReporteEstudiantes con inyección de dependencia
            var reporte = new ReporteEstudiantes(new GestorMySQL());
            reporte.Generar();

            // Mensaje simple
            string mensaje = "Hola";
            Console.WriteLine(mensaje);

            Console.WriteLine("\nFin del programa");
        }
    }
}