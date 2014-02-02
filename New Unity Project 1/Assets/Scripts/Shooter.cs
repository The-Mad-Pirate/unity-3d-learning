using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
	public Rigidbody bullet;
	public float power = 2250f;
	public float moveSpeed = 5f;
	private Vector3 ballRotationVector = new Vector3();
	private Vector3 ballAngularVelocity = new Vector3();
	private const float radius = 1.0f;
	
	void Start() {
		
		System.Random randGen = new System.Random();
		
		ballAngularVelocity.x = (float) (randGen.NextDouble() * 2*Mathf.PI);
		ballAngularVelocity.z = (float) (randGen.NextDouble() * 2*Mathf.PI);
		ballAngularVelocity.y = (float) (randGen.NextDouble() * Mathf.PI);		
		
	}
	
	void Update () {
		
		float h = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
		float v = Input.GetAxis("Vertical")   * Time.deltaTime * moveSpeed;
				
		ballRotationVector.x += ballRotationVector.x + ballAngularVelocity.x * Time.deltaTime;
		ballRotationVector.y += ballRotationVector.y + ballAngularVelocity.y * Time.deltaTime;
		ballRotationVector.z += ballRotationVector.z + ballAngularVelocity.z * Time.deltaTime;
		
		ballRotationVector.Normalize();
				
		transform.Translate(h, v, 0);
		
		if( Input.GetButtonUp("Fire1")){
//			Rigidbody instance = Instantiate( bullet, transform.position, transform.rotation) as Rigidbody;
			Quaternion rotation = new Quaternion(ballRotationVector.x,
			                                     ballRotationVector.y,
			                                     ballRotationVector.z,
			                                     ballAngularVelocity.magnitude/2*Mathf.PI);
			Rigidbody instance = Instantiate( bullet, transform.position, rotation) as Rigidbody;
			Vector3 fwd = transform.TransformDirection(Vector3.forward);
			
			instance.MoveRotation(rotation);
			instance.AddForce( fwd * power );
			instance.AddTorque( Vector3.Cross(ballAngularVelocity, fwd ) );
					
	
		}
	}
}