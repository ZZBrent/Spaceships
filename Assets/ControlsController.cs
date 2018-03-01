using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsController : MonoBehaviour {

    [SerializeField]
    private GameObject androidControls;
    [SerializeField]
    private GameObject otherControls;
    public static Button fireButton;
    public static Button leftButton;
    public static Button rightButton;

    // Use this for initialization
    void Start () {
		if(Application.platform == RuntimePlatform.Android)
        {
            if (otherControls != null)
            {
                otherControls.SetActive(false);
            }
        }
        else
        {
            androidControls.SetActive(false);
        }
	}
}
