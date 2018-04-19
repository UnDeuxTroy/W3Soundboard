using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPanelController : MonoBehaviour {

    [SerializeField]
    Animator leftMenuAnimator;
    [SerializeField]
    GameObject musicCheckArrow;

	// Use this for initialization
	void Start () {
	}

    public void TogglePanel(bool display)
    {
        leftMenuAnimator.SetBool("isDisplayed", display);
    }

    public void ToggleMusicCheckArrow()
    {
        musicCheckArrow.SetActive(!musicCheckArrow.activeSelf);
    }
}
