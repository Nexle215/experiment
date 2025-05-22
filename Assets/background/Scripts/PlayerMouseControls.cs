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

    public AudioSource gameBGM;
    public AudioSource endingBGM;
    
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
                    gameBGM.Stop();
                    
                    Camera.main.transform.position = new Vector3(-47.8f, 1f, -10);
                    endingBGM.Play();
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
                    Camera.main.transform.position = new Vector3(21.7f, -0.2f, -10);
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
                Camera.main.transform.position = new Vector3(42.9f, -0.4f, -10);
                break;

            case "gameover":
                SceneManager.LoadScene("scene2");
                break;
            
            case "ctrls button" :
                Camera.main.transform.position = new Vector3(-25f, 15.9f, -10);
                break;

            case "back ctrls" :
                Camera.main.transform.position = new Vector3(0.2f, 15.9f, -10);
                break;
            
            case "storybutton" :
                Camera.main.transform.position = new Vector3(-46.76f, 16f, -10);
                break;
            
            case "back story" :
                Camera.main.transform.position = new Vector3(0.2f, 15.9f, -10);
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