using UnityEngine;

public class FlockManager : MonoBehaviour {

    // Access the flock prefab
    public GameObject prefab;
    // Flock start position
    public GameObject start;
    // Starting number of flock
    public int numFlock = 20;
    // Array of flock prefabs
    public GameObject[] allFlock;
    // Bounds for flock
    public Vector3 limits = new Vector3(5.0f, 5.0f, 5.0f);
    // Goal position
    public Vector3 goalPos;

    // Header title for Unity Editor
    [Header("Flock Settings")]
    [Range(0.0f, 5.0f)]
    public float minSpeed;          // Minimum speed range
    [Range(0.0f, 5.0f)]
    public float maxSpeed;          // Maximum speed range
    [Range(0.05f, 10.0f)]
    public float neighbourDistance; // Prefab neighbout distance
    [Range(0.0f, 5.0f)]
    public float rotationSpeed;     // Speed at which the prefabs rotate

    void Start() {
        allFlock = new GameObject[numFlock];
        for (int i = 0; i < numFlock; ++i) {
            Vector3 pos = start.transform.position + new Vector3(Random.Range(-limits.x, limits.x),0,Random.Range(-limits.x, limits.x));

            allFlock[i] = (GameObject)Instantiate(prefab, pos, Quaternion.identity);
            allFlock[i].GetComponent<Flock>().myManager = this;
        }
        goalPos = this.transform.position;
    }

    void Update() {
            goalPos = this.transform.position;
    }
}
