using UnityEngine;

public class ToolScript : MonoBehaviour
{
    [SerializeField] private ToolTypes type;
    [SerializeField] private ToolMaterials material;
    public int damageToEnemy;
    public int damageToBlock;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        damageToEnemy = (int)type * (int)material;
        switch (type)
        {
            case ToolTypes.PICKAXE:
                damageToBlock = 4 * (int)material;
                break;

            case ToolTypes.SWORD:
                damageToBlock = 1 * (int)material;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
