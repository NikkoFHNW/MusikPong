using UnityEngine;
using System.Collections;
using System.Threading;

public class ColliderDetector : MonoBehaviour
{


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter2D (Collider2D cd)
	{

	
		if (cd.gameObject.tag.Equals ("Guitar")) {
		
			Object[] clips = Resources.LoadAll ("Sounds2/Guitar");
			AudioClip sound = (AudioClip)clips [Random.Range (0, clips.Length)];
			AudioSource.PlayClipAtPoint (sound, Camera.main.transform.position);
			Destroy (cd.gameObject);
//			Destroy (this.gameObject);

		}
		else if (cd.gameObject.tag.Equals ("Violin")) {

			Object[] clips = Resources.LoadAll ("Sounds2/Violin");
			AudioClip sound = (AudioClip)clips [Random.Range (0, clips.Length)];
			AudioSource.PlayClipAtPoint (sound, Camera.main.transform.position);
			Destroy (cd.gameObject);
			//			Destroy (this.gameObject);

		}



	}

}
