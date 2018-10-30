using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour {
	[SerializeField]
	private float throwSpeed;
	private float speed;
	private float lastMousex, lastMousey;

	private bool thrown,holding;
	private Rigidbody _rigidbody;
	private Vector3 newPosition;
	// Use this for initialization
	public void Start () {
		_rigidbody = GetComponent<Rigidbody> ();
		Reset ();

	}
	public void OnTouch(){
		Vector3 mousePos = Input.GetTouch (0).position;
		mousePos.z = Camera.main.nearClipPlane * 7.5f;
		newPosition = Camera.main.ScreenToWorldPoint (mousePos);
		transform.localPosition = Vector3.Lerp (transform.localPosition,newPosition,50f * Time.deltaTime);
	}
	public void ThrowBall(Vector2 mousePos){
		_rigidbody.useGravity = true;
		float diffrenceY = (mousePos.y - lastMousey) / Screen.height * 10;
		speed = throwSpeed - diffrenceY;
		float x = (mousePos.x / Screen.width) - (lastMousex / Screen.width);
		x = Mathf.Abs (Input.GetTouch(0).position.x - lastMousex) / Screen.width * 100 * x;
		Vector3 direction = new Vector3 (x,0f,1f);
		direction = Camera.main.transform.TransformDirection (direction);

		_rigidbody.AddForce ((direction*speed*2f)+(Vector3.up*speed));
		holding = false;
		thrown = true;
		Invoke ("Reset", 5.0f);


	}

	// Update is called once per frame
	public void Update () {
		if(holding){

			OnTouch ();
		}
		if (thrown) {
			return;
		}
		if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began){
			Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit, 100f)) {
				if(hit.transform==transform){
					holding=true;
					transform.SetParent(null);
				}
			}
		}
		if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended){
			if (lastMousey < Input.GetTouch (0).position.y) {
				ThrowBall (Input.GetTouch (0).position);
			}
			if(Input.touchCount == 1){
				lastMousex = Input.GetTouch (0).position.x;
				lastMousey = Input.GetTouch (0).position.y;
			}
		}
	}
	public void Reset(){
		CancelInvoke ();
		transform.position = Camera.main.ViewportToWorldPoint (new Vector3(0.5f,0.1f,Camera.main.nearClipPlane * 7.5f));
		newPosition = transform.position;
		thrown = holding = false;

		_rigidbody.useGravity = false;
		_rigidbody.velocity = Vector3.zero;
		_rigidbody.angularVelocity = Vector3.zero;
		transform.rotation = Quaternion.Euler (0f,200f,0f);
		transform.SetParent (Camera.main.transform);
	}


}