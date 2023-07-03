# Procedural Maze Generation with Recursive Backtracking in Unity and Taxi Minigame

This project combines procedural maze generation using the recursive backtracking algorithm with a fun taxi minigame, all implemented in Unity. The maze generation algorithm creates intricate and challenging mazes, while the taxi minigame adds an entertaining element to navigate through the maze.

## Maze Generation

The maze generation utilizes the recursive backtracking algorithm, a popular method for creating randomized mazes. This algorithm works by starting at a specific point and recursively exploring adjacent cells, carving passages and creating walls as it progresses. Here's an overview of the maze generation process:

1. Initialize a grid with cells, each initially surrounded by walls.
2. Choose a starting point and mark it as visited.
3. While there are unvisited cells:
   - Randomly select a neighboring cell that has not been visited.
   - Carve a passage between the current cell and the chosen cell.
   - Recursively call the algorithm on the chosen cell.
4. Once all cells have been visited, the maze is complete.

## Taxi Minigame

To make the maze more engaging, a taxi minigame has been added using Unity's game development capabilities. The objective is to navigate a taxi through the generated maze and reach a designated destination. The taxi can move in four directions: up, down, left, and right. Here are the key features of the taxi minigame:

1. Controls:
   - Use the arrow keys or WASD to move the taxi.
   - Press the Esc key to pause the game.
2. Objective:
   - Start at the entrance of the maze and drive the taxi to the exit.
   - Get to the 2 Points in the maze
4. Additional Features:
   - Visual indicators for the taxi's position, the exit, and the destination.

## Getting Started

To run the procedural maze generation and taxi minigame in Unity, follow these steps:

1. Download and install Unity Hub from the Unity website.
2. Clone the project repository from [GitHub](https://github.com/FadingFire/MazeGameUnity).
3. Open Unity Hub and click on the "Projects" tab.
4. Click the "Add" button and navigate to the cloned project folder.
5. Select the project and click "Open."
6. Unity will load the project. Once loaded, navigate to the "Scenes" folder and open the main scene.
7. Press the Play button to start the game in the Unity Editor.
