using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterModel : MonoBehaviour {
	public int MaxHP;
	public int HP;
	public int MaxSP;
	public int SP;
	public GameObject MainWeapon;
	public GameObject SubWeapon;
	public GameObject Accessory1;
	public GameObject Accessory2;
	public bool Active;
	public ActionModel[] Actions;
}
