using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public bool isDestroyed = false;
    private string _name;
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }
    public int health { get; set; }
    [SerializeField]
    private BlockTypes blockType;
    private void Start()
    {
        health = (int)blockType;
    }

    public void DestroyBehaviour()
    {
        isDestroyed = true;

        GameObject miniBlock = Resources.Load<GameObject>(
        "Mini" + blockType.ToString()
    );

        if (miniBlock == null)
        {
            Debug.LogError(
                "Mini block prefab NOT FOUND: Mini" + blockType.ToString()
            );
            Destroy(gameObject);
            return;
        }
        Instantiate(miniBlock, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
