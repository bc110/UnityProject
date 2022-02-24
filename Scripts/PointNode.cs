using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointNode : IComparable<PointNode>
{
  
    public Vector3 position;
    public PointNode parent;
    public float gCost, hCost,fCost;
    public List<PointNode> neighbors;

    public PointNode(Vector3 positions)
    {
        this.position = positions;
        neighbors = new List<PointNode>();

        gCost = 0;
        hCost = 0;
        fCost = 0;
    }

    public void AddNeighbors(List<PointNode> neighbors)
    {
        this.neighbors = neighbors;
    }

    public int CompareTo(PointNode nodeToCompare)
    {
        int cost = fCost.CompareTo(nodeToCompare.fCost);
        if (cost == 0){
            cost = hCost.CompareTo(nodeToCompare.hCost);
        }
        return cost;
    }

}
