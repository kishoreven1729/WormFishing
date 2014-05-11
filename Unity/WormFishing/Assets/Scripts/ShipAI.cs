using UnityEngine;
using System.Collections;

public class ShipAI : MonoBehaviour
{
    #region Private Variables
    private Vector3         _target;
    
    private float           _switchTargetTimer;

    private bool            _isWormApproaching;
    private float           _limitsThreshold;    
    #endregion

    #region Public Variables
    public float            switchTargetInterval;    
    public float            forceMagnitude;
    public float            forceFluctuation;

    public Transform        leftLimit;
    public Transform        rightLimit;
    #endregion

    #region Constructor
    void Start () 
    {
        _switchTargetTimer = Time.time + switchTargetInterval;

        _isWormApproaching = false;

        _limitsThreshold = 0.5f;   
	}
    #endregion

    #region Loop
    void Update () 
    {
        if(Time.time > _switchTargetTimer)
        {
            if(_isWormApproaching == false)
            {
                Vector3 force3D = Vector3.zero;

                Vector2 force = GetShootForce();

                force3D.x = force.x;
                force3D.y = force.y;
                                
                if(force3D.x != 0.0f)
                {   
                    rigidbody.AddForce(force3D);
                }

                _switchTargetTimer = Time.time + switchTargetInterval;
            }
        }
    }    
    #endregion

    #region Private Methods
    /// <summary>
    /// Generates a new direction for the ship to catch the worm
    /// </summary>
    /// <returns>The new direction for the ship as Vector2</returns>
    private Vector2 GetShootForce()
    {
        Vector2 force               = Vector2.zero;
        
        float randomValue           = Random.Range(0.0f, 1.0f);
        int additive                = Random.Range(-1, 2);

        float newForceMagnitude     = forceMagnitude;
        float newFluctuation        = 0.0f;

        RandomModifier(ref randomValue);

        if(randomValue < 0.4f)
        {
            #region Shoot Left
            float ratioAffected = randomValue / 0.4f;

            newFluctuation = ratioAffected * forceFluctuation * additive;

            force = new Vector2(-1.0f, 0.0f);
            #endregion
        }
        else if(randomValue < 0.8f)
        {
            #region Shoot Right
            float ratioAffected = (randomValue - 0.4f) / 0.4f;

            newFluctuation = ratioAffected * forceFluctuation * additive;

            force = new Vector2(1.0f, 0.0f);
            #endregion
        }
        else
        {
            #region Don't Change Direction
            #endregion
        }

        newForceMagnitude += newFluctuation;

        force = force * newForceMagnitude;

        return force;
    }

    /// <summary>
    /// Adjusts the random value when the ship has sailed to the edges
    /// </summary>
    /// <param name="randomValue">The original random value passed as reference</param>
    private void RandomModifier(ref float randomValue)
    {
        float distanceFromLeft = Vector2.Distance(leftLimit.position, transform.position);
        float distanceFromRight = Vector2.Distance(rightLimit.position, transform.position);

        if (distanceFromRight < _limitsThreshold && randomValue > 0.4f)            //Check if it is still being sent right and modify to send left
        {
            randomValue -= 0.4f;
        }

        if (distanceFromLeft < _limitsThreshold && randomValue < 0.4f)       //Check if it is still being sent left and modify to send right
        {
            randomValue += 0.4f;
        }        
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Should be called when the worm is jumping out of the dune to capture the human
    /// </summary>
    public void WormAproaching()
    {
        if(_isWormApproaching == false)
        {
            _isWormApproaching = true;
        }
    }

    /// <summary>
    /// Should be called when the worm misses the human and head back into the hole it came from
    /// </summary>
    public void WormBackInDune()
    {
        if(_isWormApproaching == true)
        {
            _isWormApproaching = false;

            _switchTargetTimer = Time.time;
        }
    }
    #endregion
}
