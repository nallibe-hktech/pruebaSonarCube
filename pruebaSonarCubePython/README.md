<h1 align="center"> Análisis Estático con SonarQube</h1>

<p align="center">
Plataforma de análisis continuo para evaluación de calidad, mantenibilidad y seguridad del código.
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Tool-SonarQube-blue" />
  <img src="https://img.shields.io/badge/Language-Python-yellow" />
  <img src="https://img.shields.io/badge/Analysis-Static-green" />
  <img src="https://img.shields.io/badge/CI/CD-Integrated-orange" />
</p>

---

# 1. Introducción

**SonarQube** es una plataforma de análisis estático de código desarrollada por **SonarSource**, diseñada para evaluar la calidad del software sin necesidad de ejecutarlo.

Permite detectar:

- Bugs
- Vulnerabilidades de seguridad
- Código duplicado
- Code Smells
- Deuda técnica
- Problemas de mantenibilidad

Soporta más de 35 lenguajes y se integra fácilmente en entornos DevOps modernos.

---

# 2. Metodología de Pruebas

## 2.1 Entorno

- SonarQube desplegado mediante **Docker**
- Proyecto de prueba desarrollado en **Python**
- Reglas por defecto del perfil de calidad para Python

---

## 2.2 Procedimiento

1. Configuración y levantamiento de SonarQube.
2. Análisis de aplicación Python con errores intencionales.
3. Evaluación de resultados con reglas por defecto.
4. Modificación del Quality Gate.
5. Reanálisis tras correcciones.

---

# 3. Resultados del Análisis

## 3.1 Detección de Incidencias

El sistema clasificó los problemas en:

- 🚫 Bloqueantes
- 🔴 Críticos
- 🟠 Altos
- 🟡 Medios
- 🟢 Bajos

<img src="\images\level errores.png" />

Además:

- Identifica línea exacta del error
- Proporciona recomendación de corrección
- Visualiza gráficamente la distribución del código afectado

<img src="\images\tags.png" />

---

## 3.2 Evaluación del Quality Gate

Inicialmente:

Quality Gate: **Passed**  
(A pesar de existir errores de alta severidad)
<img src="\images\passed python inicial.png" />

Tras redefinir condiciones:

- Mantenibilidad obligatoria en nivel **A**
- Complejidad cognitiva promedio ≤ 15
- Código duplicado ≤ 5%

<img src="\images\add quality.png" />

Resultado:

Quality Gate: **Failed**

<img src="\images\failed py tras quality.png" />

Después de corregir el código y reanalizar:

Quality Gate: **Passed**

<img src="\images\passed python corregido.png" />


---

# 4. Integración DevOps

SonarQube permite integración con:

- Git
- Jenkins
- GitHub Actions
- GitLab CI
- Azure DevOps

El análisis puede ejecutarse automáticamente en:

- Commits
- Pull Requests
- Merges

También se probó **SonarQube Cloud**, que simplifica la integración sin necesidad de instalación local, pero esta no permite en su versión gratuita añadir o modificar los Quality Gates

img src="\images\cloud.png" />

img src="\images\cloud quality.png" />

img src="\images\passed cloud.png" />

---

#  5. Conclusión de Investigación

Tras la realización de las pruebas prácticas, se concluye que SonarQube es una herramienta sólida y eficaz para el control de calidad del software en entornos de desarrollo modernos.

Los resultados obtenidos demuestran que:

1. La herramienta detecta con precisión errores estructurales, problemas de mantenibilidad y vulnerabilidades potenciales.
2. El análisis estático permite identificar fallos antes de la fase de producción, reduciendo riesgos.
3. La configuración del Quality Gate es un elemento crítico: con parámetros por defecto puede aprobar código con incidencias relevantes.
4. Al definir métricas estrictas (mantenibilidad, complejidad y duplicación), se convierte en un mecanismo efectivo para prevenir deuda técnica.
5. La integración con pipelines DevOps automatiza el control de calidad y fomenta buenas prácticas en equipos de desarrollo.

Desde un enfoque investigativo, los resultados obtenidos evidencian que SonarQube trasciende su función como simple detector de errores, posicionándose como una herramienta estratégica de gobernanza del código. Su capacidad para aplicar métricas objetivas y políticas de calidad configurables permite establecer estándares medibles, reproducibles y sostenibles en el tiempo.

La integración de SonarQube dentro del ciclo de desarrollo demuestra que puede actuar como un mecanismo automatizado de control de calidad, funcionando como un gatekeeper que condiciona el avance del código en el pipeline en función del cumplimiento de criterios previamente definidos. De este modo, no solo identifica defectos, sino que previene activamente la propagación de deuda técnica hacia fases posteriores del proceso de integración y despliegue.

En consecuencia, SonarQube se consolida como una solución eficaz para implementar estrategias de mejora continua en la calidad del software, especialmente cuando sus Quality Gates y perfiles de calidad se alinean con los objetivos técnicos y organizacionales del proyecto.
