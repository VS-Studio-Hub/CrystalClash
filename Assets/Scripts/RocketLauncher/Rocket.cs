using UnityEngine;

public class Rocket : MonoBehaviour
{
    public Transform Target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Target.position);

        transform.position = Vector3.MoveTowards(transform.position, Target.position, 50 * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(this.gameObject);
    }
}
