Shader "GlobalGameJam2017/RGB_Additive"
{
    Properties
    {
        _MainTexture ("Main Texture", 2D) = "white" {}
        _TintColor ("Tint Color", Color) = (1,1,1,1)

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

            #include "UnityCG.cginc"
            #include "Include/Common.cginc"
            #include "Include/Lighting.cginc"

            sampler2D _MainTexture;
            half4 _MainTexture_ST;
            fixed4 _TintColor;

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
                o.uv1 = TRANSFORM_TEX(v.uv1, _MainTexture);

                return o;
            }
            
            fixed4 frag (vertex_output i) : SV_Target
            {   
                fixed3 tex = tex2D(_MainTexture, i.uv1).rgb;

                return fixed4(tex * _TintColor.rgb * i.color.rgb * i.color.a, 1.0);
            }
            ENDCG
        }
    }
}
