using UnityEngine;

public class GridGeneratorScript : MonoBehaviour
{
    [SerializeField] GameObject plane;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int x = 0; x < 10; x++)
        {
            for(int z = 0; z < 10; z++)
            {
                Instantiate(plane,
                            new Vector3(x, 0, z),
                            Quaternion.identity);
            }
        }
    }


}
