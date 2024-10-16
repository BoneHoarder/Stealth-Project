using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{   
    private  int[,] cells;
    [SerializeField] private int size_x= 10;

    [SerializeField] private int size_y= 8;

    private List<Vector2Int> walls= new();

    [SerializeField] private GameObject wallPrefab;

    [SerializeField] private float gridSize= 2f;

    void Awake()
    {   
        //1.start with a grid full of walls
        cells= new int[size_x, size_y];

        for ( int i=0; i < size_x; i++){
            for (int j = 0; j < size_y; j++)
            {
                cells[i,j]=0;
            }
        }
    }

    void Start()
    {
        GenerateMaze();

        SpawnMaze();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegenerateMaze()
    {   
        //reset grid to 0
        for ( int i=0; i < size_x; i++){
            for (int j = 0; j < size_y; j++)
            {
                cells[i,j]=0;
            }
        }

        //destroy previous children of Maze Generator (empty object)
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        GenerateMaze();

        SpawnMaze();
    }

    int NumberOfVisitedNeighbours(Vector2Int pos)
    {
        int count=0;

        if ( pos.x < size_x-1 && cells[pos.x +1, pos.y] > 0)
        {
            count++;
        }

        if (pos.x > 0 && cells[pos.x -1 , pos.y] > 0)
        {
            count++;
        }

        if ((pos.y < size_y -1) && cells[pos.x, pos.y +1]>0)
        {
            count++;
        }

        if (pos.y > 0 && cells[pos.x, pos.y - 1]>0)
        {
            count++;
        }

        return count;
    }

    void AddNeighbouringWallsToList(Vector2Int pos)
    {
        if ( pos.x < size_x-1 && cells[pos.x +1, pos.y] <= 0)
        {
            Vector2Int newWall = new Vector2Int(pos.x +1, pos.y);

            if(!walls.Contains(newWall)){
                walls.Add(newWall);
            }
        }

        if (pos.x > 0 && cells[pos.x -1 , pos.y] <= 0)
        {
            Vector2Int newWall = new Vector2Int(pos.x -1 , pos.y);

            if(!walls.Contains(newWall)){
                walls.Add(newWall);
            }
        }

        if ((pos.y < size_y -1) && cells[pos.x, pos.y + 1] <= 0)
        {   
            Vector2Int newWall = new Vector2Int(pos.x, pos.y + 1);

            if(!walls.Contains(newWall)){
                walls.Add(newWall);
            }
        }

        if (pos.y > 0 && cells[pos.x, pos.y - 1] <= 0)
        {
            Vector2Int newWall = new Vector2Int(pos.x, pos.y - 1);

            if(!walls.Contains(newWall)){
                walls.Add(newWall);
            }
        }

    }

    void GenerateMaze()
    {
        //2a. Pick a cell and mark it as part of the maze

        cells[0,0]= 1;

        //2b. Add the walls of the cell to the wall list
        walls.Add(new Vector2Int (1,0));

        walls.Add(new Vector2Int (0,1));

        //3. While there are walls in the list

        while (walls.Count > 0)
        {
            //3.1a. Pick a random wall from the list
            int walls_index = Random.Range(0, walls.Count);

            Vector2Int wall= walls[walls_index];

            //3.1b. If only one of the cells that the walls divides is visited, then:

            if (NumberOfVisitedNeighbours(wall) == 1)
            {
                //3.1.1 make the wall a passage and mark the unvisited cells as part of the maze
                cells[wall.x, wall.y] = 1;

                //3.1.2 Add the neighbouring walls of the cell to the wall list
                AddNeighbouringWallsToList(wall);
            }

            //3.2 Remove wall from list
            walls.Remove(wall);

        }

    }

    void SpawnMaze()
    {
        for (int i = 0; i < size_x; i++)
        {
            for (int j = 0; j < size_y; j++)
            {
                if (cells[i,j] <= 0)
                {
                    Instantiate(wallPrefab, new Vector3(i* gridSize, 0, j*gridSize), Quaternion.identity, transform);
                }
            }
        }

        // borders up and down
        for (int i = -1; i < size_x +1; i++)
        {
            Instantiate(wallPrefab, new Vector3(i* gridSize, 0, -1 * gridSize), Quaternion.identity, transform);

            Instantiate(wallPrefab, new Vector3(i* gridSize, 0, size_y *gridSize), Quaternion.identity, transform);

        }

        //borders left and right
        for (int j = 0; j < size_y ; j++)
        {
            Instantiate(wallPrefab, new Vector3( -1 * gridSize, 0, j * gridSize), Quaternion.identity, transform);

            Instantiate(wallPrefab, new Vector3( size_x * gridSize, 0, j *gridSize), Quaternion.identity, transform);

        }
    }
}
