using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

	[SerializeField] private Enums.ItemType ItemType;

	private void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.layer == Layers.GROUND) {
			PoolManager.Instance.ReturnToPool (gameObject);

			if (ItemType == Enums.ItemType.GOAL) {
				GameStateManager.Instance.GoalItemHitsGround ();
			}
			if (ItemType == Enums.ItemType.DEBREE) {
				GameStateManager.Instance.DebreeItemHitsGround ();
			}
		} else if (collision.gameObject.layer == Layers.ROPE) {
			PoolManager.Instance.ReturnToPool (gameObject);

			if (ItemType == Enums.ItemType.GOAL) {
				GameStateManager.Instance.GoalItemHitsSafeNet ();
			}
			if (ItemType == Enums.ItemType.DEBREE) {
				GameStateManager.Instance.DebreeItemHitsSafeNet ();
			}
		}
	}

}
