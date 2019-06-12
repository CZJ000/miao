using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistantCtl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Animation animation = GetComponent<Animation>();
        animation.CrossFade("idle", 0.1f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
