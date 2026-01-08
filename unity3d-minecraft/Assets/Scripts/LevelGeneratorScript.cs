using NUnit.Framework.Internal;
using UnityEngine;

public class LevelGeneratorScript : MonoBehaviour
{
    private const int PYRAMID_COUNT = 5;
    private const int PYRAMID_HEIGHT = 6;
    private const int PYRAMID_BASE = PYRAMID_HEIGHT * 2 - 1;

    [SerializeField] private GameObject cubePrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int x = 0; x < PYRAMID_COUNT; x++)
        {
            for (int z = 0; z < PYRAMID_COUNT; z++)
            {
                CreatePyramid(new Vector3(x * PYRAMID_BASE, 0f, z * PYRAMID_BASE));
            }
        }

    }
    void CreateCube()
    {
        for (int x = 0; x < 15; x++)
            for (int z = 0; z < 15; z++)
                for (int y = 0; y < 15; y++)
                {
                    Instantiate(cubePrefab, new Vector3(x + 0.5f, y + 0.5f, z + 0.5f), Quaternion.identity);
                }
    }
    void CreatePyramid(Vector3 pos)
    {
        int offsetX = 0, offsetZ = 0;
        for (int y = 0; y < PYRAMID_HEIGHT; y++)
        {
            //here
            for (int x = (int)pos.x + offsetX; x < pos.x + PYRAMID_BASE - offsetX; x++)
            {
                for (int z = (int)pos.z + offsetZ; z < pos.z + PYRAMID_BASE - offsetZ; z++)
                {
                    GameObject tempObj = Instantiate(cubePrefab, new Vector3(x + 0.5f, y + 0.5f, z + 0.5f), Quaternion.identity);
                    if (y % 2 == 0)
                    {
                        tempObj.GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 0.5f);
                    }
                    else
                    {
                        tempObj.GetComponent<MeshRenderer>().material.color = new Color(0f, 0.5f, 0f);
                    }
                }
            }

            //here
            offsetX++;
            offsetZ++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
