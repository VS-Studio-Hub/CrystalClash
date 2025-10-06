using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject rocket;
    public Transform car;

    public float distance;
    bool canShoot;

    private void Update()
    {
        rocket.transform.LookAt(car.position);

        distance = Vector3.Distance(this.transform.position, car.position);

        if (distance <= 40)
        {
            if (canShoot == false)
            {
                InvokeRepeating("Shoot", 0, 1);
                canShoot = true;
            }
        }
        else
        {
            if (canShoot == true)
            {
                CancelInvoke("Shoot");
                canShoot = false;
            }
        }
    }

    public GameObject rocketPrefab;
    public Transform rocketPlace;
    public void Shoot()
    {
        GameObject R = Instantiate(rocketPrefab, rocketPlace.position, Quaternion.identity);
        R.GetComponent<Rocket>().Target = car;
    }
}
