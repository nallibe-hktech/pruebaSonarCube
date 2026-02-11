ANÁLISIS DE MANTENIBILIDAD CON SONARQUBE (PYTHON)

Este proyecto tiene como objetivo analizar la mantenibilidad de un código Python utilizando SonarQube en local y demostrar cómo el resultado del análisis depende de la configuración del Quality Gate aplicado.

Se ha utilizado un mismo código con problemas de mantenibilidad detectables por SonarQube. El análisis se ha realizado bajo dos configuraciones distintas:

1. Quality Gate genérico de SonarQube
   - Resultado: PASSED
   - Maintainability Issues detectados: 10
   - Security y Reliability: A

2. Quality Gate personalizado (mantenibilidad estricta)
   - Condición: Maintainability Issues > 5
   - Resultado: FAILED
   - Motivo del fallo: exceso de problemas de mantenibilidad

El código analizado es el mismo en ambos casos. El único cambio realizado ha sido la configuración del Quality Gate.

Este análisis demuestra que SonarQube evalúa la calidad del código en función de los criterios definidos por el Quality Gate y no de forma absoluta.
