using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BinaryTreeGenerator : MonoBehaviour
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

    IEnumerator Generate()
    {
        var rnd = new Random();
        var gb = grid.GetComponent<GridBehavior>();
        Debug.Log(gb.cells.Count);
        foreach (var cell in gb.cells)
        {
            var cb = cell.GetComponent<CellBehavior>();
            var neighbors = new List<Tuple<GameObject, CellBehavior.DirectionType>>();
            if(cb.top != null) neighbors.Add(Tuple.Create(cb.top, CellBehavior.DirectionType.TOP));
            if(cb.right != null) neighbors.Add(Tuple.Create(cb.right, CellBehavior.DirectionType.RIGHT));
            
            if(neighbors.Count == 0) continue;
            
            var neighbor = neighbors[rnd.Next(neighbors.Count)];
            
            cb.Link(neighbor.Item1.GetComponent<CellBehavior>(), neighbor.Item2);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
