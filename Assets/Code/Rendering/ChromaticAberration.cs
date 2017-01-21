using UnityEngine;
using System.Collections;

public class ChromaticAberration : MonoBehaviour
{
	private const string Tag = "ChromaticAberration";

	public Material ChromaticAberrationMaterial;

	[SerializeField] private AnimationCurve AmountCurve = null;
	[SerializeField] private float CurveSpeed = 1.0f;
	private float ChromaticAberrationTimer;
	private int AmountID;

	private void OnRenderImage (RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit (src, dest, ChromaticAberrationMaterial);
	}

	private void Awake ()
	{
		AmountID = Shader.PropertyToID ("_Amount");
		ChromaticAberrationTimer = 1.0f;
	}

	private void Update ()
	{
		if (ChromaticAberrationTimer < 1.0f) {
			float val = AmountCurve.Evaluate (ChromaticAberrationTimer);
			ChromaticAberrationTimer += Time.deltaTime * CurveSpeed;

			ChromaticAberrationMaterial.SetFloat (AmountID, val);
		}
	}

	public void StartVfx ()
	{
		ChromaticAberrationTimer = 0.0f;
	}
}