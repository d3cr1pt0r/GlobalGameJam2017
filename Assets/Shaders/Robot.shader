Shader "GlobalGameJam2017/Robot"
{
    Properties
    {
    	_MainTex ("Main Texture", 2D) = "black" {}
        _AlphaTex ("Alpha Texture", 2D) = "white" {}

        _BurnColor ("Burn Color", Color) = (1,1,1,1)
        _BurnAmount ("Burn Amount", Range(0, 5)) = 0

    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Include/Common.cginc"
            #include "Include/Lighting.cginc"

            sampler2D _MainTex;
            sampler2D _AlphaTex;

            fixed4 _BurnColor;
            half _BurnAmount;

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
            	fixed3 tex = tex2D(_MainTex, i.uv1).rgb;
                fixed alpha = tex2D(_AlphaTex, i.uv1).a;

                return fixed4(i.color.rgb * tex + (_BurnColor.rgb * _BurnAmount), i.color.a * alpha);
            }
            ENDCG
        }
    }
}
