"""
Proyecto de An�lisis de C�digo Python - Versi�n Corregida
Este proyecto aplica principios SOLID y buenas pr�cticas de programaci�n
"""

import sys
from abc import ABC, abstractmethod
from typing import List, Optional, Dict, Any


# Clase Estudiante con l�gica corregida
class Estudiante:
    def __init__(self, nombre: str, edad: int):
        self.nombre = nombre
        self.edad = edad
    
    def es_mayor_de_edad(self) -> bool:
        """Retorna True si el estudiante tiene 18 a�os o m�s"""
        return self.edad >= 18  # CORREGIDO: ahora incluye exactamente 18 a�os


# Clase con responsabilidad �nica: gestionar estudiantes
class GestorEstudiantes:
    """Gestiona una colecci�n de estudiantes"""
    
    def __init__(self):
        self.estudiantes: List[Estudiante] = []
    
    def agregar_estudiante(self, nombre: str, edad: int) -> None:
        """Agrega un estudiante a la colecci�n"""
        self.estudiantes.append(Estudiante(nombre, edad))
    
    def obtener_estudiantes(self) -> List[Estudiante]:
        """Retorna la lista de estudiantes"""
        return self.estudiantes.copy()
    
    def obtener_estudiantes_mayores_de_edad(self) -> List[Estudiante]:
        """Retorna estudiantes mayores de edad"""
        return [e for e in self.estudiantes if e.es_mayor_de_edad()]


# Clase con responsabilidad �nica: calcular estad�sticas
class CalculadoraEstadisticas:
    """Calcula estad�sticas sobre estudiantes"""
    
    @staticmethod
    def calcular_promedio_edad(estudiantes: List[Estudiante]) -> float:
        """Calcula el promedio de edad de los estudiantes"""
        if not estudiantes:
            return 0.0
        
        total = sum(e.edad for e in estudiantes)
        # CORREGIDO: usa divisi�n normal (/) en lugar de divisi�n entera (//)
        return total / len(estudiantes)
    
    @staticmethod
    def calcular_suma_edades(estudiantes: List[Estudiante]) -> int:
        """Calcula la suma total de edades"""
        return sum(e.edad for e in estudiantes)


# Clase con responsabilidad �nica: imprimir reportes
class ImpresorReportes:
    """Imprime reportes de estudiantes"""
    
    @staticmethod
    def imprimir_resumen(estudiantes: List[Estudiante], promedio: float) -> None:
        """Imprime un resumen de los estudiantes"""
        print(f"Total estudiantes: {len(estudiantes)}")
        print(f"Suma de edades: {sum(e.edad for e in estudiantes)}")
        print(f"Promedio de edad: {promedio:.2f}")
    
    @staticmethod
    def imprimir_lista_estudiantes(estudiantes: List[Estudiante]) -> None:
        """Imprime la lista de estudiantes"""
        for estudiante in estudiantes:
            print(f"- {estudiante.nombre}: {estudiante.edad} a�os")


# Clase con responsabilidad �nica: guardar datos
class GuardadorDatos:
    """Guarda datos de estudiantes"""
    
    @staticmethod
    def guardar_estudiantes(estudiantes: List[Estudiante]) -> None:
        """Simula guardar datos en almacenamiento"""
        print("Guardando datos...")
        print(f"Se guardaron {len(estudiantes)} estudiantes")


# Interfaces segregadas (aplica ISP - Interface Segregation Principle)
class IConexionBaseDatos(ABC):
    """Interfaz para operaciones de conexi�n a base de datos"""
    
    @abstractmethod
    def conectar(self) -> None:
        pass
    
    @abstractmethod
    def desconectar(self) -> None:
        pass


class IRepositorioEstudiantes(ABC):
    """Interfaz para operaciones CRUD de estudiantes"""
    
    @abstractmethod
    def guardar_estudiante(self, estudiante: Estudiante) -> None:
        pass
    
    @abstractmethod
    def eliminar_estudiante(self, id_estudiante: int) -> None:
        pass


class IGeneradorReportes(ABC):
    """Interfaz separada para generaci�n de reportes"""
    
    @abstractmethod
    def generar_reporte(self) -> None:
        pass


class IServicioEmail(ABC):
    """Interfaz separada para env�o de emails"""
    
    @abstractmethod
    def enviar_email(self, destinatario: str, mensaje: str) -> None:
        pass


# Implementaci�n concreta con interfaces segregadas
class GestorMySQL(IConexionBaseDatos, IRepositorioEstudiantes):
    """Gestor de base de datos MySQL con responsabilidades espec�ficas"""
    
    def __init__(self, servidor: str = "localhost"):
        self.servidor = servidor
        self.conectado = False
    
    def conectar(self) -> None:
        """Establece conexi�n con MySQL"""
        print(f"Conectando a MySQL en {self.servidor}")
        self.conectado = True
    
    def desconectar(self) -> None:
        """Cierra conexi�n con MySQL"""
        print("Desconectando de MySQL")
        self.conectado = False
    
    def guardar_estudiante(self, estudiante: Estudiante) -> None:
        """Guarda un estudiante en la base de datos"""
        if self.conectado:
            print(f"Guardando {estudiante.nombre} en MySQL")
        else:
            print("Error: No hay conexi�n a la base de datos")
    
    def eliminar_estudiante(self, id_estudiante: int) -> None:
        """Elimina un estudiante de la base de datos"""
        if self.conectado:
            print(f"Eliminando estudiante {id_estudiante} de MySQL")
        else:
            print("Error: No hay conexi�n a la base de datos")


# Generador de reportes independiente
class GeneradorReportesEstudiantes(IGeneradorReportes):
    """Genera reportes de estudiantes"""
    
    def __init__(self, estudiantes: List[Estudiante]):
        self.estudiantes = estudiantes
    
    def generar_reporte(self) -> None:
        """Genera un reporte completo de estudiantes"""
        print("\n=== REPORTE DE ESTUDIANTES ===")
        calculadora = CalculadoraEstadisticas()
        promedio = calculadora.calcular_promedio_edad(self.estudiantes)
        
        impresor = ImpresorReportes()
        impresor.imprimir_resumen(self.estudiantes, promedio)
        impresor.imprimir_lista_estudiantes(self.estudiantes)


# Clase con inyecci�n de dependencias (aplica DIP - Dependency Inversion Principle)
class ServicioReportes:
    """Servicio que usa inyecci�n de dependencias"""
    
    def __init__(self, generador: IGeneradorReportes):
        # CORREGIDO: usa abstracci�n en lugar de implementaci�n concreta
        self.generador = generador
    
    def generar(self) -> None:
        """Genera el reporte usando el generador inyectado"""
        self.generador.generar_reporte()


# Clase para procesar datos de forma estructurada
class ProcesadorEstudiantes:
    """Procesa estudiantes aplicando SRP"""
    
    def __init__(self, gestor: GestorEstudiantes):
        self.gestor = gestor
        self.calculadora = CalculadoraEstadisticas()
        self.impresor = ImpresorReportes()
        self.guardador = GuardadorDatos()
    
    def procesar(self) -> None:
        """Procesa estudiantes coordinando las diferentes responsabilidades"""
        estudiantes = self.gestor.obtener_estudiantes()
        
        # Calcular estad�sticas
        promedio = self.calculadora.calcular_promedio_edad(estudiantes)
        
        # Imprimir resultados
        self.impresor.imprimir_resumen(estudiantes, promedio)
        
        # Filtrar y mostrar mayores de edad
        mayores = self.gestor.obtener_estudiantes_mayores_de_edad()
        print(f"\nEstudiantes mayores de edad: {len(mayores)}")
        
        # Guardar datos
        self.guardador.guardar_estudiantes(estudiantes)


# Funci�n de utilidad con par�metros utilizados correctamente
def formatear_datos_estudiante(nombre: str, edad: int) -> str:
    """Formatea los datos de un estudiante"""
    # CORREGIDO: eliminado par�metro no usado
    return f"{nombre} tiene {edad} a�os"


def formatear_datos_completos(nombre: str, edad: int, ciudad: str) -> str:
    """Formatea los datos completos de un estudiante incluyendo ciudad"""
    # CORREGIDO: ahora usa todos los par�metros
    return f"{nombre} tiene {edad} a�os y vive en {ciudad}"


# Funci�n simplificada sin c�digo muerto
def calcular_valor() -> int:
    """Calcula un valor de ejemplo"""
    # CORREGIDO: c�digo limpio sin bloques inalcanzables
    resultado = 42
    return resultado


# Funci�n con complejidad reducida
def calcular_suma_pares(limite: int) -> int:
    """
    Calcula la suma de productos de n�meros pares
    CORREGIDO: complejidad reducida mediante extracci�n de l�gica
    """
    resultado = 0
    
    for num_i in range(limite):
        if num_i % 2 == 0:
            resultado += _procesar_fila(num_i, limite)
    
    return resultado


def _procesar_fila(num_i: int, limite: int) -> int:
    """Procesa una fila del c�lculo"""
    suma_fila = 0
    
    for num_j in range(limite):
        if num_j % 2 == 0:
            suma_fila += _procesar_celda(num_i, num_j, limite)
    
    return suma_fila


def _procesar_celda(num_i: int, num_j: int, limite: int) -> int:
    """Procesa una celda del c�lculo"""
    suma_celda = 0
    
    for num_k in range(limite):
        if num_k % 2 == 0:
            suma_celda += num_i * num_j * num_k
    
    return suma_celda


# Funci�n principal
def main():
    print("=== Proyecto de An�lisis de C�digo Python - Versi�n Corregida ===\n")
    
    # Crear gestor de estudiantes
    gestor = GestorEstudiantes()
    
    # Agregar estudiantes
    gestor.agregar_estudiante("Juan", 20)
    gestor.agregar_estudiante("Mar�a", 22)
    gestor.agregar_estudiante("Carlos", 18)
    gestor.agregar_estudiante("Ana", 18)
    
    # Calcular promedio correctamente
    calculadora = CalculadoraEstadisticas()
    promedio = calculadora.calcular_promedio_edad(gestor.obtener_estudiantes())
    print(f"Promedio de edad (correcto): {promedio:.2f}")
    
    # Probar verificaci�n de mayor�a de edad corregida
    estudiante_ana = Estudiante("Ana", 18)
    print(f"�Ana (18 a�os) es mayor de edad? {estudiante_ana.es_mayor_de_edad()}")  # Ahora: True
    
    # Probar generador de reportes con DIP
    print("\n--- Generando reporte con inyecci�n de dependencias ---")
    generador = GeneradorReportesEstudiantes(gestor.obtener_estudiantes())
    servicio_reportes = ServicioReportes(generador)
    servicio_reportes.generar()
    
    # Probar procesador de estudiantes
    print("\n--- Procesando estudiantes ---")
    procesador = ProcesadorEstudiantes(gestor)
    procesador.procesar()
    
    # Probar funciones de formato corregidas
    print(f"\nDatos simples: {formatear_datos_estudiante('Pedro', 25)}")
    print(f"Datos completos: {formatear_datos_completos('Pedro', 25, 'Madrid')}")
    
    # Probar funci�n sin c�digo muerto
    print(f"\nResultado funci�n limpia: {calcular_valor()}")
    
    # Variables con nombres descriptivos (CORREGIDO)
    longitud = 10
    offset = 20
    print(f"\nVariables descriptivas: longitud={longitud}, offset={offset}")
    
    # Probar gestor de base de datos con interfaces segregadas
    print("\n--- Probando gestor MySQL ---")
    gestor_mysql = GestorMySQL("localhost")
    gestor_mysql.conectar()
    gestor_mysql.guardar_estudiante(estudiante_ana)
    gestor_mysql.desconectar()
    
    print("\n=== Fin del programa - C�digo corregido y refactorizado ===")


if __name__ == "__main__":
   
    main()
