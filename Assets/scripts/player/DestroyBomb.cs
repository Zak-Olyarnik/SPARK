using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBomb : MonoBehaviour
{
	public float timer = 3.0f;
	public GameObject bombCollider;
	//public AudioClip bombSound;

	void Start()
	{
		Invoke("BlowUp", timer);	// Set time before bomb expodes
        bombCollider.transform.SetParent(null);
    }

	void BlowUp()
	{
		bombCollider.SetActive(true);	// activates a trigger collider signifying range of explosion
        AudioSource aso = GetComponent<AudioSource>();
        aso.Play();
        gameObject.transform.position = new Vector3(0, -500, 0);    // allows audio to be played
        //AudioSource.PlayClipAtPoint(bombSound, transform.position);
		Destroy(gameObject, aso.clip.length);    // delay by time of sound clip
										// Destroy() of destroyable object handled by Destroyable.cs
	}
}
