using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBehavior : MonoBehaviour
{
    public enum DirectionType
    {
        LEFT,
        RIGHT,
        TOP,
        BOTTOM
    }
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject topWall;
    public GameObject bottomWall;
    public GameObject body;
    private bool _visited = false;
    public int row;
    public int column;
    public Dictionary<CellBehavior, bool> links = new Dictionary<CellBehavior, bool>();
    public GameObject top;
    public GameObject bottom;
    public GameObject left;
    public GameObject right;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Visit(DirectionType from)
    {
        _visited = true;
        body.SetActive(false);

        switch (from)
        {
            case DirectionType.TOP:
                topWall.SetActive(false);
                break;
            case DirectionType.BOTTOM:
                bottomWall.SetActive(false);
                break;
            case DirectionType.LEFT:
                leftWall.SetActive(false);
                break;
            case DirectionType.RIGHT:
                rightWall.SetActive(false);
                break;
        }
    }

    public void Link(CellBehavior cell, DirectionType from, bool bidi = true)
    {
        links[cell] = true;
        Visit(from);
        if (bidi)
        {
            cell.Link(this, GetOpposite(from), false);
        }
    }

    public void Unlink(CellBehavior cell, bool bidi = true)
    {
        links.Remove(cell);
        cell.Unlink(this);
    }

    public List<GameObject> Neighbors()
    {
        var list = new List<GameObject>();
        if (top != null) list.Add(top);
        if(bottom != null) list.Add(bottom);
        if (left != null) list.Add(left);
        if(right != null) list.Add(right);

        return list;
    }

    public float GetWidth()
    {
        return leftWall.transform.localScale.x + body.transform.localScale.x;
    }

    public DirectionType GetOpposite(DirectionType d)
    {
        switch (d)
        {
            case DirectionType.TOP:
                return DirectionType.BOTTOM;
            case DirectionType.BOTTOM:
                return DirectionType.TOP;
            case DirectionType.LEFT:
                return DirectionType.RIGHT;
            case DirectionType.RIGHT:
                return DirectionType.LEFT;
            default:
                return DirectionType.TOP;
        }
    }
}
