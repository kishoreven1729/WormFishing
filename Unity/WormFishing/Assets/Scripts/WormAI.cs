#region References
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#endregion

public class WormAI : MonoBehaviour
{
    #region Enum
    public enum ShootEvent
    {
        NotShot,
        Shot,
        Animating,
        Returning
    }
    #endregion

    #region Private Variables
    private float           _fireTimer;
    private bool            _canFire;
    private Animator        _wormControl;
    private List<Vector3>   _spawnLocations;
    private int             _spawnCount;

    private float           _reachedTreshold;
    private float           _animationThreshold;
    private ShootEvent      _shootEvent;

    private Vector3         _nextHeadPosition;
    private Quaternion      _nextHeadRotation;

    private Vector3         _originatingHeadPosition;
    private Quaternion      _originatingHeadRotation;

    private Vector3         _wormSpeeds;                //x - Head motion, Y - Worm motion, Z - Head rotation
    #endregion

    #region Public Variables
    public float            fireInterval; 
    public Transform        wormBase;
    public Transform        wormHead;
    public ConstantForce    wormPivot;
    
    public float            wormTargetY;

    public float            timeToCatch;    

    public float            constantForce;
    #endregion

    #region Constructor
    void Start () 
    {
        _fireTimer = Time.time + fireInterval;

        _canFire = true;
        
        _spawnLocations = new List<Vector3>();

        Transform spawnLocationsTransform = GameObject.FindGameObjectWithTag("WormSpawn").transform;

        _spawnCount = spawnLocationsTransform.childCount;

        foreach(Transform spawnLocation in spawnLocationsTransform)
        {
            _spawnLocations.Add(spawnLocation.position);
        }

        _shootEvent = ShootEvent.NotShot;
        _reachedTreshold = 0.2f;
        _animationThreshold = _reachedTreshold * 4.0f;

        _originatingHeadPosition = Vector3.zero;
        _originatingHeadRotation = Quaternion.identity;

        _nextHeadPosition = Vector3.zero;
        _nextHeadRotation = Quaternion.identity; 
	}
    #endregion

    #region Loop
    void Update () 
    {
        switch (_shootEvent)
        {
            case ShootEvent.NotShot:
                {
                    if (Time.time > _fireTimer)
                    {
                        if (_canFire == true)
                        {
                            transform.position = GetNextSpawnLocation();

                            ShootWorm();                            

                            _canFire = false;

                            GameDirector.instance.HaltShipAnchor();

                            GameDirector.instance.EnablePlayerControl();
                        }
                    }
                    break;
                }
            case ShootEvent.Shot:
                {
                    float distanceToTarget = Vector3.Distance(_nextHeadPosition, wormHead.position);

                    if (distanceToTarget < _animationThreshold)
                    {
                        //_wormControl.SetTrigger("Catch");
                    }

                    if (distanceToTarget < _reachedTreshold)
                    {
                        //_shootEvent = ShootEvent.Animating;
                        _shootEvent = ShootEvent.Returning;
                    }
                    else
                    {
                        wormHead.position = Vector3.Lerp(wormHead.position, _nextHeadPosition, Time.deltaTime * _wormSpeeds.x);
                        wormHead.rotation = Quaternion.Slerp(wormHead.rotation, _nextHeadRotation, Time.deltaTime * _wormSpeeds.z);
                    }

                    break;
                }
            case ShootEvent.Animating:
                {


                    break;
                }
            case ShootEvent.Returning:
                {
                    float distanceToTarget = Vector3.Distance(_originatingHeadPosition, wormHead.position);

                    if (distanceToTarget < _reachedTreshold)
                    {
                        wormPivot.force = new Vector3(0.0f, 0.0f, 0.0f);

                        _canFire = true;

                        GameDirector.instance.ResumeShipAnchor();

                        GameDirector.instance.DisablePlayerControl();

                        _fireTimer = Time.time + fireInterval;

                        _shootEvent = ShootEvent.NotShot;

                        GameDirector.instance.AddMissScore();
                    }
                    else
                    {
                        wormHead.position = Vector3.Lerp(wormHead.position, _originatingHeadPosition, Time.deltaTime * _wormSpeeds.x);
                        wormHead.rotation = Quaternion.Slerp(wormHead.rotation, _originatingHeadRotation, Time.deltaTime * _wormSpeeds.z);
                    }
                    break;
                }
        }
        
    }
    #endregion

    #region Private Methods
    public void ShootWorm()
    {
        Vector3 characterPosition = GameDirector.instance.character.position;

        _originatingHeadPosition = wormHead.position;
        _originatingHeadRotation = wormHead.rotation;

        _nextHeadPosition = Vector3.zero;
        _nextHeadPosition.x = characterPosition.x;
        _nextHeadPosition.y = wormTargetY;
        _nextHeadPosition.z = wormHead.position.z;

        characterPosition.z = _originatingHeadPosition.z;
        Vector3 targetDirection = characterPosition - _originatingHeadPosition;
        _nextHeadRotation = Quaternion.FromToRotation(Vector3.up, targetDirection);

        _wormSpeeds = Vector3.zero;
        _wormSpeeds.x = Vector3.Distance(_originatingHeadPosition, _nextHeadPosition) / timeToCatch;
        _wormSpeeds.z = 5.0f * Mathf.Abs(Quaternion.Dot(_nextHeadRotation, _originatingHeadRotation)) / timeToCatch;

        Vector3 appliedForce = Vector3.Cross(Vector3.forward, new Vector3(targetDirection.x, targetDirection.z, 1));

        if (targetDirection.x < 0)
        {
            appliedForce = -appliedForce;
        }

        wormPivot.force = appliedForce * constantForce;

        _shootEvent = ShootEvent.Shot;
    }

    private Vector3 GetNextSpawnLocation()
    {
        Vector3 nextSpawnLocation = Vector3.zero;

        Vector3 anchorPosition = GameDirector.instance.shipAnchor.position;

        int index = 0;

        if(anchorPosition.x < 1f)
        {
            index = Random.Range(0, 3);
        }
        else if (anchorPosition.x > 1.2f)
        {
            index = Random.Range(4, 7);
        }
        else
        {
            index = Random.Range(2, 5);
        }

        nextSpawnLocation = _spawnLocations[index];

        return nextSpawnLocation;
    }

    private void AdjustOrientation()
    {
        Vector3 characterPosition = GameDirector.instance.character.position;
        characterPosition.z = transform.position.z;

        Vector3 targetVector =  characterPosition - transform.position;

        transform.rotation = Quaternion.FromToRotation(Vector3.up, targetVector);
    }
    #endregion

    #region Public Methods
    public void StopFiringWorm()
    {
        _shootEvent = ShootEvent.NotShot;

        _canFire = false;

        wormHead.rotation = _originatingHeadRotation;

        wormPivot.force = new Vector3(0.0f, 0.0f, 0.0f);

        wormBase.rigidbody.constraints ^= RigidbodyConstraints.FreezePositionX;

        transform.parent = GameDirector.instance.shipAnchor;
    }

    public void CatchAnimationComplete()
    {
        _shootEvent = ShootEvent.Returning;
    }

    public void IncreaseDifficulty()
    {
        timeToCatch = timeToCatch - (timeToCatch / 3);
    }
    #endregion
}
