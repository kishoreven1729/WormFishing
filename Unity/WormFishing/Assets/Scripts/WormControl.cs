#region References
using UnityEngine;
using System.Collections;
#endregion

public class WormControl : MonoBehaviour
{
    #region Private Variables
    private Animator        _wormAnimator;
    private WormAI          _wormAI;
    #endregion

    #region Constructor
    void Start () 
    {
        _wormAnimator = GetComponent<Animator>();

        _wormAI = transform.parent.GetComponent<WormAI>();
	}
    #endregion

    #region Loop
    void Update () 
    {

    }
    #endregion

    #region Methods
    public void PlayAnimation(string triggerValue)
    {
        _wormAnimator.SetTrigger(triggerValue);
    }

    public void OnAnimationEnd()
    {
        _wormAI.AnimationEnded();
    }
    #endregion
}
