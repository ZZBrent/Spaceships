using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(showFor4Seconds());
	}
	
    private IEnumerator showFor4Seconds()
    {
        yield return new WaitForSeconds(4);
        this.gameObject.SetActive(false);
    }
}
