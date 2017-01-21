Shader "GlobalGameJam2017/A_Vcolor_Additive"
{
    Properties
    {
        _AlphaTexture ("Alpha Texture", 2D) = "white" {}

    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType"="Transparent" }
        Blend One One
        ZWrite Off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Include/Common.cginc"
            #include "Include/Lighting.cginc"

            sampler2D _AlphaTexture;

            struct vertex_input
            {
                float4 vertex : POSITION;
                fixed4 color : COLOR;
                fixed2 uv1 : TEXCOORD;
            };

            struct vertex_output
            {
                float4 vertex : SV_POSITION;
                fixed4 color : TEXCOORD0;
                fixed2 uv1 : TEXCOORD1;
            };
            
            vertex_output vert (vertex_input v)
            {
                vertex_output o;

                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.color = v.color;
                o.uv1 = v.uv1;

                return o;
            }
            
            fixed4 frag (vertex_output i) : SV_Target
            {   
                fixed alpha = tex2D(_AlphaTexture, i.uv1).a;

                return fixed4(i.color.rgb * alpha, 1.0);
            }
            ENDCG
        }
    }
}
