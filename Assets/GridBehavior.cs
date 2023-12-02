using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class GridBehavior : MonoBehaviour
{
    public GameObject cellPrefab;
    public int rows;
    public int columns;
    public List<GameObject> cells = new List<GameObject>();
    public float scale = 10.0f;
    void Start()
    {
        PrepareGrid();
        ConfigureCells();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PrepareGrid()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                var newCell = Instantiate(cellPrefab, transform.position + new Vector3(1.2f * scale * row, 0, 1.2f * scale * column), Quaternion.identity);
                if (newCell == null) continue;
                var cb = newCell.GetComponent<CellBehavior>();
                cb.row = row;
                cb.column = column;
                cells.Add(newCell);
            }
        }
    }

    public GameObject GetCellAt(int row, int column)
    {
        if (row < 0 || row >= rows || column < 0 || column >= columns) return null;
        return cells[columns * row + column];
    }

    void ConfigureCells()
    {
        foreach(var cell in cells)
        {
            var cb = cell.GetComponent<CellBehavior>();
            cb.top = GetCellAt(cb.row - 1, cb.column);
            cb.bottom = GetCellAt(cb.row + 1, cb.column);
            cb.left = GetCellAt(cb.row, cb.column - 1);
            cb.right = GetCellAt(cb.row, cb.column + 1);
        }

        var starCell = cells[0];
        var startcb = starCell.GetComponent<CellBehavior>();
        startcb.leftWall.SetActive(false);
        var lastCell = cells.Last();
        var lastcb = lastCell.GetComponent<CellBehavior>();
        lastcb.rightWall.SetActive(false);
    }

    public GameObject RandomCell()
    {
        var rn = new Random();
        return GetCellAt(rn.Next(0, rows), rn.Next(0, columns));
    }
}
