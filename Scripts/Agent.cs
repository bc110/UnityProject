using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public float speed = 3;
    private Vector3[] _targetPath;
    private int _indexPath = 0;
    public float yPos = 0.5f;

    public GameObject player;


    private void Update()
    {
        SetNewTarget();
        if(_targetPath != null ) MoveToTarget();
    }

    void MoveToTarget()
    {
        if (_indexPath  <  _targetPath.Length)
        {
            transform.LookAt(_targetPath[_indexPath]);
            transform.position = Vector3.MoveTowards(transform.position, _targetPath[_indexPath], speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _targetPath[_indexPath]) < 0.05f)
            {
                _indexPath++;
            }
        }
    }

    void SetNewTarget()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x,yPos,player.transform.position.z);

        Vector3[] newPath = Astar_Manager.Singleton.FindingPath(transform.position,playerPos);

        if(newPath != null){
            _targetPath = newPath;
            _indexPath = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"){
            Vector3 vector = (player.transform.position-this.transform.position).normalized;
            vector = new Vector3(vector.x,0.0f,vector.z).normalized;
            Debug.Log(player.GetComponent<PlayerController>().speed);
            player.GetComponent<PlayerController>().rb.AddForce(vector*2000);
        }
    }
}
