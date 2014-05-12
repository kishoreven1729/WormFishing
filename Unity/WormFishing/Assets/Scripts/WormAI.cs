#region References
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#endregion

public class WormAI : MonoBehaviour
{
    #region Private Variables
    private float           _fireTimer;
    private bool            _canFire;
    private WormControl     _wormControl;
    private List<Vector3>   _spawnLocations;
    private int             _spawnCount;
    #endregion

    #region Public Variables
    public float            fireInterval;    
    #endregion

    #region Constructor
    void Start () 
    {
        _fireTimer = Time.time + fireInterval;

        _canFire = true;

        _wormControl = transform.GetChild(0).GetComponent<WormControl>();

        _spawnLocations = new List<Vector3>();

        Transform spawnLocationsTransform = GameObject.FindGameObjectWithTag("WormSpawn").transform;

        _spawnCount = spawnLocationsTransform.childCount;

        foreach(Transform spawnLocation in spawnLocationsTransform)
        {
            _spawnLocations.Add(spawnLocation.position);
        }
	}
    #endregion

    #region Loop
    void Update () 
    {
        if(Time.time > _fireTimer)
        {
            if(_canFire == true)
            {
                transform.position = GetNextSpawnLocation();

                AdjustOrientation();

                _wormControl.PlayAnimation("Pullup");

                _canFire = false;

                GameDirector.instance.HaltShipAnchor();

                GameDirector.instance.EnablePlayerControl();
            }
        }
    }
    #endregion

    #region Private Methods
    private Vector3 GetNextSpawnLocation()
    {
        Vector3 nextSpawnLocation = Vector3.zero;

        Vector3 anchorPosition = GameDirector.instance.shipAnchor.position;
        Vector3 anchorVelocity = GameDirector.instance.shipAnchor.rigidbody.velocity;

        int index = 0;

        if(anchorPosition.x < 0.0f)
        {
            if(anchorVelocity.x < 0.0f)
            {
                index = Random.Range(0, 3);
            }
            if(anchorVelocity.x >= 0.0f)
            {
                index = Random.Range(1, 4);
            }
        }
        else if(anchorPosition.x > 0.0f)
        {
            if (anchorVelocity.x < 0.0f)
            {
                index = Random.Range(1, 4);
            }
            if (anchorVelocity.x >= 0.0f)
            {
                index = Random.Range(2, 5);
            }
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
    public void AnimationEnded()
    {
        _canFire = true;

        GameDirector.instance.ResumeShipAnchor();

        GameDirector.instance.DisablePlayerControl();

        transform.rotation = Quaternion.identity;

        _fireTimer = Time.time + fireInterval;
    }
    #endregion
}
