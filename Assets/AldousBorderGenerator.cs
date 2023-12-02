using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class AldousBorderGenerator : MonoBehaviour
{
    public GameObject grid;
    IEnumerator Start()
    {
        yield return StartCoroutine(Wait());
        yield return Generate();
    }
    void Update()
    {
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
    }

    CellBehavior.DirectionType GetDirection(CellBehavior from, CellBehavior to)
    {
        switch (from.row - to.row)
        {
            case 1:
                return CellBehavior.DirectionType.TOP;
            case -1:
                return CellBehavior.DirectionType.BOTTOM;
        }

        switch (from.column - to.column)
        {
            case 1:
                return CellBehavior.DirectionType.LEFT;
            case -1:
                return CellBehavior.DirectionType.RIGHT;
        }

        return CellBehavior.DirectionType.TOP;
    }

    IEnumerator Generate()
    {
        var rnd = new Random();
        var gb = grid.GetComponent<GridBehavior>();

        var cell = gb.RandomCell();
        var unvisited = gb.rows * gb.columns - 1;

        while (unvisited > 0)
        {
            var cb = cell.GetComponent<CellBehavior>();
            var neighbors = cb.Neighbors();
            var neighbor = neighbors[rnd.Next(neighbors.Count)];
            var ncb = neighbor.GetComponent<CellBehavior>();

            if (ncb.links.Count == 0)
            {
                cb.Link(ncb, GetDirection(cb, ncb));
                unvisited--;
            }

            cell = neighbor;

            yield return new WaitForSeconds(0.002f);
        }
    }
}
