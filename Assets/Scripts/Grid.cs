using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] int lines = 10;
    [SerializeField] int columns = 20;
    [SerializeField] GameObject cellPrefab;

    GameObject[,] cells;

    // Start is called before the first frame update
    void Start()
    {
        cells = new GameObject[lines,columns];

        for (int i = 0; i < lines; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                Vector3 position = new Vector3(j + transform.position.x, i + transform.position.y, 0);
                
                cells[i,j] = Instantiate(cellPrefab, position, Quaternion.identity, transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool IsValidCell(int i, int j)
    {
        return i > 0 && j > 0 && i < lines && j < columns;
    }

    bool IsAlive(int i, int j)
    {
        if (!IsValidCell(i, j)) return false;

        return cells[i, j].GetComponent<Cell>().isAlive;
    }

    int CountAliveNeighbors(int i, int j)
    {
        int aliveNeighbors = 0;

        if (IsAlive(i - 1,j - 1))
        {
            aliveNeighbors++;
        }

        if (IsAlive(i - 1, j))
        {
            aliveNeighbors++;
        }

        if (IsAlive(i - 1, j + 1))
        {
            aliveNeighbors++;
        }

        if (IsAlive(i, j + 1))
        {
            aliveNeighbors++;
        }

        if (IsAlive(i, j - 1))
        {
            aliveNeighbors++;
        }

        if (IsAlive(i + 1, j - 1))
        {
            aliveNeighbors++;
        }

        if (IsAlive(i + 1, j))
        {
            aliveNeighbors++;
        }

        if (IsAlive(i + 1, j + 1))
        {
            aliveNeighbors++;
        }
        
        return aliveNeighbors;
    }

    public void CountAliveNeighborsOnGrid()
    {
        for (int i = 0; i < lines; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                cells[i, j].GetComponent<Cell>().aliveNeighbors = CountAliveNeighbors(i, j);
            }
        }
    }

    public void ApplyRules()
    {
        for (int i = 0; i < lines; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Cell cell = cells[i, j].GetComponent<Cell>();

                if(cell.isAlive && (cell.aliveNeighbors < 2 || cell.aliveNeighbors > 3))
                {
                    cell.isAlive = false;
                }

                if(!cell.isAlive && cell.aliveNeighbors == 3)
                {
                    cell.isAlive = true;
                }
            }
        }
    }
}
