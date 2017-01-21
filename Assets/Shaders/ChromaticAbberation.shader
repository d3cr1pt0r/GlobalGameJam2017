Shader "GlobalGameJam2017/ChromaticAbberation"
{
    Properties
    {
    	_MainTex ("Base (RGB)", 2D) = "white" {}
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
                return tex2D(_MainTex, i.uv1);
            }
            ENDCG
        }
    }
}
