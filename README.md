MiTerreno - Proyecto Unity

Este es un proyecto de un juego en Unity que simula un entorno 3D con personajes interactivos. El objetivo es crear un mundo virtual donde el jugador pueda moverse, interactuar con objetos (como cubos/zombis) y realizar acciones como saltar, caminar, atacar y más.

Características
Personaje principal:
Movimiento en 3D con las teclas WASD.
Cámara en tercera persona.
Animaciones de caminar, correr, saltar y atacar.
Interacciones:
El jugador puede atacar a los enemigos (representados como cubos) al hacer clic con el mouse.
El personaje puede saltar presionando la tecla Space.
Entorno:
El escenario incluye objetos y obstáculos en 3D.
Se utiliza una configuración básica de iluminación y clima (como lluvia y paisajes).
Requisitos del sistema
Unity 2023.1 o superior
Git LFS para manejar archivos grandes del proyecto (como texturas y modelos 3D).
Instalación

Clona este repositorio:

git clone https://github.com/AbelRamos16/MiTerreno.git

Asegúrate de tener Git LFS instalado:
Si no lo tienes, puedes instalarlo usando:

git lfs install
Abre el proyecto en Unity:
Abre Unity Hub y selecciona Open y navega hasta la carpeta del proyecto.
Abre el proyecto MiTerreno.
Controles del juego
Movimiento:
W = Avanzar.
A = Moverse a la izquierda.
S = Retroceder.
D = Moverse a la derecha.
Cámara:
Mueve el ratón para rotar la cámara y ver el entorno en 360 grados.
Acciones:
Saltar: Presiona la tecla Space.
Atacar: Haz clic con el botón izquierdo del ratón.
Descripción técnica
Estructura del Proyecto
Assets: Contiene todos los modelos 3D, texturas y materiales utilizados en el juego.
Scripts: Contiene los archivos de código C# que gestionan el movimiento, las animaciones, la interacción con los objetos y otras mecánicas del juego.
Scenes: Las escenas que componen el juego (en este caso, una única escena de ejemplo).
Git LFS
Los archivos de gran tamaño (como texturas .tga y modelos 3D .fbx) se gestionan a través de Git LFS para evitar sobrecargar el repositorio de GitHub con archivos grandes.
Animaciones

El personaje tiene animaciones para:

Caminar (Idle, RunForward, RunBackward).
Saltar (Jump).
Atacar (Attack).
Próximos pasos
Mejorar la lógica de ataques para incluir más animaciones y efectos visuales.
Añadir más interacciones con el entorno (por ejemplo, abrir puertas, recoger objetos).
Optimizar el rendimiento para que el juego funcione de manera fluida en dispositivos más antiguos.
Contribuciones

Si deseas contribuir a este proyecto, por favor sigue estos pasos:

Haz un fork de este repositorio.
Crea una nueva rama (git checkout -b nombre-de-la-rama).
Realiza tus cambios y haz un commit (git commit -am 'Añadir nueva característica').
Haz push a tu rama (git push origin nombre-de-la-rama).
Crea un pull request.
