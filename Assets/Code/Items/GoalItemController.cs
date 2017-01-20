using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalItemController : MonoBehaviour
{

	private void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.layer == Layers.GROUND) {
			GameStateManager.Instance.GoalItemHitsGround ();
		} else if (collision.gameObject.layer == Layers.SAFE_NET) {
			GameStateManager.Instance.GoalItemHitsSafeNet ();
		}
	}

}
