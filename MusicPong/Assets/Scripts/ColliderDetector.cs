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

	
		if (cd.gameObject.tag.Equals ("MusicInstrument")) {
		
			Object[] clips = Resources.LoadAll ("Sounds");
			AudioClip sound = (AudioClip)clips [Random.Range (0, clips.Length)];
			AudioSource.PlayClipAtPoint (sound, Camera.main.transform.position);
			Destroy (cd.gameObject);
			Destroy (this.gameObject);

		}



	}

}
