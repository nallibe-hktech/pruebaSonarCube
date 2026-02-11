using System;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoAnalisis
{
    // Clase principal del programa - Esta clase tiene múltiples responsabilidades (viola SRP)
    public class GestorEstudiantes
    {
        private List<Estudiante> estudiantes = new List<Estudiante>();
        private List<Estudiante> Estudiantes = new List<Estudiante>(); // Variable duplicada con mayúscula diferente

        // Constructor vacío
        public GestorEstudiantes()
        {
            // Variable no utilizada
            string variableNoUsada;

            // Variable declarada pero no asignada
            int numero;

            // Variables con nombres similares (cambian mayúsculas)
            string nombre = "Juan";
            string Nombre = "Pedro";
            string NOMBRE = "Carlos";
        }

        // Método duplicado (mismo nombre y parámetros)
        public void AgregarEstudiante(string nombre, int edad)
        {
            estudiantes.Add(new Estudiante { Nombre = nombre, Edad = edad });
        }

       

        // Método que devuelve variable no modificada
        public int CalcularPromedioEdad()
        {
            int promedio = 0; // Inicializada pero no modificada realmente
            int total = 0;

            foreach (var est in estudiantes)
            {
                total += est.Edad;
            }

            // Aquí debería calcularse el promedio pero no se hace
            // promedio = total / estudiantes.Count; // Esta línea está comentada

            return promedio; // Siempre devuelve 0
        }

        // Método con error de división por cero potencial
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

            // Error: división entera cuando debería ser decimal
            return total / estudiantes.Count;
        }

        // Método demasiado largo que hace muchas cosas (viola SRP)
        public void ProcesarEstudiantes()
        {
            // 1. Agregar estudiantes
            estudiantes.Add(new Estudiante { Nombre = "Ana", Edad = 20 });

            // 2. Calcular promedio
            int total = 0;
            foreach (var est in estudiantes)
            {
                total += est.Edad;
            }

            // 3. Imprimir resultados
            Console.WriteLine($"Total estudiantes: {estudiantes.Count}");
            Console.WriteLine($"Suma de edades: {total}");

            // 4. Filtrar estudiantes
            var mayores = estudiantes.Where(e => e.Edad > 18).ToList();

            // 5. Guardar en archivo (simulado)
            Console.WriteLine("Guardando datos...");

            // Demasiadas responsabilidades en un solo método
        }

        // Variable duplicada
        private string conexionDB = "Server=localhost";
            }

    // Clase Estudiante
    public class Estudiante
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }

        // Método que no hace lo que dice
        public bool EsMayorDeEdad()
        {
            // Error lógico: debería ser Edad >= 18
            return Edad > 18; // Falla cuando tiene exactamente 18 años
        }
    }

    // Interfaz que viola el principio de segregación de interfaces
    public interface IGestorBaseDatos
    {
        void Conectar();
        void Desconectar();
        void GuardarEstudiante(Estudiante e);
        void EliminarEstudiante(int id);
        void GenerarReporte(); // No todos los que implementen necesitarán esto
        void EnviarEmail(); // Tampoco esto es responsabilidad de gestión de DB
    }

    // Clase que implementa interfaz demasiado grande
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

        // Métodos que esta clase no debería implementar
        public void GenerarReporte()
        {
            throw new NotImplementedException("No soy responsable de reportes");
        }

        public void EnviarEmail()
        {
            throw new NotImplementedException("No soy un gestor de emails");
        }
    }

    // Clase con dependencia concreta (viola DIP)
    public class ReporteEstudiantes
    {
        private GestorMySQL gestor; // Dependencia concreta en lugar de abstracción

        public ReporteEstudiantes()
        {
            gestor = new GestorMySQL(); // Creación directa, no inyección
        }

        public void Generar()
        {
            gestor.GenerarReporte(); // Esto lanzará excepción
        }
    }

  

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Proyecto de Análisis de Código ===\n");

            // Crear instancia del gestor
            GestorEstudiantes gestor = new GestorEstudiantes();

            // Intentar usar método con división por cero
            try
            {
                double promedio = gestor.CalcularPromedioCorrecto();
                Console.WriteLine($"Promedio: {promedio}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al calcular promedio: {ex.Message}");
            }

            // Variable con error de mayúsculas
            string mensaje = "Hola";
            Console.WriteLine(mensaje);

            // Error: variable no existe
            // Console.WriteLine(MENSAJE); // Descomentar para ver error

            Console.WriteLine("\nFin del programa");
        }
    }
}