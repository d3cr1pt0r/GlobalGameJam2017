using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

	[SerializeField] private Enums.ItemType ItemType;

	private Rigidbody2D rigidBody2D;

	public Enums.ItemType GetItemType ()
	{
		return ItemType;
	}

	private void Awake ()
	{
		rigidBody2D = GetComponent<Rigidbody2D> ();
	}

	private void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.layer == Layers.GROUND) {
			ResetRigidBodyPhysics ();
			PoolManager.Instance.ReturnToPool (gameObject);

			if (ItemType == Enums.ItemType.GOAL) {
				VfxManager.Instance.StartChromaticAbberation ();
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
		ResetRigidBodyPhysics ();
		PoolManager.Instance.ReturnToPool (gameObject);

		if (ItemType == Enums.ItemType.GOAL) {
			GameStateManager.Instance.GoalItemHitsSafeNet ();
		}
		if (ItemType == Enums.ItemType.DEBREE) {
			VfxManager.Instance.StartChromaticAbberation ();
			GameStateManager.Instance.DebreeItemHitsSafeNet ();
		}
	}

	private void ResetRigidBodyPhysics ()
	{
		rigidBody2D.velocity = Vector3.zero;
		rigidBody2D.angularVelocity = 0.0f;
		rigidBody2D.inertia = 0.0f;
	}

}
