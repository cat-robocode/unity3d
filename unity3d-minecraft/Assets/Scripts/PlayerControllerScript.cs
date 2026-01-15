using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControllerScript : MonoBehaviour
{
    private const float gravityScale =9.8f;
    private const float speedScale = 5f;
    private const float jumpForce = 8f;
    private const float turnSpeed = 90f;
    
    private float verticalSpeed =0f;
    private float mouseX = 0f;
    private float mouseY = 0f;
    private float currentAngleX =0f;

    private CharacterController controller; 
    [SerializeField] private Camera goCamera;
    private GameObject lastRayHittedBlock;
    private Color lastRayHittedBlockColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        lastRayHittedBlock = null;
    }
    private void RotateCharacter()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(new Vector3(0f,mouseX*turnSpeed*Time.deltaTime, 0f));
        currentAngleX += mouseY * turnSpeed * Time.deltaTime * -1f;
        currentAngleX = Mathf.Clamp(currentAngleX, -89f, 89f);
        goCamera.transform.localEulerAngles = new Vector3(currentAngleX, 0f, 0f);
    }
    private void MoveCharacter()
    {
        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical"));
        velocity = transform.TransformDirection(velocity) * speedScale;
        if(controller.isGrounded)
        {
            verticalSpeed = 0f;
            if(Input.GetButton("Jump"))
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

            Debug.Log("hit");
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
    void Update()
    {
        RotateCharacter();
        MoveCharacter();
        CursorSelectedBlock();
    }
}
