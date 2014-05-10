using UnityEngine;
using System.Collections;

public class ShipAI : MonoBehaviour
{
    #region Private Variables
    private Vector3         _target;

    private float           _movementSpeed;
    private float           _switchTargetTimer;
    #endregion

    #region Public Variables
    public float            switchTargetInterval;

    private Vector3 _leftBoundary;
    private Vector3 _rightBoundary;
    #endregion

    #region Constructor
    void Start () 
    {
	    
	}
    #endregion

    #region Loop
    void Update () 
    {

    }
    #endregion

    #region Methods
    #endregion
}
