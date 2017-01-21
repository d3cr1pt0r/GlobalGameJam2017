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
				VfxManager.Instance.EmitGoalGroundHitVfx (gameObject.transform.position);
				GameStateManager.Instance.GoalItemHitsGround ();
			}
			if (ItemType == Enums.ItemType.DEBREE) {
				VfxManager.Instance.EmitDebreeGroundHitVfx (gameObject.transform.position);
				GameStateManager.Instance.DebreeItemHitsGround ();
			}
		}
	}

	public void OnCollisionEnterRope ()
	{
		PoolManager.Instance.ReturnToPool (gameObject);

		if (ItemType == Enums.ItemType.GOAL) {
			GameStateManager.Instance.GoalItemHitsSafeNet ();
		}
		if (ItemType == Enums.ItemType.DEBREE) {
			VfxManager.Instance.StartChromaticAbberation ();
			GameStateManager.Instance.DebreeItemHitsSafeNet ();
		}
	}

}
