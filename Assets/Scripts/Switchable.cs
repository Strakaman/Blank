using System.Collections;
using UnityEngine;

public abstract class Switchable : MonoBehaviour
{	
	public abstract void flipStatus(); //when the switchable is associated with a switch that user will activate
	public abstract void setStatus(bool activeStatus); //when the switchable is associated with multiple triggers
}

