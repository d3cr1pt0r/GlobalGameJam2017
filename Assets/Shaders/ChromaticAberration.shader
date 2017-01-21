Shader "GlobalGameJam2017/ChromaticAberration"
{
    Properties
    {
    	_MainTex ("Main Texture", 2D) = "white" {}
    	_RefractionTex("Refraction Texture", 2D) = "black" {}

    	_Amount ("Amount", Range(0, 1)) = 0
    	_RefractionAmount ("Refraction Amount", Range(-0.5, 0.5)) = 0
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType"="Transparent" }
        ZWrite Off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _RefractionTex;
            fixed _Amount;
            half _RefractionAmount;

            struct vertex_input
            {
                float4 vertex : POSITION;
                fixed2 uv1 : TEXCOORD;
            };

            struct vertex_output
            {
                float4 vertex : SV_POSITION;
                fixed2 uv1 : TEXCOORD0;
            };
            
            vertex_output vert (vertex_input v)
            {
                vertex_output o;

                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv1 = v.uv1;

                return o;
            }
            
            fixed4 frag (vertex_output i) : SV_Target
            {   
            	half2 refraction = tex2D(_RefractionTex, i.uv1).rg;
            	half d = refraction * 2 - 1;

            	i.uv1 += refraction * _RefractionAmount;
            	i.uv1 = saturate(i.uv1);

            	fixed red = tex2D(_MainTex, i.uv1 + fixed2(-_Amount, _Amount/3)).r;
            	fixed green = tex2D(_MainTex, i.uv1 + fixed2(_Amount, -_Amount/3)).g;
            	fixed blue = tex2D(_MainTex, i.uv1 + fixed2(_Amount/3, _Amount)).b;

				return fixed4(red, green, blue, 1.0);
            }
            ENDCG
        }
    }
}
