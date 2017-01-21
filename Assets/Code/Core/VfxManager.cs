using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxManager : Singleton<VfxManager>
{

	[SerializeField] private ParticleSystem ItemGroundHitVfx;

	protected VfxManager ()
	{
	}

	public void EmitItemGroundHitVfx (Vector3 position)
	{
		ItemGroundHitVfx.transform.position = position;
		ItemGroundHitVfx.Play ();
	}

}
