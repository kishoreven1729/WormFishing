#region References
using UnityEngine;
using System.Collections;
#endregion

public class PlayerControl : MonoBehaviour 
{
	#region Private Variables
    private float               _gestureFactor;

    private Vector2             _startPos;
    private Vector2             _endPos;

    private bool                _playerCanMove;    
	#endregion

	#region Public Variables
	public float 				forceMagnitude;
    public float                forceFluctuation;
    public float                minGestureDistance;
    public float                maxGestureDistance;
	#endregion

	#region Constructor
	void Start() 
	{
		//rigidbody.AddForce (new Vector3 (1000.0f, 0.0f, 0.0f));		

        _gestureFactor = forceFluctuation / (maxGestureDistance - minGestureDistance) ;

        _startPos = Vector2.zero;
        _endPos = Vector2.zero;

        _playerCanMove = false;
	}
	#endregion
	
	#region Loop
	void Update() 
	{
        if (_playerCanMove == true)
        {
            Vector3 direction = Vector3.zero;

            #region Keyboard
            if (Input.GetKeyDown(KeyCode.A))
            {
                direction.x = -1.0f;

                _playerCanMove = false;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                direction.x = 1.0f;

                _playerCanMove = false;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                direction.y = 1.0f;

                _playerCanMove = false;
            }

            rigidbody.AddForce(direction * forceMagnitude);            
            #endregion

            #region Touch
            if (Input.touchCount > 0)
            {
                Touch currentTouch = Input.GetTouch(0);

                switch (currentTouch.phase)
                {
                    case TouchPhase.Began:
                        {
                            _startPos = currentTouch.position;

                            break;
                        }
                    case TouchPhase.Ended:
                        {
                            _endPos = currentTouch.position;

                            float moveDistance = Vector2.Distance(_startPos, _endPos);

                            if (moveDistance > minGestureDistance)
                            {
                                if (moveDistance > maxGestureDistance)
                                {
                                    moveDistance = maxGestureDistance;
                                }

                                float magnitudeModifier = (moveDistance - minGestureDistance) * _gestureFactor;

                                direction.x = _endPos.x - _startPos.x;
                                direction.y = _endPos.y - _startPos.y;

                                float newMagnitude = forceMagnitude + magnitudeModifier;

                                if (direction.y < 0.0f)
                                {
                                    newMagnitude -= Mathf.Abs(direction.y * forceMagnitude);
                                }

                                rigidbody.AddForce(direction * newMagnitude);

                                _playerCanMove = false;
                            }

                            break;
                        }
                }
            }
            #endregion
        }
	}
	#endregion

    #region Private Methods
    #endregion

    #region Public Methods
    public void EnablePlayerMovement()
    {
        _playerCanMove = true;
    }

    public void DisablePlayerMovement()
    {
        _playerCanMove = false;
    }

    public void KillCharacter()
    {
        Vector3 characterPosition = transform.localPosition;

        characterPosition.x = 0;
        characterPosition.y = -2.57f;

        transform.localPosition = characterPosition;

        DisablePlayerMovement();
    }
    #endregion
}
