using UnityEngine;
using System.Collections;

public class ButtonClickingSound : MonoBehaviour
{
    private AudioSource _clickSound;

    private void Awake()
    {
        _clickSound = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        StartCoroutine(PressForSeconds(0.5f));
    }

    IEnumerator PressForSeconds(float seconds)
    {
        _clickSound.Play();
        
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(seconds);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
}
