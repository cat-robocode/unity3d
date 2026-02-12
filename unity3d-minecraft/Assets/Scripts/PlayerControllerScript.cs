using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControllerScript : MonoBehaviour
{
    private const float gravityScale = 9.8f;
    private const float speedScale = 5f;
    private const float jumpForce = 8f;
    private const float turnSpeed = 90f;

    private float verticalSpeed = 0f;
    private float mouseX = 0f;
    private float mouseY = 0f;
    private float currentAngleX = 0f;

    private CharacterController controller;
    [SerializeField] private Camera goCamera;
    private GameObject lastRayHittedBlock;
    private Color lastRayHittedBlockColor;
    [SerializeField]
    GameObject particleObject, tool;
    private const float hitScaleSpeed = 15f;
    private float hitLastTime = 0f;
    public bool canMove = true;
    private InventoryManagerScript inventoryManager;
    public List<ItemData> inventoryItems, currentChestItems;
    private Transform itemParent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;

        controller = GetComponent<CharacterController>();

        inventoryManager =
            GameObject.Find("InventoryManager").GetComponent<InventoryManagerScript>();

        itemParent = GameObject.Find("InventoryContent").transform;

        inventoryManager.CreateItem(0, inventoryItems);
    }
    private void RotateCharacter()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(new Vector3(0f, mouseX * turnSpeed * Time.deltaTime, 0f));
        currentAngleX += mouseY * turnSpeed * Time.deltaTime * -1f;
        currentAngleX = Mathf.Clamp(currentAngleX, -89f, 89f);
        goCamera.transform.localEulerAngles = new Vector3(currentAngleX, 0f, 0f);
    }
    private void MoveCharacter()
    {
        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        velocity = transform.TransformDirection(velocity) * speedScale;
        if (controller.isGrounded)
        {
            verticalSpeed = 0f;
            if (Input.GetButton("Jump"))
            {
                verticalSpeed = jumpForce;
            }
        }
        verticalSpeed -= gravityScale * Time.deltaTime;
        velocity.y = verticalSpeed;
        controller.Move(velocity * Time.deltaTime);

    }
    private void CursorSelectedBlock()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hittedObj = hit.transform.gameObject;
            if (hittedObj.CompareTag("Block"))
            {
                if (lastRayHittedBlock == null)
                {
                    lastRayHittedBlockColor = hittedObj.GetComponent<MeshRenderer>().material.color;
                    hittedObj.GetComponent<MeshRenderer>().material.color = Color.red;
                    lastRayHittedBlock = hittedObj;
                }
                else if (!lastRayHittedBlock.Equals(hittedObj))
                {
                    lastRayHittedBlock.GetComponent<MeshRenderer>().material.color = lastRayHittedBlockColor;
                    lastRayHittedBlock = null;
                }
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (canMove)
        {
            RotateCharacter();
            MoveCharacter();
            CursorSelectedBlock();
            RaycastHit hit;
            if (Physics.Raycast(goCamera.transform.position,
                                goCamera.transform.forward, out hit, 5f))
            {

                if (Input.GetMouseButton(0))
                {
                    ObjectInteraction(hit.transform.gameObject);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && !inventoryManager.inventoryPanel.activeSelf)
        {

            OpenInventory();
        }

        else if (Input.GetKeyDown(KeyCode.Q))
        {
            CloseInventoryPanels();
        }
    }
    private void Dig(BlockScript block)
    {
        if (block == null || block.isDestroyed)
            return;


        if (Time.time - hitLastTime > 1f / hitScaleSpeed)
        {
            tool.GetComponent<Animator>().SetTrigger("attack");

            GameObject go = Instantiate(
                particleObject,
                block.transform.position,
                Quaternion.identity
            );

            go.GetComponent<ParticleSystemRenderer>().material =
                block.GetComponent<MeshRenderer>().material;

            hitLastTime = Time.time;
            block.health -= tool.GetComponent<ToolScript>().damageToBlock;

            if (block.health <= 0)
            {
                block.DestroyBehaviour();
            }
        }
    }

    private void ObjectInteraction(GameObject tempObject)
    {
        switch (tempObject.tag)
        {
            case "Block":
                Dig(tempObject.GetComponent<BlockScript>());
                break;
            case "Enemy":
                break;
            case "Chest":
                currentChestItems =
                    tempObject.GetComponent<ChestScript>().chestItems;
                OpenChest();
                break;
        }

    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.StartsWith("mini"))
        {
            inventoryManager.CreateItem(2, inventoryItems);
            Destroy(col.gameObject);
        }
    }
    private void OpenInventory()
    {
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        canMove = false;

        inventoryManager.inventoryPanel.SetActive(true);
        if (inventoryItems.Count > 0)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                inventoryManager.InstantiatingItem(inventoryItems[i],
                                                  itemParent,
                                                  inventoryManager.inventorySlots);
            }
        }
    }
    private void OpenChest()
    {
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        canMove = false;

        if (!inventoryManager.chestPanel.activeSelf)
        {
            inventoryManager.chestPanel.SetActive(true);
            Transform itemParent = GameObject.Find("ChestContent").transform;
            for (int i = 0; i < currentChestItems.Count; i++)
            {
                inventoryManager.InstantiatingItem(currentChestItems[i],
                                                  itemParent,
                                                  inventoryManager.currentChestSlots);
            }
        }
    }
    private void CloseInventoryPanels()
    {
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        canMove = true;

        foreach (GameObject slot in inventoryManager.currentChestSlots)
        {
            Destroy(slot);
        }

        foreach (GameObject slot in inventoryManager.inventorySlots)
        {
            Destroy(slot);
        }

        inventoryManager.currentChestSlots.Clear();
        inventoryManager.inventorySlots.Clear();
        inventoryManager.inventoryPanel.SetActive(false);
        inventoryManager.chestPanel.SetActive(false);
    }


}
