# Space Invaders Game

## Descripción del Proyecto

Space Invaders es una versión moderna del clásico juego arcade, desarrollado
utilizando la **Uno Platform** para crear una experiencia multiplataforma que
puede ejecutarse en diversas plataformas, incluyendo Windows, WebAssembly, iOS
y Android. En este juego, los jugadores controlan una nave espacial que se
desplaza horizontalmente por la pantalla mientras intenta destruir oleadas de
invasores alienígenas antes de que estos lleguen a la parte inferior de la
pantalla. La simplicidad del juego, combinada con su creciente dificultad,
ofrece un reto divertido y adictivo para jugadores de todas las edades.

## Arquitectura del Proyecto

El proyecto sigue la **Arquitectura MVVM (Model-View-ViewModel)**, que separa
la lógica de la interfaz de usuario (UI) de la lógica del negocio y de los datos.
Esta arquitectura facilita la escalabilidad y el mantenimiento del código,
permitiendo que los componentes de la UI (View) se mantengan separados de la
lógica de la aplicación (ViewModel) y de la gestión de datos (Model).

### Componentes:

- **Model**: Representa los datos del juego, como la posición de los aliens, la nave del jugador, las puntuaciones, y las vidas restantes.
- **View**: Define la interfaz de usuario que los jugadores ven e interactúan. En este caso, se implementa utilizando XAML en Uno Platform.
- **ViewModel**: Actúa como un intermediario entre el View y el Model, manejando la lógica de la aplicación, actualizando la UI en respuesta a los cambios en los datos, y respondiendo a las entradas del usuario.

## Uso de Uno Platform

La **Uno Platform** es un marco que permite el desarrollo de aplicaciones
multiplataforma usando una única base de código. En este proyecto, Uno Platform
se utiliza para construir la UI y gestionar la lógica del juego, asegurando que
la experiencia del usuario sea consistente en todas las plataformas soportadas.

## Almacenamiento de Información

Para el almacenamiento de la información del juego, como las puntuaciones altas
y las configuraciones del jugador, se utilizan **rutas absolutas**.
Esto facilita el acceso y la gestión de los archivos independientemente de la
plataforma en la que se esté ejecutando el juego. Es importante tener en cuenta
que, dado que se utilizan rutas absolutas, es necesario asegurar que las rutas
especificadas sean válidas y accesibles en el sistema del usuario.

## Ejecución del Juego

### Requisitos Previos

- **SDK de .NET 6.0** o superior instalado.
- **Uno Platform** configurado en tu entorno de desarrollo (Visual Studio
- recomendado).

### Pasos para Ejecutar el Juego

1. **Clonar el Repositorio**:
   ```bash
   git clone https://github.com/tu-usuario/space-invaders-game.git
   cd space-invaders-game

**Ejecutar en Windows**
`dotnet build -c Release -f net6.0-windows
dotnet run`
