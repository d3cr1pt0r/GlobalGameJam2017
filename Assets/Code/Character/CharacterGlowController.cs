using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGlowController : MonoBehaviour
{

	[SerializeField] private Renderer[] glowRenderers;
	[SerializeField] private AnimationCurve GlowAnimationCurve;
	[SerializeField] private float GlowSpeed;

	private float GlowTimer;
	private int _BurnAmountID;

	private void Awake ()
	{
		GlowTimer = 1.0f;
		_BurnAmountID = Shader.PropertyToID ("_BurnAmount");

		for (int i = 0; i < glowRenderers.Length; i++) {
			//glowRenderers [i].material.SetFloat (_BurnAmountID, 0);
		}
	}

	private void Update ()
	{
		if (GlowTimer < 1.0f) {
			float val = GlowAnimationCurve.Evaluate (GlowTimer);

			for (int i = 0; i < glowRenderers.Length; i++) {
				glowRenderers [i].material.SetFloat (_BurnAmountID, val);
			}

			GlowTimer += Time.deltaTime * GlowSpeed;
		}
	}

	public void StartGlowVfx ()
	{
		GlowTimer = 0.0f;
	}

}
