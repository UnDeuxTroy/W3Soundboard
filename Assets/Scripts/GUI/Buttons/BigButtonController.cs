using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigButtonController : MonoBehaviour {

    [SerializeField]
    Animator buttonAnimator;

    public void bigButtonClicked()
    {
        buttonAnimator.SetBool("isClicked", true);
    }

    public void bigButtonReleased()
    {
        buttonAnimator.SetBool("isClicked", false);
    }
}
