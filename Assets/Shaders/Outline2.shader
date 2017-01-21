Shader "GlobalGameJam2017/Outline2"
{
	Properties
	{
		_MainTex ("Main Texture", 2D) = "white" {}
		_OutlineTex ("Outline Texture", 2D) = "black" {}
		_Size ("Size", Float) = 0.1
		_BlurAmount("Blur Amount", Float) = 0.0075
		_OutlineColor ("Outline Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags {  }

		// Silhouette
		Pass
		{
			CGPROGRAM
			#pragma vertex vertex_shader
			#pragma fragment fragment_shader
			
			#include "UnityCG.cginc"

			fixed4 _OutlineColor;
			float _Size;

			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				fixed2 texcoord : TEXCOORD0;
			};

			struct fragmentInput
			{
				float4 vertex : SV_POSITION;
				fixed2 texcoord : TEXCOORD0;
			};
			
			fragmentInput vertex_shader (vertexInput v)
			{
				fragmentInput o;

				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = v.texcoord;

				return o;
			}
			
			fixed4 fragment_shader (fragmentInput i) : SV_Target
			{
				return _OutlineColor;
			}
			ENDCG
		}

		GrabPass {  }

		// Blur vertical
		Pass
		{
			CGPROGRAM
			#pragma vertex vertex_shader
			#pragma fragment fragment_shader
			
			#include "UnityCG.cginc"

			sampler2D _GrabTexture;

			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				fixed2 texcoord : TEXCOORD0;
			};

			struct fragmentInput
			{
				float4 vertex : SV_POSITION;
				fixed2 texcoord : TEXCOORD0;
				fixed4 screenPos : TEXCOORD1;
			};
			
			fragmentInput vertex_shader (vertexInput v)
			{
				fragmentInput o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = v.texcoord;
				o.screenPos = ComputeScreenPos(o.vertex);
				return o;
			}
			
			fixed4 fragment_shader (fragmentInput i) : SV_Target
			{
				fixed4 tex = tex2D(_GrabTexture, i.screenPos.xy);
				return tex;
			}
			ENDCG
		}
	}
}