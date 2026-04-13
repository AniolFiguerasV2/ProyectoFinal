using UnityEngine;

public class HoverSound : MonoBehaviour
{
    public AudioSource hoversound;
    public void MouseSound(){
        hoversound.PlayOneShot(hoversound.clip);
    }
    
}
