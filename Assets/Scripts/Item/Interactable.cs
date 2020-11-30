using UnityEngine;

/*	
	This component is for all objects that the player can
	interact with such as enemies, items etc. It is meant
	to be used as a base class.
*/

public class Interactable : MonoBehaviour
{
	public GameObject interactable;
	public bool interact;
	public virtual void Interact()
	{
		// This method is meant to be overwritten
		//Debug.Log("Interacting with " + transform.name);
	}

	void Update()
	{
        if (Input.GetKeyDown(KeyCode.E))
        {
			interact = true;
        }
		
		if (Input.GetKeyUp(KeyCode.E))
        {
			interact = false;
        }
	}

    public void OnCollisionStay2D(Collision2D collision)
    {
		if(interactable.activeSelf && interact)
			Interact();
    }

}