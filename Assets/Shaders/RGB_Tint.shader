Shader "GlobalGameJam2017/RGB_Tint"
{
    Properties
    {
    	_MainTexture ("Main Texture", 2D) = "black" {}
    	_TintColor ("Tint Color", Color) = (1,1,1,1)

    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType"="Transparent" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTexture;
            fixed4 _TintColor;

            struct vertex_input
            {
                float4 vertex : POSITION;
                fixed2 uv1 : TEXCOORD;
            };

            struct vertex_output
            {
                float4 vertex : SV_POSITION;
                fixed2 uv1 : TEXCOORD1;
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
            	fixed3 tex = tex2D(_MainTexture, i.uv1).rgb;

                return fixed4(tex * _TintColor.rgb, 1.0);
            }
            ENDCG
        }
    }
}
