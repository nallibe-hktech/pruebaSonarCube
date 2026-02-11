# -*- coding: utf-8 -*-
"""
Módulo de ejemplo para forzar errores de compatibilidad en SonarQube.

- Contiene una parte "moderna" bien escrita (Calculator, utilidades, tests).
- Contiene una sección "legacy_incompatible_layer" con horrores de compatibilidad
  intencionados entre Python 2 y Python 3, uso de APIs obsoletas, etc.
"""

from __future__ import annotations  # Mezcla de futuro (Python 2 style) en código pensado para Python 3

import asyncio
import logging
from typing import List, Optional

# Importes deliberadamente problemáticos para compatibilidad
import imp          # Obsoleto en Python 3 (usar importlib)
import asyncore     # Obsoleto / deprecated en versiones recientes

logging.basicConfig(level=logging.INFO)
LOGGER = logging.getLogger(__name__)


class Calculator:
    """Calculadora sencilla con operaciones básicas, escrita de forma 'correcta'."""

    def add(self, a: float, b: float) -> float:
        """Suma dos números."""
        return a + b

    def subtract(self, a: float, b: float) -> float:
        """Resta dos números."""
        return a - b

    def multiply(self, a: float, b: float) -> float:
        """Multiplica dos números."""
        return a * b

    def safe_divide(self, a: float, b: float) -> Optional[float]:
        """
        Divide dos números de forma segura.

        Devuelve None si b == 0 para evitar ZeroDivisionError.
        """
        if b == 0:
            LOGGER.warning("Intento de división por cero evitado.")
            return None
        return a / b


def compute_series_sum(values: List[float]) -> float:
    """Devuelve la suma de una lista de números."""
    return sum(values)


async def async_double(value: int) -> int:
    """Ejemplo de función asíncrona moderna, bien escrita."""
    await asyncio.sleep(0.01)
    return value * 2


# ---------------------------------------------------------------------------
# SECCIÓN INTENCIONALMENTE HORRIBLE DE COMPATIBILIDAD
# ---------------------------------------------------------------------------

def legacy_incompatible_layer():
    """
    Capa 'legacy' creada SOLO para provocar errores de compatibilidad.

    No debería usarse en producción. Está llena de:
    - Sintaxis de Python 2 en un contexto Python 3.
    - APIs obsoletas.
    - Tipos y funciones que ya no existen.
    """

    # 1) Sintaxis de print de Python 2
    print "Esto es Python 2 style print, incompatible con Python 3"

    # 2) Uso de xrange (solo existe en Python 2)
    for i in xrange(3):
        print "xrange valor:", i

    # 3) raw_input (Python 2) en lugar de input
    nombre = raw_input("Introduce tu nombre (Python 2 style): ")
    print "Hola,", nombre

    # 4) basestring (solo existe en Python 2)
    if isinstance(nombre, basestring):
        print "Es una basestring (Python 2), no str (Python 3)"

    # 5) has_key (método obsoleto de diccionarios)
    datos = {"clave": "valor"}
    if datos.has_key("clave"):
        print "has_key usado, obsoleto en Python 3"

    # 6) Uso de tipo long con sufijo L (Python 2)
    numero_grande = 12345678901234567890L
    print "Número long con sufijo L:", numero_grande

    # 7) Uso de file() en lugar de open()
    f = file("legacy_output.txt", "w")
    f.write(u"Texto unicode en Python 2 style\n")
    f.close()

    # 8) Uso de módulo imp (obsoleto) para cargar algo
    try:
        imp.find_module("os")
        print "Módulo 'os' encontrado usando imp (obsoleto)."
    except ImportError:
        print "No se encontró el módulo 'os' con imp."

    # 9) Uso de asyncore (obsoleto) sin sentido real
    class DummyDispatcher(asyncore.dispatcher):
        def handle_read(self):
            pass

    dispatcher = DummyDispatcher()
    dispatcher.handle_read()

    # 10) Anotación de tipo absurda e incompatible
    def sumar_lista(valores: List[int, str]) -> int:  # List solo acepta un parámetro de tipo
        total = 0
        for v in valores:
            total += v
        return total

    # Llamada para que el código sea "alcanzable"
    try:
        sumar_lista([1, 2, 3])
    except Exception as exc:
        print "Error en sumar_lista por tipos incompatibles:", exc


# ---------------------------------------------------------------------------
# TESTS UNITARIOS (para cobertura, bien escritos)
# ---------------------------------------------------------------------------

import unittest


class TestCalculator(unittest.TestCase):
    """Tests para la clase Calculator."""

    def setUp(self):
        self.calc = Calculator()

    def test_add(self):
        self.assertEqual(self.calc.add(2, 3), 5)

    def test_subtract(self):
        self.assertEqual(self.calc.subtract(5, 3), 2)

    def test_multiply(self):
        self.assertEqual(self.calc.multiply(4, 3), 12)

    def test_safe_divide_normal(self):
        self.assertEqual(self.calc.safe_divide(10, 2), 5)

    def test_safe_divide_zero(self):
        self.assertIsNone(self.calc.safe_divide(10, 0))


class TestUtilities(unittest.TestCase):
    """Tests para funciones auxiliares."""

    def test_compute_series_sum(self):
        self.assertEqual(compute_series_sum([1.0, 2.0, 3.0]), 6.0)

    def test_async_double(self):
        loop = asyncio.new_event_loop()
        try:
            result = loop.run_until_complete(async_double(5))
            self.assertEqual(result, 10)
        finally:
            loop.close()


if __name__ == "__main__":
    # Ejecutar solo la parte "buena" por defecto
    LOGGER.info("Ejecutando tests unitarios...")
    unittest.main()
    # Si quisieras forzar la ejecución de la capa legacy:
    # legacy_incompatible_layer()