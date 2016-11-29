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
	private static float bufferTime = 0.5f;

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
//			StartCoroutine(FadeOut(cd.gameObject));
//			Destroy (cd.gameObject,5);
		
			Destroy (cd.gameObject,bufferTime);

//			Destroy (this.gameObject);

		} else if (cd.gameObject.tag.Equals ("Violin")) {


			Object[] clips = Resources.LoadAll ("Sounds2/Violin");
			AudioClip sound = (AudioClip)clips [Random.Range (0, clips.Length)];
			playCollision (sound);
//			StartCoroutine(FadeOut(cd.gameObject));
//			Destroy (cd.gameObject,5);

			Destroy (cd.gameObject,bufferTime);

//			Destroy (this.gameObject);

		} else if (cd.gameObject.tag.Equals ("Piano")) {


			Object[] clips = Resources.LoadAll ("Sounds2/Piano");
			AudioClip sound = (AudioClip)clips [Random.Range (0, clips.Length)];
			playCollision (sound);
//			StartCoroutine(FadeOut(cd.gameObject));
//			Destroy (cd.gameObject,5);

			Destroy (cd.gameObject,bufferTime);

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

	IEnumerator FadeOut(GameObject go){
		if (go != null) {

			SpriteRenderer sr = go.GetComponent<SpriteRenderer> ();
			if (sr != null) {
				if (sr != null) {
					while (sr.color.a > 0) {


						Color c = sr.color;
						c.a -= Time.deltaTime / 0.5f;
						sr.color = c;
						yield return null;
						if (sr == null)
							break;

					}
				}
			}
		}
	}

}
