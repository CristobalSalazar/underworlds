using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControls : MonoBehaviour
{
	public static GameControls main;
	public static Vector3 inputDirection;
	public static float sensitivity;
	public static bool holdToRun;

	[SerializeField] int clockLength;
	int clockTick;
	[SerializeField] int bufferLength;
	[SerializeField] RectTransform[] ignore;
	float dragDistance;

	Vector3 phoneInput;
	Vector3 heldInput;
	Vector3 fp;
	Vector3 lp;
	Vector3 heldDirection;
	Vector3[] buffer;

	bool clockGate;
	bool holdGate;
	bool ignoreTouch;
	bool dragging;

// _____________________________________________________________________________________________________________________________________________________________________

	void Awake()
	{
		if (main == null){
			main = this;
		}
		else if (main != this) {
			Destroy(gameObject);
		}

		dragDistance = Screen.height * sensitivity/100;
		clockGate = true;
		clockTick = clockLength;
	}
	void Start()
	{
		SetBuffer(bufferLength);
	}
	void Update ()
	{
		if (bufferLength > 0)
		{
			InputBuffer();
		}
		else
		{
			InputClock();
		}
	}
// _____________________________________________________________________________________________________________________________________________________________________

	public void ConsumeInput()
	{
		if (bufferLength > 0)
		{
			for (int i = 0; i < bufferLength - 1; i ++)
			{
				buffer[i] = buffer[i + 1];
			}

			buffer[bufferLength - 1] = Vector3.zero;
		}

	}
	public void SetBuffer(int length)
	{
		bufferLength = length;
		buffer = new Vector3[bufferLength];
		ClearBuffer();
	}
	public void ClearBuffer()
	{
		if(bufferLength > 0)
		{
			for (int i = 0; i < bufferLength; i ++ )
			{
				buffer[i] = Vector3.zero;
			}
		}
	}
	public void SetClock(int frames)
	{
		clockTick = frames;
	}
// _____________________________________________________________________________________________________________________________________________________________________

	Vector3 GetUniversalInput ()
	{
		float horizontalInput = Input.GetAxisRaw("Horizontal");
		float verticalInput = Input.GetAxisRaw("Vertical");
		inputDirection = new Vector3(horizontalInput, verticalInput, 0);
		return inputDirection;
	}

	Vector3 GetPhoneInput ()
	{
		if (Input.touchCount > 0)
		{
		    Touch myTouch = Input.touches[0];
				if (myTouch.phase == TouchPhase.Began) {
						foreach (RectTransform rt in ignore) {
								if (RectTransformUtility.RectangleContainsScreenPoint(rt, myTouch.position,Camera.main))
								{
									ignoreTouch = true;
								}
						}

						dragging = false;
						holdGate = true;
						fp = myTouch.position;
						phoneInput = Vector3.zero;
						return phoneInput;
				}

		    else if (myTouch.phase == TouchPhase.Moved &&! ignoreTouch)
		    {
				lp = myTouch.position;
				dragDistance = Screen.height * sensitivity /100;

				if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance) // Greater than sensitivity
				{
					dragging = true;

					if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y)) // horizontal
					{
						if ((lp.x > fp.x)) // right
					    {
							if (holdGate)// first time through
							{
								clockTick = 0; // reset clock
								holdGate = false;
								heldInput = Vector3.right;
								phoneInput = Vector3.right; // input to right
							}
							else if (!holdGate) //subsequent times
							{
								heldInput = Vector3.right;
							}
					    }
					    else // left
					    {
							if (holdGate)
							{
								clockTick = 0;
								phoneInput = Vector3.left;
								holdGate = false;
								heldInput = Vector3.left;
							}
							else if (!holdGate)
							{
								heldInput = Vector3.left;
							}
						}

						fp = lp;
					}
					else if (Mathf.Abs(lp.x - fp.x) < Mathf.Abs(lp.y - fp.y)) // vertical
					{
						if (lp.y > fp.y) //up
						{
							if (holdGate)
							{
								clockTick = 0;
								phoneInput = Vector3.up;
								holdGate = false;
								heldInput = Vector3.up;
							}
							else if (!holdGate)
							{
								heldInput = Vector3.up;
							}
						}
						else //down
						{
							if (holdGate)
							{
								clockTick = 0;
								phoneInput = Vector3.down;
								holdGate = false;
								heldInput = Vector3.down;
							}
							else if (!holdGate)
							{
								heldInput = Vector3.down;
							}
						}
						fp = lp;
					}
					if (heldInput != phoneInput)
					{
						clockTick = 0;
						phoneInput = heldInput;
					}
					return phoneInput;
				}
			}
			else if (myTouch.phase == TouchPhase.Ended)
			{
				ignoreTouch = false;
				holdGate = true;
				phoneInput = Vector3.zero;
				heldInput = Vector3.zero;
				if (!dragging)
				{
					//phoneInput = Vector3.forward;
					clockTick = 0;
				}
				return phoneInput;
			}
			if (holdToRun)
			{
				return phoneInput;
			}
		}
		ignoreTouch = false;
		return Vector3.zero;
	}
	Vector3 InputClock()
	{
	#if UNITY_EDITOR

			inputDirection = GetUniversalInput();
	#else
			inputDirection = GetPhoneInput();
	#endif

		if (clockTick <= 0)
		{
			clockTick = clockLength;
			clockGate = true;
		}
		if (inputDirection != Vector3.zero && clockGate)
		{
			clockGate = false;
			return inputDirection;
		}
		else
		{
			clockGate = false;
			clockTick --;
			inputDirection = Vector3.zero;
			return inputDirection;
		}
	}
	Vector3[] InputBuffer()
	{
		Vector3 currentInput = InputClock();

		if (currentInput != Vector3.zero)
		{
			for (int i = bufferLength - 1; i > 0; i --)
			{
				buffer[i] = buffer [i - 1];
			}

			buffer[0] = currentInput;
		}
		for (int i = bufferLength - 1; i > -1; i --)
		{
			if (buffer[i] != Vector3.zero)
			{
				inputDirection = buffer[i];
				break;
			}
		}
		return buffer;
	}
}