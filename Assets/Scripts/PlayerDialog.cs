using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script is placed on the bubble button of Player
// only function is keep the bubble following the player
public class PlayerDialog : MonoBehaviour
{
    public Vector3 offset;  // adjust the relative position of the bubble to the Player
    
    private GameObject player;
    
    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("playerbedroom");
    }
	
	// Update is called once per frame
	void Update () {
	    // calculate the position of the bubble should be,
	    // convert the world position to ScreenPoint
        transform.position = Camera.main.WorldToScreenPoint(player.transform.position + offset);    
    }
}
