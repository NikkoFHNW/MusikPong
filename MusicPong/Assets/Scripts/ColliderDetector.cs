using UnityEngine;
using System.Collections;
using System.Threading;

public class ColliderDetector : MonoBehaviour
{
	public GameObject particleSys1;
	public GameObject particleSys2;
	public GameObject particleSys3;
	public GameObject particleSys4;

	private static float particleTime = 15;

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
			playCollision (sound);
			Destroy (cd.gameObject);

//			Destroy (this.gameObject);

		} else if (cd.gameObject.tag.Equals ("Violin")) {


			Object[] clips = Resources.LoadAll ("Sounds2/Violin");
			AudioClip sound = (AudioClip)clips [Random.Range (0, clips.Length)];
			playCollision (sound);
			Destroy (cd.gameObject);

//			Destroy (this.gameObject);

		} else if (cd.gameObject.tag.Equals ("Piano")) {


			Object[] clips = Resources.LoadAll ("Sounds2/Piano");
			AudioClip sound = (AudioClip)clips [Random.Range (0, clips.Length)];
			playCollision (sound);
			Destroy (cd.gameObject);

//			Destroy (this.gameObject);

		}



	}

	private void playCollision (AudioClip sound)
	{
		GameObject go=null;
		int rand = Random.Range (0, 4);
		switch (rand) {
		case 0:
			go = Instantiate (particleSys1, this.transform.position, Quaternion.identity) as GameObject;
			break;
		case 1:
			go = Instantiate (particleSys2, this.transform.position, Quaternion.identity) as GameObject;
			break;
		case 2:
			go = Instantiate (particleSys3, this.transform.position, Quaternion.identity) as GameObject;
			break;
		case 3:
			go = Instantiate (particleSys4, this.transform.position, Quaternion.identity) as GameObject;
			break;
		}


		AudioSource.PlayClipAtPoint (sound, Camera.main.transform.position);
		Destroy (go, particleTime);
	}

}
