using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]private EasyJoystick joystick;
    [SerializeField]private float movingSpeed = 60f;
	[Range(-10,10)]
	[SerializeField] private float xPosRange = 7.8f;
    private CharacterController playerCharacter;
	private Vector3 moveVector = Vector3.zero;
	private Rigidbody rb;


    private float rotationZ = 0f;
    private float sensitivity = 2f;
    private float rotSpeed = 100f;

	private float smoothFactor = 20f;
	Quaternion defaultRot = Quaternion.identity;
    private void Awake()
    {
       // playerCharacter = GetComponent<CharacterController>();
		rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {

		//	playerCharacter.Move(transform.forward * movingSpeed * Time.deltaTime);
		//rb.MovePosition(transform.forward * movingSpeed * Time.deltaTime);
		transform.position += transform.forward * movingSpeed * Time.deltaTime;
        if(joystick.IsPressed())
        {
			
				rotationZ += joystick.MoveInput().x * sensitivity * rotSpeed * Time.deltaTime;
				rotationZ = Mathf.Clamp(rotationZ, -15, 15);
				//  transform.rotation = Quaternion.AngleAxis(rotationZ * Time.deltaTime * rotSpeed , Vector3.up);
				//transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotationZ, transform.localEulerAngles.z);
			transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(new Vector3(transform.localEulerAngles.x, rotationZ, transform.localEulerAngles.z)), smoothFactor * Time.deltaTime);
            
        }else
        {

			//transform.localEulerAngles = Vector3.Lerp(transform.eulerAngles, Vector3.zero, smoothFactor); //new Vector3(0f, 0f, 0f);
			transform.localRotation =  Quaternion.Lerp(transform.localRotation, Quaternion.Euler(Vector3.zero),smoothFactor * Time.deltaTime);
        }
       
    }
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			//Activate Joystick
			joystick.ShowJoyStivk(true, Input.mousePosition);
		}
		else if (Input.GetMouseButtonUp(0))
		{
			// Deactivate JoyStick.
			joystick.ShowJoyStivk(false, Input.mousePosition);
		}
	}
}
