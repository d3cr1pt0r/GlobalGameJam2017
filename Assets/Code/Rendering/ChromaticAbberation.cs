using UnityEngine;
using System.Collections;

public class ChromaticAbberation : MonoBehaviour
{
	public Material ChromaticAbberationMaterial;

	void OnRenderImage (RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit (src, dest, ChromaticAbberationMaterial);
	}
}