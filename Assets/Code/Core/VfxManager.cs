using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxManager : Singleton<VfxManager>
{

	[SerializeField] private ParticleSystem GoalGroundHitVfx;
	[SerializeField] private ParticleSystem DebreeGroundHitVfx;

	protected VfxManager ()
	{
	}

	public void EmitGoalGroundHitVfx (Vector3 position)
	{
		GoalGroundHitVfx.transform.position = position;
		GoalGroundHitVfx.Play ();
	}

	public void EmitDebreeGroundHitVfx (Vector3 position)
	{
		DebreeGroundHitVfx.transform.position = position;
		DebreeGroundHitVfx.Play ();
	}

}
