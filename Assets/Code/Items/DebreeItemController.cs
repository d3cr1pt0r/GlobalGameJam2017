using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebreeItemController : MonoBehaviour
{

	private void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.layer == Layers.GROUND) {
			GameStateManager.Instance.DebreeItemHitsGround ();
		} else if (collision.gameObject.layer == Layers.SAFE_NET) {
			GameStateManager.Instance.DebreeItemHitsSafeNet ();
		}
	}
		
}
