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
	public float fadeDegree;
	private bool fade=false;
	private GameObject colInstrument;




	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		StartCoroutine (FaderCor ());
//		Fade();
	}

	void OnTriggerEnter2D (Collider2D cd)
	{

		string tag = cd.gameObject.tag;
			
		if (tag.Equals ("Guitar")) {
		


			Object[] clips = Resources.LoadAll ("Sounds2/Guitar");
			AudioClip sound = (AudioClip)clips [Random.Range (0, clips.Length)];
			playCollision (sound);
			prepFadeOut (cd.gameObject);
		



		} else if (tag.Equals ("Violin")) {


			Object[] clips = Resources.LoadAll ("Sounds2/Violin");
			AudioClip sound = (AudioClip)clips [Random.Range (0, clips.Length)];
			playCollision (sound);
			prepFadeOut (cd.gameObject);





		} else if (tag.Equals ("Piano")) {


			Object[] clips = Resources.LoadAll ("Sounds2/Piano");
			AudioClip sound = (AudioClip)clips [Random.Range (0, clips.Length)];
			playCollision (sound);
			prepFadeOut (cd.gameObject);



		} else if (tag.Equals ("Trumpet")) {
			Object[] clips = Resources.LoadAll ("Sounds2/Trumpet");
			AudioClip sound = (AudioClip)clips [Random.Range (0, clips.Length)];
			playCollision (sound);
			prepFadeOut (cd.gameObject);
		}else if(tag.Equals("Saxophone")){
			Object[] clips = Resources.LoadAll ("Sounds2/Saxophone");
			AudioClip sound = (AudioClip)clips [Random.Range (0, clips.Length)];
			playCollision (sound);
			prepFadeOut (cd.gameObject);
		}


	}

	private void playCollision (AudioClip sound)
	{
		GameObject go = null;
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





	private void prepFadeOut(GameObject go){
		go.GetComponent<PolygonCollider2D> ().enabled = false;
		fade = true;
		colInstrument = go;
		SpriteRenderer sr = go.GetComponent<SpriteRenderer> ();
		Color c = sr.color;
		c.a = 1;
		sr.color = c;
		go.GetComponent<InstrumentInfo> ().isColliding = true;

	
	}

	IEnumerator FaderCor ()
	{
		
			
		if (fade && colInstrument!=null) {
				
			SpriteRenderer sr = colInstrument.GetComponent<SpriteRenderer> ();
			if (sr.color.a >= 0) {
				
				Color c = sr.color;
				c.a -= fadeDegree * Time.deltaTime / 255f;
				sr.color = c;
			} else {
				colInstrument.GetComponent<InstrumentInfo> ().isColliding = false;
				Destroy (colInstrument);
				colInstrument = null;
				fade = false;
			}

		}
		yield return null;
	}



}
