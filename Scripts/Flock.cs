using UnityEngine;

public class Flock : MonoBehaviour {

    // Access the FlockManager script
    public FlockManager myManager;
    // Prefab initial speed;
    float speed;
    // Bool used to check the limits
    bool turning = false;
    private Rigidbody rb;

    public bool IsGrounded;

    void Start() {
        // Assign a random speed to each this prefab
        rb = GetComponent<Rigidbody>();
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
    }

    void OnCollisionStay (Collision collisionInfo)
    {
        IsGrounded = true;
    }
 
    void OnCollisionExit (Collision collisionInfo)
    {
        IsGrounded = false;
    }

    // Update is called once per frame
    void Update() {

        // Determine the bounding box of the manager cube
        Bounds b = new Bounds(myManager.transform.position, myManager.limits * 2.0f);

        Vector3 direction = Vector3.zero;

        if (!b.Contains(transform.position)) {
            turning = true;
            direction = myManager.transform.position - transform.position;
        }else {turning = false;}

        // Test if we're turning
        if (turning) {

            // Turn towards the centre
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(direction),
                                                  myManager.rotationSpeed * Time.deltaTime);
        } else {

            // 10% chance of altering prefab speed
            if (Random.Range(0.0f, 100.0f) < 10.0f) {
                speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
            }

            // 15% chance of applying the flocking rules
            if (Random.Range(0.0f, 100.0f) < 15.0f) {
                ApplyRules();
            }
        }

        float jumpSpeed = 0.0f;
        if (IsGrounded){
            // 10% chance of jumping
            if (Random.Range(0.0f, 100.0f) < 10.0f) {
                jumpSpeed = 2.0f;
            }
        }
        rb.AddForce(Quaternion.Euler(0,this.transform.eulerAngles.y,0)*new Vector3(0.0f, jumpSpeed, Time.deltaTime * speed*100));
    }

    void ApplyRules() {

        GameObject[] gos;
        gos = myManager.allFlock;

        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.01f;
        float nDistance;
        int groupSize = 0;

        foreach (GameObject go in gos) {

            if (go != this.gameObject) {

                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if (nDistance <= myManager.neighbourDistance) {
                    vcentre += go.transform.position;
                    groupSize++;
                    if (nDistance < 0.03f) {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        if (groupSize > 0) {

            // Find the average centre of the group then add a vector to the target (goalPos)
            vcentre = vcentre / groupSize + (myManager.goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            Vector3 direction = (vcentre + vavoid) - transform.position;
            if (direction != Vector3.zero) {

                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(direction),
                                                      myManager.rotationSpeed * Time.deltaTime);
            }
        }
    }
}
