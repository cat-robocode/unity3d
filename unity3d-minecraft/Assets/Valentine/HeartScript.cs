using UnityEngine;
using UnityEngine.UI;


public class HeartScript : MonoBehaviour
{
    public Text text;
    void Start()
    {
        text = GameObject.Find("Text").GetComponent<Text>();
    } 
    public void Launch(Vector3 velocity)
    {
        GetComponent<Rigidbody>().linearVelocity = velocity;
    }

    private void OnMouseDown()
    {

        HandleHit();
    }

    void HandleHit()
    {
        Debug.Log(gameObject.name + " was hit!");
        if (gameObject.tag == "valentine-good-heart")
        {
            int textNumber = int.Parse(text.text);
            textNumber ++;
            text.text = textNumber.ToString();
        }
        else if (gameObject.tag == "valentine-bad-heart")
        {
            Time.timeScale = 0f;
        }
        Destroy(gameObject);
    }
}
