using UnityEngine;
using UnityEngine.UI;

public class ValentineScript : MonoBehaviour
{
    public GameObject[] prefabs; 
    public float launchForce = 12f;
    public float spawnInterval = 1.5f;
    


    void Start()
    {
        
        InvokeRepeating(nameof(SpawnFruit), 1f, spawnInterval);
    }

    void SpawnFruit()
    {
 
        GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Length)];
        GameObject instance = Instantiate(prefabToSpawn, transform.position, prefabToSpawn.transform.rotation);


        float randomZAngle = Random.Range(-15f, 15f);
        Vector3 launchDirection = Quaternion.Euler(0, 0, randomZAngle) * Vector3.up;


        instance.GetComponent<HeartScript>().Launch(launchDirection * launchForce);
    }
}
