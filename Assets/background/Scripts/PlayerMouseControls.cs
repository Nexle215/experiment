using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMouseControls : MonoBehaviour
{
    private Vector2 mousePos;
    
    private bool hasScrewdriver = false;
    private bool clickedbowl = true;
    private GameObject Player;

    
    void Start ()
    {
        Player = GameObject.FindWithTag("player");
    }
    
    public void OnPoint(InputValue inputValue)
    {
        mousePos = inputValue.Get<Vector2>();
    }

    public int points = 3;
    


        void Damage(int value)
        {
            points = points - value;
            if (points < 1)
            {
                Destroy(Player);
            }
        }


    public void OnClick(InputValue inputValue)
    {
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

            default:
                Debug.Log("I don't know what this is");
                break;


        }


        switch (result.tag)
        {
            case "livingroom door":
                Camera.main.transform.position = new Vector3(0.1f, 0.1f, -10);
                break;

        }

        switch (result.tag)
        {
            case "back button yes":
                Camera.main.transform.position = new Vector3(0, 0, -10);
                break;
        }

        switch (result.tag)
        {
            case "kitchen door":
                Camera.main.transform.position = new Vector3(-23.6f, 0, -10);
                break;
        }
        
        switch (result.tag)
        {
        case "bowl":
        
            Debug.Log("bowl");
            clickedbowl = true;
            Damage(1);
        break;
        }
    

}
    
}