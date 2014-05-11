#region References
using UnityEngine;
using System.Collections;
#endregion

public class WormAI : MonoBehaviour
{
    #region Private Variables
    private float           _fireTimer;
    private bool            _canFire;
    private WormControl     _wormControl;
    #endregion

    #region Public Variables
    public float       fireInterval;    
    #endregion

    #region Constructor
    void Start () 
    {
        _fireTimer = Time.time + fireInterval;

        _canFire = true;

        _wormControl = transform.GetChild(0).GetComponent<WormControl>();
	}
    #endregion

    #region Loop
    void Update () 
    {
        if(Time.time > _fireTimer)
        {
            if(_canFire == true)
            {
                AdjustOrientation();

                _wormControl.PlayAnimation("Pullup");

                _canFire = false;
            }
        }
    }
    #endregion

    #region Private Methods
    private void AdjustOrientation()
    {
        Vector3 characterPosition = GameDirector.instance.character.position;
        characterPosition.z = transform.position.z;

        Vector3 targetVector =  characterPosition - transform.position;

        transform.rotation = Quaternion.FromToRotation(Vector3.up, targetVector);
    }
    #endregion

    #region Public Methods
    public void AnimationEnded()
    {
        _canFire = true;

        transform.rotation = Quaternion.identity;

        _fireTimer = Time.time + fireInterval;
    }
    #endregion
}
