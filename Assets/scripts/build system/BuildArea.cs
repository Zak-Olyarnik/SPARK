using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildArea : MonoBehaviour
{
	public GameObject buildableGear, buildableBattery, buildableMagnet;	// references to buildables present
	public GameObject mobiLift, electromagnet;					// machines to instantiate
	private AudioSource sourceBuildArea;
    private bool safe = true;

    void Awake()
	{
		sourceBuildArea = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider coll)
	{
		// player has brought the required Buildable to this location
		if (coll.tag == "Gear")
		{
			buildableGear = coll.gameObject;
		}
		if (coll.tag == "Battery")
		{
			buildableBattery = coll.gameObject;
		}
		if (coll.tag == "Magnet")
		{
			buildableMagnet = coll.gameObject;
		}
		// more Buildables...

		if (coll.tag == "AlphaConstruct")   // player is trying to Build
		{
			ConstructObj();
		}
	}

	void OnTriggerStay(Collider coll)	// needed for if the player tries bringing more than one of an object to the BA, then removing it
	{
		// player has brought the required Buildable to this location
		if (coll.tag == "Gear")
		{
			buildableGear = coll.gameObject;
		}
		if (coll.tag == "Battery")
		{
			buildableBattery = coll.gameObject;
		}
		if (coll.tag == "Magnet")
		{
			buildableMagnet = coll.gameObject;
		}
		// more Buildables...
	}

	void OnTriggerExit(Collider coll)
	{
		// player removes Buildable from this location, clear reference
		if (coll.tag == "Gear")
		{
			buildableGear = null;
		}
		if (coll.tag == "Battery")
		{
			buildableBattery = null;
		}
		if (coll.tag == "Magnet")
		{
			buildableMagnet = null;
		}
		// more Buildables...
	}

	void ConstructObj()
	{
		AlphaController alpha=GameObject.FindObjectOfType<AlphaController>();
		// if both required objects are present, instantiate
		if(buildableGear && buildableMagnet && safe)	// mobi-lift
		{
            safe = false;
            Invoke("MakeSafe", 0.5f);
            sourceBuildArea.Play();
			Destroy(buildableGear);
			Destroy(buildableMagnet);
			Instantiate(mobiLift, transform.position + new Vector3(0, 1.25f, 0), new Quaternion(0, 0, 0, 0));

			if(alpha) {alpha.animator.primary(); }
		}
		else if (buildableMagnet && buildableBattery && safe)	// electromagnet
		{
            safe = false;
            Invoke("MakeSafe", 0.5f);
            sourceBuildArea.Play();
			Destroy(buildableMagnet);
			Destroy(buildableBattery);
			Instantiate(electromagnet, transform.position + new Vector3(0, 2, 0), new Quaternion(0, 0, 0, 0));

			if(alpha) {alpha.animator.primary(); }
		}
		//if (buildableA && buildableB)...
	}

    private void MakeSafe()
    {
        safe = true;
    }


}
