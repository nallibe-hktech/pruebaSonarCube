"""
Proyecto de Análisis de Código Python
Este proyecto contiene varios errores y violaciones de principios para análisis estático
"""

import sys
from abc import ABC, abstractmethod
from typing import List, Optional, Dict, Any


# Clase principal del programa - Esta clase tiene múltiples responsabilidades (viola SRP)
class GestorEstudiantes:
    def __init__(self):
        self.estudiantes = []
        self.Estudiantes = []  # Variable duplicada con mayúscula diferente
        
        # Variable no utilizada
        variable_no_usada = None
        
        # Variable declarada pero no asignada correctamente
        numero = None  # En Python no podemos dejar sin asignar como en C#
        
        # Variables con nombres similares (cambian mayúsculas)
        nombre = "Juan"
        Nombre = "Pedro"
        NOMBRE = "Carlos"
    
    # Método duplicado (mismo nombre y parámetros)
    def agregar_estudiante(self, nombre: str, edad: int) -> None:
        self.estudiantes.append(Estudiante(nombre, edad))
    
    # Método duplicado (en Python esto sobrescribe el anterior, no es error de compilación)
    def agregar_estudiante(self, nombre: str, edad: int) -> None:
        self.estudiantes.append(Estudiante(nombre, edad))
    
    # Método que devuelve variable no modificada
    def calcular_promedio_edad(self) -> float:
        promedio = 0.0  # Inicializada pero no modificada realmente
        total = 0
        
        for est in self.estudiantes:
            total += est.edad
        
        # Aquí debería calcularse el promedio pero no se hace
        # promedio = total / len(self.estudiantes) if self.estudiantes else 0
        
        return promedio  # Siempre devuelve 0.0
    
    # Método con error de división por cero potencial
    def calcular_promedio_correcto(self) -> float:
        if len(self.estudiantes) == 0:
            return 0.0
        
        total = 0
        for est in self.estudiantes:
            total += est.edad
        
        # Error: división entera cuando debería ser decimal, pero en Python 3 la división de enteros produce float
        # Sin embargo, si queremos que sea un error, podríamos usar // para división entera
        # Pero en realidad, en Python 3, / siempre devuelve float, así que no es un error.
        # Vamos a forzar un error de división entera usando // para que el resultado sea entero
        return total // len(self.estudiantes)  # Esto devuelve un entero, no es el promedio correcto
    
    # Método demasiado largo que hace muchas cosas (viola SRP)
    def procesar_estudiantes(self) -> None:
        # 1. Agregar estudiantes
        self.estudiantes.append(Estudiante("Ana", 20))
        
        # 2. Calcular promedio
        total = 0
        for est in self.estudiantes:
            total += est.edad
        
        # 3. Imprimir resultados
        print(f"Total estudiantes: {len(self.estudiantes)}")
        print(f"Suma de edades: {total}")
        
        # 4. Filtrar estudiantes
        mayores = [e for e in self.estudiantes if e.edad > 18]
        
        # 5. Guardar en archivo (simulado)
        print("Guardando datos...")
        
        # Demasiadas responsabilidades en un solo método
    
    # Variable duplicada (esto no es posible en Python como en C#, aquí solo se reasigna)
    # Pero podemos simularlo con dos variables en la misma clase? No, porque no hay declaración.
    # En su lugar, podemos tener dos variables de instancia con el mismo nombre, pero la última sobrescribe.
    # Vamos a hacer algo que no tenga sentido:
    def duplicar_variables(self):
        self.conexion_db = "Server=localhost"
        self.conexion_db = "Server=127.0.0.1"  # Esto sobrescribe la anterior, no es error de compilación

# Clase Estudiante
class Estudiante:
    def __init__(self, nombre: str, edad: int):
        self.nombre = nombre
        self.edad = edad
    
    # Método que no hace lo que dice
    def es_mayor_de_edad(self) -> bool:
        # Error lógico: debería ser edad >= 18
        return self.edad > 18  # Falla cuando tiene exactamente 18 años

# Interfaz que viola el principio de segregación de interfaces
# En Python no hay interfaces nativas, pero podemos usar ABC (Abstract Base Classes)
class IGestorBaseDatos(ABC):
    @abstractmethod
    def conectar(self):
        pass
    
    @abstractmethod
    def desconectar(self):
        pass
    
    @abstractmethod
    def guardar_estudiante(self, estudiante: Estudiante):
        pass
    
    @abstractmethod
    def eliminar_estudiante(self, id: int):
        pass
    
    @abstractmethod
    def generar_reporte(self):  # No todos los que implementen necesitarán esto
        pass
    
    @abstractmethod
    def enviar_email(self):  # Tampoco esto es responsabilidad de gestión de DB
        pass

# Clase que implementa interfaz demasiado grande
class GestorMySQL(IGestorBaseDatos):
    def conectar(self):
        print("Conectando a MySQL")
    
    def desconectar(self):
        print("Desconectando de MySQL")
    
    def guardar_estudiante(self, estudiante: Estudiante):
        print(f"Guardando {estudiante.nombre}")
    
    def eliminar_estudiante(self, id: int):
        print(f"Eliminando estudiante {id}")
    
    # Métodos que esta clase no debería implementar
    def generar_reporte(self):
        raise NotImplementedError("No soy responsable de reportes")
    
    def enviar_email(self):
        raise NotImplementedError("No soy un gestor de emails")

# Clase con dependencia concreta (viola DIP)
class ReporteEstudiantes:
    def __init__(self):
        self.gestor = GestorMySQL()  # Creación directa, no inyección
    
    def generar(self):
        self.gestor.generar_reporte()  # Esto lanzará excepción

# Clase duplicada (en Python, esto sobrescribiría la anterior, pero si están en el mismo módulo, es un error lógico)
# Vamos a crear otra clase con el mismo nombre en el mismo módulo, lo que sobrescribirá la anterior.
# Esto no es un error de sintaxis, pero es un error de diseño.
class GestorEstudiantes:
    def __init__(self):
        pass  # Esta clase duplicada sobrescribirá la anterior

# Función principal
def main():
    print("=== Proyecto de Análisis de Código ===\n")
    
    # Crear instancia del gestor (la última definición de GestorEstudiantes)
    gestor = GestorEstudiantes()
    
    # Intentar usar método con división por cero (pero ya lo manejamos)
    try:
        promedio = gestor.calcular_promedio_correcto()  # Este método existe en la primera definición, pero ahora GestorEstudiantes es la segunda, que no tiene este método
        print(f"Promedio: {promedio}")
    except Exception as e:
        print(f"Error al calcular promedio: {e}")
    
    # Variable con error de mayúsculas
    mensaje = "Hola"
    print(mensaje)
    
    # Error: variable no existe
    # print(MENSAJE)  # Descomentar para ver error
    
    # Ejemplo de error de tipo
    try:
        resultado = "5" + 3  # TypeError en Python
    except TypeError as e:
        print(f"Error de tipo: {e}")
    
    # Uso de variable antes de asignación (posible error)
    try:
        print(variable_no_definida)  # NameError
    except NameError as e:
        print(f"Error de variable no definida: {e}")
    
    print("\nFin del programa")

# Función con múltiples problemas
def funcion_con_problemas():
    # Variables con nombres confusos
    l = 10  # Nombre de variable de una sola letra
    O = 20  # Se parece a cero
    
    # Función demasiado compleja (viola principios)
    x = 0
    for i in range(100):
        for j in range(100):
            for k in range(100):
                x += i * j * k
    
    return x

# Función que no usa todos sus parámetros
def funcion_parametros_no_usados(param1, param2, param3):
    resultado = param1 * 2
    # param2 y param3 no se usan
    return resultado

# Función con código muerto
def funcion_codigo_muerto():
    resultado = 42
    
    # Código que nunca se ejecuta
    if False:
        print("Esto nunca se ejecuta")
    
    # Return temprano que deja código sin ejecutar
    return resultado
    
    print("Esto nunca se ejecuta")  # Código muerto

if __name__ == "__main__":
    main()
    
    # Probamos la función con código muerto
    print(f"\nResultado función código muerto: {funcion_codigo_muerto()}")
    
    # Probamos la función con parámetros no usados
    print(f"Resultado parámetros no usados: {funcion_parametros_no_usados(5, 10, 15)}")