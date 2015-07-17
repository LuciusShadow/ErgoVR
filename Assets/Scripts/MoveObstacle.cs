using UnityEngine;
using System.Collections;
//TODO: Randomize speed and pause
public class MoveObstacle : MonoBehaviour {
	public float speed = 50f;
	public float tspeed = 20f;
	
	private Vector3 start;
	private Vector3 end;
	
	IEnumerator Start ()
	{
		float goalz = 30;
		if(transform.position.z > 0) goalz = -goalz;
		start = transform.position;
		end = transform.position + new Vector3(0,0,goalz);
		while (true) {
			yield return StartCoroutine (MoveObject(transform, start, end, 3f));
			yield return StartCoroutine (MoveObject(transform, end, start, 3f));
		}
	}
	
	IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
	{
		float speed = Random.Range(1.0F, 3.0F);
		float i= 0.0f;
		float rate= speed/time;
		while (i < 1.0f) {
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
			yield return null; 
		}
	}
	
	void Update ()
	{
		transform.Rotate(Vector3.up, speed * Time.deltaTime);
		transform.Rotate(Vector3.left, speed * Time.deltaTime);
		
		/*
		float step = tspeed * Time.deltaTime;
		yield transform.position = Vector3.MoveTowards(transform.position, end, step);

		//transform.position = Vector3.MoveTowards(start, end, step);
		//if(transform.position != end) transform.position = end;*/
	}
	
}
