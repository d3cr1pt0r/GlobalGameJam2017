Shader "Outfit7/BRDF"
{
    Properties
    {
        _MainTexture ("Main Texture", 2D) = "white" {}
        _BRDFTexture ("BRDF Texture", 2D) = "black" {}

        _TintColor ("Tint Color", Color) = (1,1,1,1)

        _AmbientColor ("Ambient Color", Color) = (1,1,1,1)
        _BRDFMultiplier ("BRDF Multiplier", Range(0, 10)) = 1

        _SpecularColor ("Specular Color", Color) = (1,1,1,1)
        _SpecularShininess ("Specular Shininess", Range(0, 100)) = 1
        _SpecularMultiplier ("Specular Multiplier", Range(0, 5)) = 1

        _RimColor ("Rim Color", Color) = (1,1,1,1)
        _RimPower ("Rim Power", Range(0, 30)) = 1
        _RimMultiplier ("Rim Multiplier", Range(0, 30)) = 1
        _RimPassThrough ("Rim PassThrough", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags {"Queue" = "Geometry" "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Include/Common.cginc"
            #include "Include/Lighting.cginc"

            sampler2D _MainTexture;
            sampler2D _BRDFTexture;

            fixed4 _TintColor;

            fixed3 _AmbientColor;
            half _BRDFMultiplier;

            fixed3 _SpecularColor;
            half _SpecularShininess;
            half _SpecularMultiplier;

            fixed3 _RimColor;
            half _RimPower;
            half _RimMultiplier;
            fixed _RimPassThrough;

            uniform half3 _LightDirection;

            struct vertex_input
            {
                float4 vertex : POSITION;
                fixed2 uv1 : TEXCOORD0;
                half3 normal : NORMAL;
            };

            struct vertex_output
            {
                float4 vertex : SV_POSITION;
                fixed2 uv1 : TEXCOORD0;
                float4 vertexLocal: TEXCOORD1;
                half3 normal : TEXCOORD2;
            };
            
            vertex_output vert (vertex_input v)
            {
                vertex_output o;

                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv1 = v.uv1;
                o.vertexLocal = v.vertex;
                o.normal = v.normal;

                return o;
            }
            
            fixed4 frag (vertex_output i) : SV_Target
            {   
                half3 normalWorld = getWorldSpaceNormal(i.normal);
                half3 viewDirection = getViewDirection(i.vertexLocal);

                fixed NdotL = saturate(dot(normalWorld, _LightDirection));
                fixed NdotV = saturate(dot(normalWorld, viewDirection));

                fixed3 s = SpecularLighting(_SpecularColor, _LightDirection, normalWorld, viewDirection, _SpecularShininess, _SpecularMultiplier);
                fixed3 r = RimLighting2(NdotL, viewDirection, normalWorld, _RimColor, _RimPower, _RimMultiplier, _RimPassThrough);

                fixed NdotLRemap = NdotL * 0.4 + 0.5;
                fixed NdotVRemap = NdotV * 0.8;

                fixed2 uv = fixed2(NdotVRemap, NdotLRemap);
                fixed3 brdf = tex2D(_BRDFTexture, uv).rgb;
                fixed3 tex = tex2D(_MainTexture, i.uv1).rgb;

                //fixed3 light = ConvertToGamma(brdf.rgb + s + r);
                fixed3 light = (brdf.rgb * _BRDFMultiplier + s + r + _AmbientColor);

                return fixed4(tex * _TintColor.rgb * light, 1.0);
            }
            ENDCG
        }
    }
}
