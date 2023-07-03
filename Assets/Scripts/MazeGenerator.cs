using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeGenerator : MonoBehaviour
{
    public int mazeWidth;
    public int mazeHeight;
    public GameObject wallPrefab;
    public GameObject groundPrefab;
    public GameObject progressPrefab;
    public GameObject taxiPrefab;
    public GameObject endPointPrefab;
    public Vector2 cellSize;

    private GameObject endPoint;
    private Camera mainCamera;
    private GameObject[,] mazeCells;
    private Stack<Vector2> stack;
    private Vector2 currentCell;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    
    public void GenerateFull()
    {
        GenerateMaze();
        GenerateWall();
        GenerateGround();
    }
    
    public void ReGenerateFull()
    {
        DeleteMaze();
        GenerateMaze();
        GenerateWall();
        GenerateGround();
    }
    
    public void GenerateGame()
    {
        GenerateMaze();
        GenerateWall();
        GenerateGround();
        GenerateProgress();
        GenerateTaxi();
    }
    
    public void ReGenerateGame()
    {
        DeleteMaze();
        GenerateMaze();
        GenerateWall();
        GenerateGround();
        GenerateProgress();
        GenerateTaxi();
    }
    
    private void GenerateWall()
    {
        for (int x = -1; x < mazeWidth; x++)
        {
            Instantiate(wallPrefab, new Vector3(x, 1, -1), Quaternion.identity);
            Instantiate(wallPrefab, new Vector3(x, 1, mazeHeight -1), Quaternion.identity);
        }
        for (int y = -1; y < mazeHeight; y++)
        {
            Instantiate(wallPrefab, new Vector3(-1, 1, y), Quaternion.identity);
            Instantiate(wallPrefab, new Vector3(mazeWidth -1, 1, y), Quaternion.identity);
        }
    }

    private void GenerateGround()
    {
        GameObject newObject = Instantiate(groundPrefab, new Vector3(mazeWidth / 2 - 1, 0, mazeHeight / 2 - 1), Quaternion.identity);
        newObject.transform.localScale = new Vector3(mazeWidth / 10f, 0, mazeHeight / 10f);
    }
    
    
    private void GenerateProgress()
    {
        Vector2 randomPos2 = GetRandomEmptyPosition();
        Vector2 randomPos1 = GetRandomEmptyPosition();
        // Start point
        Instantiate(progressPrefab, new Vector3(cellSize.x / 2 - 0.5f, 1, cellSize.y / 2 - 0.5f), Quaternion.identity); 
        // Random point 1
        Instantiate(progressPrefab, new Vector3(randomPos1.x * cellSize.x + cellSize.x / 2 - 0.5f, 1, randomPos1.y * cellSize.y + cellSize.y / 2 - 0.5f), Quaternion.identity);
        // Random point 2
        Instantiate(progressPrefab, new Vector3(randomPos2.x * cellSize.x + cellSize.x / 2 - 0.5f, 1, randomPos2.y * cellSize.y + cellSize.y / 2 - 0.5f), Quaternion.identity);
        // End point
        endPoint = Instantiate(endPointPrefab, new Vector3((mazeWidth - 2) * cellSize.x + cellSize.x / 2 - 0.5f, 1, (mazeHeight - 2) * cellSize.y + cellSize.y / 2 - 0.5f), Quaternion.identity);
    }
    
    
    private Vector2 GetRandomEmptyPosition()
    {
        int maxAttempts = 100;
        int attempts = 0;

        while (attempts < maxAttempts)
        {
            int randomX = Random.Range(0, mazeWidth);
            int randomY = Random.Range(0, mazeHeight);

            if (!mazeCells[randomX, randomY].activeSelf) // Check if the maze cell is inactive (wall)
            {
                return new Vector2(randomX, randomY);
            }

            attempts++;
        }

        Debug.LogWarning("Failed to find an empty position for the circle.");
        return Vector2.zero;
    }
    
    private void GenerateTaxi()
    {
        GameObject taxi = Instantiate(taxiPrefab, new Vector3(0f, 1.2f, 0f), Quaternion.identity);
        taxi.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        taxi.GetComponent<TaxiMovement>().EndPoint = endPoint;
    }

    private void GenerateMaze()
    {
        // Initialize the maze cells array
        mazeCells = new GameObject[mazeWidth, mazeHeight];

        // Create the maze
        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                mazeCells[x, y] = Instantiate(wallPrefab, new Vector3(x * cellSize.x, 1, y * cellSize.y), Quaternion.identity);
            }
        }
    
        // Placing camera responsive to maze size
        // Calculate maze center position
        Vector3 mazeCenter = new Vector3(mazeWidth * cellSize.x / 2f, 0f, mazeHeight * cellSize.y / 2f);

        // Adjust camera position
        Camera.main.transform.position = new Vector3(mazeCenter.x - 1, Camera.main.transform.position.y, mazeCenter.z - 1);

        // Calculate the desired camera size based on maze dimensions
        float targetCameraSize = Mathf.Max(mazeWidth * cellSize.x / 2f, mazeHeight * cellSize.y / 2f);

        Camera.main.orthographicSize = targetCameraSize;
        
        if (Screen.width < Screen.height)
        {
            // Get the screen aspect ratio
            float screenAspect = (float)Screen.width / (float)Screen.height;
            // Calculate the desired camera height based on the target camera size and screen aspect ratio
            float targetCameraHeight = targetCameraSize / screenAspect;
            // Check if the desired camera height is greater than the screen height
            if (targetCameraHeight > mazeHeight * cellSize.y)
            {
                // Limit the camera height to the screen height
                targetCameraHeight = mazeHeight * cellSize.y;
            }
            // Calculate the camera size based on the desired camera height and maze aspect ratio
            float cameraSize = targetCameraHeight / mazeHeight * mazeWidth;
            // Adjust camera size
            Camera.main.orthographicSize = cameraSize;
        }
        // Initialize the stack and starting cell
        stack = new Stack<Vector2>();
        currentCell = Vector2.zero;
        stack.Push(currentCell);

        StartCoroutine(GenerateMazeCoroutine());
    }

    private IEnumerator GenerateMazeCoroutine()
    {
        while (stack.Count > 0)
        {
            // Mark the current cell as visited
            mazeCells[(int)currentCell.x, (int)currentCell.y].SetActive(false); // Set the current cell to inactive

            // Get the unvisited neighbors of the current cell
            List<Vector2> unvisitedNeighbors = GetUnvisitedNeighbors(currentCell);

            if (unvisitedNeighbors.Count > 0)
            {
                // Choose a random unvisited neighbor
                int randomIndex = Random.Range(0, unvisitedNeighbors.Count);
                Vector2 randomNeighbor = unvisitedNeighbors[randomIndex];

                // Push the current cell to the stack
                stack.Push(currentCell);

                // Remove the wall between the current cell and the chosen neighbor
                RemoveWall(currentCell, randomNeighbor);

                // Set the chosen neighbor as the current cell
                currentCell = randomNeighbor;
            }
            else if (stack.Count > 0)
            {
                // Backtrack to the previous cell
                currentCell = stack.Pop();
            }
        }
        yield return null;
    }

    private List<Vector2> GetUnvisitedNeighbors(Vector2 cell)
    {
        List<Vector2> neighbors = new List<Vector2>();

        // Check the top neighbor
        if (cell.y + 2 < mazeHeight && mazeCells[(int)cell.x, (int)cell.y + 2].activeSelf)
        {
            neighbors.Add(new Vector2(cell.x, cell.y + 2));
        }

        // Check the right neighbor
        if (cell.x + 2 < mazeWidth && mazeCells[(int)cell.x + 2, (int)cell.y].activeSelf)
        {
            neighbors.Add(new Vector2(cell.x + 2, cell.y));
        }

        // Check the bottom neighbor
        if (cell.y - 2 >= 0 && mazeCells[(int)cell.x, (int)cell.y - 2].activeSelf)
        {
            neighbors.Add(new Vector2(cell.x, cell.y - 2));
        }

        // Check the left neighbor
        if (cell.x - 2 >= 0 && mazeCells[(int)cell.x - 2, (int)cell.y].activeSelf)
        {
            neighbors.Add(new Vector2(cell.x - 2, cell.y));
        }

        return neighbors;
    }

    private void RemoveWall(Vector2 cell1, Vector2 cell2)
    {
        int wallX = (int)((cell1.x + cell2.x) / 2);
        int wallY = (int)((cell1.y + cell2.y) / 2);

        mazeCells[wallX, wallY].SetActive(false); // Set the wall between the two cells to inactive
    }

    public void DeleteMaze()
    {
        // Destroy all maze cells
        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                Destroy(mazeCells[x, y]);
            }
        }
        // Destroy all walls
        GameObject[] mazes = GameObject.FindGameObjectsWithTag("Maze");
        foreach (GameObject maze in mazes)
        {
            Destroy(maze);
        }
    }
}
