using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class ItemDrop{
	public Item item;
	public float dropRate; //1.0 always drop, 0 never drop;
	public int mustDropRate;// how many tries before it must drop? Don't leave it to random chance;
}
