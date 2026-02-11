using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace HKTech.SharePointMigration
{
    // --------------------------------------------------------------------
    // 1. DUPLICIDAD DE CLASES (mismo nombre en DISTINTOS NAMESPACES)
    //    COMPILA, PERO SONAR DETECTA LA CONFUSIÓN
    // --------------------------------------------------------------------
    public class PlanMigracion
    {
        public void GenerarCSV(List<ArchivoInfo> archivos, string destino)
        {
            using var writer = new StreamWriter("plan_migracion.csv");
            writer.WriteLine("Ruta,Tamaño,Tipo");
            foreach (var a in archivos)
                writer.WriteLine($"{a.Ruta},{a.Tamano},{a.Tipo}");
        }
    }

    // Namespace adicional para la clase duplicada
    namespace V2
    {
        public class PlanMigracion
        {
            public void GenerarJSON(List<ArchivoInfo> archivos, string destino)
            {
                var json = JsonSerializer.Serialize(archivos, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("plan_migracion.json", json);
            }
        }
    }

    // --------------------------------------------------------------------
    // 2. VIOLACIÓN DE PRINCIPIOS SOLID (ISP, SRP, DIP)
    // --------------------------------------------------------------------
    public interface IServicioMigracion
    {
        List<ArchivoInfo> EscanearArchivos(string ruta);
        bool ValidarPermisos(string archivo);
        void EnviarCorreoNotificacion();   // ❌ NO DEBERÍA ESTAR (ISP)
        void GenerarGraficoTorta();        // ❌ NO DEBERÍA ESTAR (ISP)
    }

    public class AnalizadorLocal : IServicioMigracion
    {
        public List<ArchivoInfo> EscanearArchivos(string ruta)
        {
            var archivos = new List<ArchivoInfo>();
            foreach (var file in Directory.GetFiles(ruta, "*.*", SearchOption.AllDirectories))
            {
                var info = new FileInfo(file);
                archivos.Add(new ArchivoInfo
                {
                    Ruta = file,
                    Tamano = info.Length,
                    Tipo = info.Extension.TrimStart('.')
                });
            }
            return archivos;
        }

        public bool ValidarPermisos(string archivo)
        {
            try
            {
                using var fs = File.OpenRead(archivo);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Métodos que no deberían implementarse (violan ISP)
        public void EnviarCorreoNotificacion()
        {
            throw new NotImplementedException("Este analizador no envía correos"); // S112
        }

        public void GenerarGraficoTorta()
        {
            // Vacío - S1186
        }
    }

    // --------------------------------------------------------------------
    // 3. DUPLICIDAD DE VARIABLES + MAYÚSCULAS/MINÚSCULAS
    // --------------------------------------------------------------------
    public class ConfiguracionMigracion
    {
        public int tamano_maximo_mb = 250;
        public int Tamano_Maximo_MB = 500;   // DUPLICADA
        public int tamano_maximo_Mb = 1000;  // TERCERA DUPLICADA

        public string[] excluir_carpetas = { "temp", "cache" };
        public string[] excluir_Carpetas = { "node_modules", ".git" }; // DUPLICADA

        public string version_herramienta = "2.1.0"; // NUNCA USADA (S1481)
    }

    // --------------------------------------------------------------------
    // 4. MÉTODOS DUPLICADOS (mismo código en dos métodos) - S4144
    // --------------------------------------------------------------------
    public static class CalculadorTamano
    {
        public static long CalcularTotalV1(List<ArchivoInfo> archivos)
        {
            long total = 0;
            foreach (var a in archivos)
                total += a.Tamano;
            return total;
        }

        // MISMA IMPLEMENTACIÓN EXACTA (duplicación)
        public static long CalcularTotalV2(List<ArchivoInfo> archivos)
        {
            long total = 0;
            foreach (var a in archivos)
                total += a.Tamano;
            return total;
        }
    }

    // --------------------------------------------------------------------
    // 5. MÉTODO QUE DEVUELVE VARIABLE NO MODIFICADA (S1854)
    // --------------------------------------------------------------------
    public static class Optimizador
    {
        public static List<string> RecomendarOptimizacion(List<ArchivoInfo> archivos)
        {
            var recomendaciones = new List<string>(); // NUNCA MODIFICADA

            foreach (var a in archivos)
            {
                if (a.Tamano > 250 * 1024 * 1024)
                    Console.WriteLine($"Archivo grande detectado: {a.Ruta}");
            }

            return recomendaciones; // SIEMPRE VACÍA
        }
    }

    // --------------------------------------------------------------------
    // 6. COMPLEJIDAD INNECESARIA, NOMBRES POBRES, CONSTANTES MÁGICAS
    // --------------------------------------------------------------------
    public static class Metricas
    {
        public static double c(int[][][] l) // S117
        {
            double t = 0;
            // Complejidad ciclomática alta (S3776)
            for (int i = 0; i < l.Length; i++)
                for (int j = 0; j < l[i].Length; j++)
                    for (int k = 0; k < l[i][j].Length; k++)
                        t += l[i][j][k] * 0.73; // Constante mágica (S119)
            return t;
        }
    }

    // --------------------------------------------------------------------
    // 7. CÓDIGO COMENTADO Y CÓDIGO MUERTO
    // --------------------------------------------------------------------
    public static class FiltroArchivos
    {
        public static List<ArchivoInfo> FiltrarSoportados(List<ArchivoInfo> archivos)
        {
            var soportados = new List<ArchivoInfo>();

            // CÓDIGO COMENTADO - S125
            // var extensionesPermitidas = new[] { ".docx", ".xlsx", ".pptx", ".pdf" };

            foreach (var a in archivos)
            {
                if (new[] { "docx", "xlsx", "pptx", "pdf" }.Contains(a.Tipo))
                    soportados.Add(a);
            }

            return soportados;
            // CÓDIGO MUERTO - S1763
            Console.WriteLine($"Archivos filtrados: {soportados.Count}");
        }
    }

    // --------------------------------------------------------------------
    // MODELO DE DATOS
    // --------------------------------------------------------------------
    public class ArchivoInfo
    {
        public string Ruta { get; set; }
        public long Tamano { get; set; }
        public string Tipo { get; set; }
    }

    // --------------------------------------------------------------------
    // 8. CLASE PROGRAM CON MÉTODO MAIN EXPLÍCITO
    // --------------------------------------------------------------------
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new string('=', 70));
            Console.WriteLine("  ASISTENTE LOCAL DE MIGRACIÓN A SHAREPOINT - H&K TECH (C#)");
            Console.WriteLine(new string('=', 70));

            // Ruta a analizar (por defecto, directorio actual)
            string ruta = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
            Console.WriteLine($"\n📂 Analizando ruta: {Path.GetFullPath(ruta)}\n");

            // 1. Escanear archivos
            var analizador = new AnalizadorLocal();
            var archivos = analizador.EscanearArchivos(ruta);
            Console.WriteLine($"   Archivos encontrados: {archivos.Count}");

            // 2. Calcular tamaño total (métodos duplicados)
            long tamanoV1 = CalculadorTamano.CalcularTotalV1(archivos);
            long tamanoV2 = CalculadorTamano.CalcularTotalV2(archivos);
            Console.WriteLine($"   Tamaño total (MB): {tamanoV1 / (1024.0 * 1024.0):F2} (V1) / {tamanoV2 / (1024.0 * 1024.0):F2} (V2)");

            // 3. Probar configuración con variables duplicadas
            var config = new ConfiguracionMigracion();
            Console.WriteLine($"   Tamaño máximo config: {config.tamano_maximo_mb}, {config.Tamano_Maximo_MB}, {config.tamano_maximo_Mb}");

            // 4. Generar plan de migración (clase duplicada en namespace V2)
            var planCSV = new PlanMigracion();                    // namespace HKTech.SharePointMigration
            planCSV.GenerarCSV(archivos.Take(5).ToList(), "SharePoint");
            var planJSON = new V2.PlanMigracion();               // namespace HKTech.SharePointMigration.V2
            planJSON.GenerarJSON(archivos.Take(5).ToList(), "SharePoint");
            Console.WriteLine($"   Planes de migración generados: CSV + JSON");

            // 5. Recomendaciones (método con variable no modificada)
            var recomendaciones = Optimizador.RecomendarOptimizacion(archivos);
            Console.WriteLine($"   Recomendaciones generadas: {recomendaciones.Count} (DEBERÍA SER >0)");

            // 6. Probar función con nombre pobre y constantes mágicas
            int[][][] datosPrueba = new int[][][]
            {
                new int[][] { new int[] { 1, 2 }, new int[] { 3, 4 } },
                new int[][] { new int[] { 5, 6 }, new int[] { 7, 8 } }
            };
            double resultadoC = Metricas.c(datosPrueba);
            Console.WriteLine($"   Resultado métrica fragmentación: {resultadoC}");

            // 7. Probar filtro con código muerto
            var soportados = FiltroArchivos.FiltrarSoportados(archivos);
            Console.WriteLine($"   Archivos soportados: {soportados.Count}");

            Console.WriteLine("\n" + new string('=', 70));
            Console.WriteLine("  ANÁLISIS LOCAL COMPLETADO");
            Console.WriteLine("  Revise los archivos plan_migracion.csv y plan_migracion.json");
            Console.WriteLine(new string('=', 70));
        }
    }
}