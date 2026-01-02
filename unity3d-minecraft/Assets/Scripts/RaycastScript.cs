using UnityEngine;

public class RaycastScript : MonoBehaviour
{
    GameObject obj;
    GameObject prevObj;
    public GameObject cubePrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //ray.origin = transform.position;
        //ray.direction = transform.forward;
        Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            Debug.Log("hit");
            obj = hit.transform.gameObject;
            if (obj.CompareTag("Floor"))
            {
                if (prevObj == null)
                {
                    obj.GetComponent<MeshRenderer>().material.color = Color.green;
                    prevObj = obj;
                }
                else if (!prevObj.Equals(obj))
                {
                    prevObj.GetComponent<MeshRenderer>().material.color = Color.gray;
                    prevObj = null;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    LeftMouseButtonClick();
                }
            }
        }
    }
    void LeftMouseButtonClick()
    {
        Instantiate(cubePrefab,
        new Vector3(obj.transform.position.x,
                    obj.transform.position.y + 1,
                    obj.transform.position.z),
        Quaternion.identity);
    }

}
