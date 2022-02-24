using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar_Manager : MonoBehaviour
{
    private PointGrid _pointGrid;
    public static Astar_Manager Singleton;

    private void Awake()
    {
        _pointGrid = GetComponent<PointGrid>();
        Singleton = this;
    }

    public Vector3[] FindingPath(Vector3 startNode,Vector3 endNode)
    {
        List<PointNode> openList = new List<PointNode>();
        List<PointNode> closeList = new List<PointNode>();
        
        // Adds node to grid
        PointNode startingNode = _pointGrid.addPosition(startNode);
        PointNode targetNode = _pointGrid.addPosition(endNode);

        // Return Null if target not found
        if (targetNode==null)return null;

        // Return Null if no path exsists
        if(targetNode.neighbors.Capacity == 0)return null;

        openList.Add(startingNode);
        
        
        while (openList.Count > 0)
        {
            
            PointNode currentNode = openList[0];
            openList.RemoveAt(0);
            openList.Sort();

            //If no path found
            if (currentNode == null)return null;

            //Check If Reach The Goal 
            if (_pointGrid.CheckIfPointsLinkedTogether(currentNode, targetNode))
            {
                targetNode.parent = currentNode;
                break;
            }
            closeList.Add(currentNode);

            List<PointNode> neighboursNode = currentNode.neighbors;
          
            for (int i=0; i < neighboursNode.Count ; i++)
            {
                if (!closeList.Contains(neighboursNode[i]))
                {
                    float gCost = currentNode.gCost + Vector3.Distance(currentNode.position, neighboursNode[i].position);
                    float hCost = Vector3.Distance(neighboursNode[i].position, targetNode.position);                   

                    if (openList.Contains(neighboursNode[i]) == false)
                    {
                        //Update best score and parent
                        neighboursNode[i].fCost = hCost + gCost;
                        neighboursNode[i].hCost = hCost;
                        neighboursNode[i].gCost = gCost;
                        neighboursNode[i].parent = currentNode;
                        openList.Add(neighboursNode[i]);
                    }
                    else
                    {
                        float new_fCost = gCost+hCost;

                        //Check if new score is better then the old one
                        if (neighboursNode[i].fCost > new_fCost)
                        {
                            //Update best score and parent
                            neighboursNode[i].fCost = new_fCost;
                            neighboursNode[i].hCost = hCost;
                            neighboursNode[i].gCost = gCost;
                            neighboursNode[i].parent = currentNode;
                        }
                    }
                }
            }
        }
        
        //Generate and returns path
        List<Vector3> pathNode = new List<Vector3>();
        PointNode currentPathNode = targetNode;
       
        while (currentPathNode != startingNode)
        {
            pathNode.Add(currentPathNode.position);
            currentPathNode = currentPathNode.parent;
        }

        pathNode.Reverse();
        return pathNode.ToArray();
    }
}