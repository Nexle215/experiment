using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMouseControls : MonoBehaviour
{
    private Vector2 mousePos;
    private AudioSource audioSource;
    private bool hasScrewdriver = false;
    private bool hasKey = false;
    private bool clickedbowl = true;
    private GameObject Player;
    public TMP_Text Health;

    public AudioClip clickClip;

    void Start()
    {
        Player = GameObject.FindWithTag("player");
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPoint(InputValue inputValue)
    {
        mousePos = inputValue.Get<Vector2>();
    }

    public int points = 3;



     void Damage(int value)
    {
        points = points - value;
        Health.text = points.ToString();
        if (points == 0) //< 1
        {
            Camera.main.transform.position = new Vector3(0.08f, -16f, -10);
        }
    }


    public void OnClick(InputValue inputValue)
    {
        // This method doesn't work if the player clicked off the object
        if (inputValue.isPressed == false)
        {
            return;
        }
        
        // convert screen position in unity coordinates
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        Collider2D result = Physics2D.OverlapPoint(worldPosition);



        // no object was clicked
        if (result == null)
        {
            // stop the code here
            return;
        }

        switch (result.tag)
        {
            case "key":
                Debug.Log("Clicked key");
                hasKey = true;
                Destroy(result.gameObject);
                break;

            case "frontdoor":
                if (hasKey == false)
                {
                    Debug.Log("I need a key");
                }
                else
                {
                    Debug.Log("I can open the door");
                    Camera.main.transform.position = new Vector3(0.14f, 15.91f, -10);

                }
                break;
            
            case "Screwdriver":
                Debug.Log("Clicked on Screwdriver");
                hasScrewdriver = true;
                Destroy(result.gameObject);
                break;

            case "Vent":
                if (hasScrewdriver == false)
                {
                    Debug.Log("I need a screwdriver");
                    //move camera to the close vent view

                }
                else
                {
                    Debug.Log("I have a screwdriver yippieee");
                    //move camera to the open vent view
                    Camera.main.transform.position = new Vector3(22.56f, 0, -10);
                }

                break;

            case "livingroom door":
                Camera.main.transform.position = new Vector3(0.1f, 0.1f, -10);
                break;

            case "back button yes":
                Camera.main.transform.position = new Vector3(0, 0, -10);
                break;

            case "kitchen door":
                Camera.main.transform.position = new Vector3(-23.6f, 0.1397334f, -10);
                break;

            case "play button":
                Camera.main.transform.position = new Vector3(-23.6f, 0.1397334f, -10);
                break;

            case "openvent":
                Camera.main.transform.position = new Vector3(40.04f, -0.36f, -10);
                break;

            case "gameover":
                SceneManager.LoadScene("scene2");
                break;

            case "bowl":
                Debug.Log("bowl");
                clickedbowl = true;
                Damage(1);
                break;
        }

        audioSource.PlayOneShot(clickClip);
    }

}