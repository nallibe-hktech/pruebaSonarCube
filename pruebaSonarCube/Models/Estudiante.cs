namespace ProyectoAnalisis.Models
{
    public class Estudiante
    {
        public required string Nombre { get; set; }
        public int Edad { get; set; }

        public bool EsMayorDeEdad()
        {
            return Edad >= 18;
        }
    }
}