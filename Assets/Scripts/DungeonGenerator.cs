using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    [System.Serializable]
    public class Rule
    {
        public GameObject room;
        public Vector2Int minPosition;
        public Vector2Int maxPosition;

        public bool obligatory;

        public int ProbabilityOfSpawning(int x, int y)
        {
            // 0 - cannot spawn 1 - can spawn 2 - HAS to spawn

            if (x>= minPosition.x && x<=maxPosition.x && y >= minPosition.y && y <= maxPosition.y)
            {
                return obligatory ? 2 : 1;
            }

            return 0;
        }

    }

    public GameObject skeletonPrefab;

    public GameObject playerPrefab; // Assign this in the Inspector
    private Vector3 spawnPosition = Vector3.zero; // Default spawn point


    public Vector2Int size;
    public int startPos = 0;
    public Rule[] rooms;
    public Vector2 offset;

    List<Cell> board;

    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
    }

void GenerateDungeon()
{
    GameObject firstRoom = null;
    List<Vector3> validRoomPositions = new List<Vector3>();

    for (int i = 0; i < size.x; i++)
    {
        for (int j = 0; j < size.y; j++)
        {
            Cell currentCell = board[(i + j * size.x)];
            if (currentCell.visited)
            {
                int randomRoom = -1;
                List<int> availableRooms = new List<int>();

                for (int k = 0; k < rooms.Length; k++)
                {
                    int p = rooms[k].ProbabilityOfSpawning(i, j);

                    if (p == 2)
                    {
                        randomRoom = k;
                        break;
                    }
                    else if (p == 1)
                    {
                        availableRooms.Add(k);
                    }
                }

                if (randomRoom == -1)
                {
                    if (availableRooms.Count > 0)
                    {
                        randomRoom = availableRooms[Random.Range(0, availableRooms.Count)];
                    }
                    else
                    {
                        randomRoom = 0;
                    }
                }

                var newRoom = Instantiate(rooms[randomRoom].room, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                newRoom.UpdateRoom(currentCell.status);
                newRoom.name += " " + i + "-" + j;

                if (firstRoom == null) // Store the first valid room
                {
                    firstRoom = newRoom.gameObject;
                    spawnPosition = firstRoom.transform.position;
                }
                else
                {
                    validRoomPositions.Add(newRoom.transform.position); // Add room position to list
                }
            }
        }
    }

    // Remove the player's spawn room from valid choices
    validRoomPositions.Remove(spawnPosition);

    // Ensure at least 5 skeletons spawn
    int skeletonCount = Mathf.Min(50, validRoomPositions.Count); // Prevent out-of-bounds error

    if (skeletonCount > 0)
    {
        List<Vector3> selectedRooms = new List<Vector3>();

        // Shuffle the list to ensure randomness
        List<Vector3> shuffledRooms = new List<Vector3>(validRoomPositions);
        for (int i = 0; i < shuffledRooms.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffledRooms.Count);
            (shuffledRooms[i], shuffledRooms[randomIndex]) = (shuffledRooms[randomIndex], shuffledRooms[i]);
        }

        // Select the first 'skeletonCount' rooms from the shuffled list
        for (int i = 0; i < skeletonCount; i++)
        {
            selectedRooms.Add(shuffledRooms[i]);
        }

        // Spawn skeletons in selected rooms
        foreach (Vector3 roomPosition in selectedRooms)
        {
            SpawnSkeleton(roomPosition);
        }
    }

    // Spawn player after dungeon is created
    SpawnPlayer();

    }

// Function to spawn the Skeleton
    void SpawnSkeleton(Vector3 position)
    {
        if (skeletonPrefab != null)
        {
            Instantiate(skeletonPrefab, position + Vector3.up * 2.0f, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Skeleton prefab is not assigned in the Inspector!");
        }
    }



    void SpawnPlayer()
    {
        if (playerPrefab != null && IsPositionValid(spawnPosition))
        {
            Instantiate(playerPrefab, spawnPosition + Vector3.up * 1.5f, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Spawn position is invalid! Find a new location.");
        }
    }


    bool IsPositionValid(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 0.5f);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Wall")) // Make sure walls have the "Wall" tag
            {
                return false;
            }
        }
        return true;
    }


    void MazeGenerator()
    {
        board = new List<Cell>();

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos;

        Stack<int> path = new Stack<int>();

        int k = 0;

        while (k < 1000)
        {
            k++;

            board[currentCell].visited = true;

            if (currentCell == board.Count - 1)
            {
                break;
            }

            // Check the cell's neighbors
            List<int> neighbors = CheckNeighbors(currentCell);

            if (neighbors.Count == 0)
            {
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);

                // Randomly decide whether to create a loop
                if (Random.Range(0, 100) < 50) // 50% chance to create a loop
                {
                    List<int> visitedNeighbors = CheckVisitedNeighbors(currentCell);
                    if (visitedNeighbors.Count > 0)
                    {
                        int loopCell = visitedNeighbors[Random.Range(0, visitedNeighbors.Count)];
                        ConnectCells(currentCell, loopCell);
                        continue;
                    }
                }

                int newCell = neighbors[Random.Range(0, neighbors.Count)];
                ConnectCells(currentCell, newCell);
                currentCell = newCell;
            }
        }
        GenerateDungeon();
    }

    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        // Check up neighbor
        if (cell - size.x >= 0 && !board[(cell - size.x)].visited)
        {
            neighbors.Add((cell - size.x));
        }

        // Check down neighbor
        if (cell + size.x < board.Count && !board[(cell + size.x)].visited)
        {
            neighbors.Add((cell + size.x));
        }

        // Check right neighbor
        if ((cell + 1) % size.x != 0 && !board[(cell + 1)].visited)
        {
            neighbors.Add((cell + 1));
        }

        // Check left neighbor
        if (cell % size.x != 0 && !board[(cell - 1)].visited)
        {
            neighbors.Add((cell - 1));
        }

        return neighbors;
    }

    List<int> CheckVisitedNeighbors(int cell)
    {
        List<int> visitedNeighbors = new List<int>();

        // Check up neighbor
        if (cell - size.x >= 0 && board[(cell - size.x)].visited)
        {
            visitedNeighbors.Add((cell - size.x));
        }

        // Check down neighbor
        if (cell + size.x < board.Count && board[(cell + size.x)].visited)
        {
            visitedNeighbors.Add((cell + size.x));
        }

        // Check right neighbor
        if ((cell + 1) % size.x != 0 && board[(cell + 1)].visited)
        {
            visitedNeighbors.Add((cell + 1));
        }

        // Check left neighbor
        if (cell % size.x != 0 && board[(cell - 1)].visited)
        {
            visitedNeighbors.Add((cell - 1));
        }

        return visitedNeighbors;
    }

    void ConnectCells(int currentCell, int newCell)
    {
        if (newCell > currentCell)
        {
            // Down or right
            if (newCell - 1 == currentCell)
            {
                board[currentCell].status[2] = true;
                board[newCell].status[3] = true;
            }
            else
            {
                board[currentCell].status[1] = true;
                board[newCell].status[0] = true;
            }
        }
        else
        {
            // Up or left
            if (newCell + 1 == currentCell)
            {
                board[currentCell].status[3] = true;
                board[newCell].status[2] = true;
            }
            else
            {
                board[currentCell].status[0] = true;
                board[newCell].status[1] = true;
            }
        }
    }
}