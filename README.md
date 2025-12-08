# 📘 **Enunciado – Juego de Tablero “Mini-Dungeon” utilizando el Patrón Visitor**

## 🧩 Descripción General

El objetivo de este trabajo es desarrollar un **juego de tablero sencillo**, llamado **Mini-Dungeon**, donde un personaje controlado por el usuario se desplaza en una grilla interactuando con distintos elementos.
El foco principal del ejercicio es **demostrar la correcta aplicación del Patrón de Diseño Visitor**, desacoplando la lógica de movimiento del tablero y evitando condicionales por tipo.

El juego simula un pequeño dungeon donde el jugador debe moverse, empujar cajas y evitar obstáculos.

---

## 🎮 Reglas del Juego

El tablero es una matriz 2D fija (por ejemplo, 6×6 u 8×8).
Cada celda puede contener un elemento móvil (**MovableElement**).
Los elementos presentes son:

* **Player**: controlado por el usuario.
* **Rock**: roca que bloquea el paso y no puede moverse.
* **Box**: caja que puede ser empujada por el Player si la celda siguiente está libre.

El usuario puede mover el Player con las teclas:

```
↑  Arriba  
↓  Abajo  
←  Izquierda  
→  Derecha  
```

### ✔ Comportamientos esperados

* Si el **Player** intenta moverse a una celda vacía → se mueve.
* Si encuentra una **Box** → intenta empujarla (solo si la celda siguiente está libre).
* Si encuentra una **Rock** → el movimiento no ocurre.
* Una **Box** únicamente se desplaza cuando es empujada legalmente por el Player.
* Una **Rock** nunca se mueve.

La lógica que define qué ocurre ante un movimiento **no debe estar dentro del Tablero**, sino delegada al Patrón Visitor.

---

## 🧠 Objetivo del Patrón Visitor

El tablero (**Board**) mantiene posiciones, pero **no debe saber** cómo responde cada tipo de elemento al intentar moverse.
Para ello se utiliza un **MovementVisitor**, que contiene las reglas específicas para cada tipo concreto:

```csharp
Visit(Player)
Visit(Box)
Visit(Rock)
```

Cada elemento implementa su método `Accept(visitor)`, delegando la operación en el Visitor según su tipo real.

Esto evita lógica basada en:

* `if (element is Player)`
* `switch(elementType)`
* `instanceof`
* Código duplicado

y permite agregar nuevos elementos al tablero sin modificar la clase Board.

---

## 🧱 Requisitos Técnicos

* Implementar el patrón **Visitor** para manejar los movimientos del tablero.
* Usar una arquitectura de **4 capas**:

  * **UI** (WinForms)
  * **Aplicación**
  * **Dominio**
  * **Datos**
* El tablero inicial puede cargarse desde base de datos o archivo (JSON, texto, etc.).
* La interfaz de usuario debe mostrar la grilla (sugerido: `DataGridView` o `TableLayoutPanel`).
* El movimiento del jugador debe reflejarse visualmente luego de cada acción.
* No pueden existir condicionales por tipo en Board.

---

## 🎯 Requisitos Funcionales

* El jugador debe poder moverse en las cuatro direcciones.
* Las cajas deben poder empujarse respetando las reglas.
* Las rocas deben bloquear siempre el movimiento.
* La grilla debe actualizarse visualmente en forma inmediata.
* El Visitor debe ser responsable de decidir si un movimiento es válido o no.

---

## 🛠️ Requisitos No Funcionales

* Código limpio, desacoplado y extensible.
* Se deben respetar principios SOLID, especialmente *Open/Closed*.
* La lógica del juego debe ser fácil de extender con nuevos tipos de elementos (por ejemplo, trampas, enemigos, puertas, etc.).

---

## 📝 Justificación del uso del Patrón Visitor

El patrón Visitor permite definir comportamientos diferentes para cada tipo de elemento del tablero sin modificar el Board, evitando acoplamiento, condicionales por tipo y respetando el principio de **abierto/cerrado**.
Además, permite reemplazar fácilmente la lógica del juego (por ejemplo, cambiar las reglas de movimiento) implementando nuevos visitantes sin alterar el código existente.

---

## 📌 Alcance del Trabajo

* Diseño de clases.
* Diagrama UML del patrón aplicado.
* Implementación funcional en WinForms.
* Explicación técnica del Patrón Visitor aplicado al escenario.
* Código documentado.

---

Si querés, puedo generar **el diagrama UML**, **la estructura de carpetas**, **el código base**, o incluso **una versión mínima completamente funcional**. ¿Qué hacemos ahora?
