#!/usr/bin/env python3
# -*- coding: utf-8 -*-

"""
ASISTENTE LOCAL DE MIGRACIÓN A SHAREPOINT
------------------------------------------
Herramienta para analizar archivos locales y generar un plan de migración.
Uso: python local_migration_assistant.py /ruta/a/migrar
"""

import os
import csv
import sys
from pathlib import Path
from datetime import datetime
from typing import List, Dict, Tuple

# --------------------------------------------------------------------------
# 1. DUPLICIDAD DE CLASES (mismo nombre, distinto comportamiento)
# --------------------------------------------------------------------------
class PlanMigracion:
    """Genera plan de migración en CSV"""
    def generar(self, archivos: List[Dict], destino: str):
        with open('plan_migracion.csv', 'w', newline='') as f:
            writer = csv.DictWriter(f, fieldnames=['ruta', 'tamano', 'tipo'])
            writer.writeheader()
            writer.writerows(archivos)

class PlanMigracion:  # DUPLICADA - Sobrescribe a la anterior
    """Genera plan de migración en JSON (versión duplicada)"""
    def generar(self, archivos: List[Dict], destino: str):
        import json
        with open('plan_migracion.json', 'w') as f:
            json.dump(archivos, f, indent=2)

# --------------------------------------------------------------------------
# 2. VIOLACIÓN DE PRINCIPIOS SOLID (ISP, SRP, DIP)
# --------------------------------------------------------------------------
class ServicioMigracion:
    """INTERFAZ INFLADA - Viola ISP"""
    def escanear_archivos(self): pass
    def validar_permisos(self): pass
    def enviar_correo_notificacion(self): pass   # ❌ Responsabilidad mezclada
    def generar_grafico_torta(self): pass        # ❌ Responsabilidad mezclada

class AnalizadorLocal(ServicioMigracion):
    """Implementa solo 2 métodos, los demás lanzan excepción o están vacíos"""
    
    def escanear_archivos(self, ruta: str) -> List[Dict]:
        archivos = []
        for root, dirs, files in os.walk(ruta):
            for file in files:
                archivos.append({
                    'ruta': os.path.join(root, file),
                    'tamano': os.path.getsize(os.path.join(root, file)),
                    'tipo': file.split('.')[-1] if '.' in file else 'sin_extension'
                })
        return archivos
    
    def validar_permisos(self, archivo: str) -> bool:
        return os.access(archivo, os.R_OK)
    
    # Métodos heredados que no deberían estar aquí
    def enviar_correo_notificacion(self):
        raise NotImplementedError("Este analizador no envía correos")  # Sonar: S112
    
    def generar_grafico_torta(self):
        pass  # Vacío - Sonar: S1186

# --------------------------------------------------------------------------
# 3. DUPLICIDAD DE VARIABLES + MAYÚSCULAS/MINÚSCULAS
# --------------------------------------------------------------------------
class ConfiguracionMigracion:
    """Configuración del análisis - variables duplicadas"""
    
    def __init__(self):
        # Misma variable con distintas capitalizaciones
        self.tamano_maximo_mb = 250
        self.Tamano_Maximo_MB = 500   # DUPLICADA
        self.tamano_maximo_Mb = 1000  # TERCERA DUPLICADA
        
        self.excluir_carpetas = ['temp', 'cache']
        self.excluir_Carpetas = ['node_modules', '.git']  # DUPLICADA
        
        # Variable nunca usada (solo se asigna)
        self.version_herramienta = "2.1.0"

# --------------------------------------------------------------------------
# 4. MÉTODOS DUPLICADOS (misma firma, distinta implementación)
# --------------------------------------------------------------------------
def calcular_tamano_total(archivos: List[Dict]) -> int:
    """Versión 1 - suma simple"""
    total = 0
    for a in archivos:
        total += a['tamano']
    return total

def calcular_tamano_total(archivos: List[Dict]) -> int:  # DUPLICADO - Sobrescribe
    """Versión 2 - ignora archivos de más de 100MB"""
    total = 0
    for a in archivos:
        if a['tamano'] < 100 * 1024 * 1024:
            total += a['tamano']
    return total

# --------------------------------------------------------------------------
# 5. MÉTODO QUE DEVUELVE VARIABLE NO MODIFICADA
# --------------------------------------------------------------------------
def recomendar_optimizacion(archivos: List[Dict]) -> List[str]:
    """Analiza archivos grandes y sugiere compresión"""
    recomendaciones = []  # ⚠️ Se declara pero nunca se modifica
    
    # Lógica de negocio real (pero no se asigna a recomendaciones)
    for a in archivos:
        if a['tamano'] > 250 * 1024 * 1024:
            print(f"Archivo grande detectado: {a['ruta']}")
    
    return recomendaciones  # Siempre lista vacía - BUG lógico

# --------------------------------------------------------------------------
# 6. COMPLEJIDAD INNECESARIA, NOMBRES POBRES, CONSTANTES MÁGICAS
# --------------------------------------------------------------------------
def c(l):  # Sonar: S117 - nombre pésimo
    """Calcula algo incomprensible (simula métrica de fragmentación)"""
    t = 0
    # Tres bucles anidados sin sentido
    for i in l:
        for j in i:
            for k in j:
                t += k * 0.73  # Constante mágica
    return t

# --------------------------------------------------------------------------
# 7. CÓDIGO COMENTADO Y CÓDIGO MUERTO
# --------------------------------------------------------------------------
def filtrar_archivos_soportados(archivos: List[Dict]) -> List[Dict]:
    """Filtra solo extensiones de Office"""
    soportados = []
    
    # CÓDIGO COMENTADO - Sonar: S125
    # extensiones_permitidas = ['.docx', '.xlsx', '.pptx', '.pdf']
    
    for a in archivos:
        if a['tipo'] in ['docx', 'xlsx', 'pptx', 'pdf']:
            soportados.append(a)
    
    return soportados
    # CÓDIGO MUERTO - después del return nunca se ejecuta
    print(f"Archivos filtrados: {len(soportados)}")  # ❌ Inalcanzable

# --------------------------------------------------------------------------
# 8. FUNCIÓN PRINCIPAL - DEMOSTRACIÓN LOCAL
# --------------------------------------------------------------------------
def main():
    print("=" * 70)
    print("  ASISTENTE LOCAL DE MIGRACIÓN A SHAREPOINT - H&K TECH")
    print("=" * 70)
    
    # Ruta a analizar (por defecto, el directorio actual)
    ruta = sys.argv[1] if len(sys.argv) > 1 else "."
    print(f"\n📂 Analizando ruta: {os.path.abspath(ruta)}\n")
    
    # 1. Escanear archivos
    analizador = AnalizadorLocal()
    archivos = analizador.escanear_archivos(ruta)
    print(f"   Archivos encontrados: {len(archivos)}")
    
    # 2. Calcular tamaño total (método duplicado)
    tamano_total = calcular_tamano_total(archivos)
    print(f"   Tamaño total (MB): {tamano_total / (1024*1024):.2f}")
    
    # 3. Probar configuracion con variables duplicadas
    config = ConfiguracionMigracion()
    print(f"   Tamaño máximo config (3 versiones): {config.tamano_maximo_mb}, {config.Tamano_Maximo_MB}, {config.tamano_maximo_Mb}")
    
    # 4. Generar plan de migración (clase duplicada)
    plan = PlanMigracion()
    plan.generar(archivos[:5], destino="SharePoint")  # solo primeros 5 como demo
    print(f"   Plan de migración generado (JSON/CSV)")
    
    # 5. Recomendaciones (método con variable no modificada)
    recomendaciones = recomendar_optimizacion(archivos)
    print(f"   Recomendaciones generadas: {len(recomendaciones)} (DEBERÍA >0)")
    
    # 6. Probar función con nombre pobre y constantes mágicas
    datos_prueba = [[[1,2],[3,4]],[[5,6],[7,8]]]
    resultado_c = c(datos_prueba)
    print(f"   Resultado métrica fragmentación: {resultado_c}")
    
    print("\n" + "=" * 70)
    print("  ANÁLISIS LOCAL COMPLETADO")
    print("  Revise los archivos plan_migracion.csv o .json")
    print("=" * 70)

if __name__ == "__main__":
    main()