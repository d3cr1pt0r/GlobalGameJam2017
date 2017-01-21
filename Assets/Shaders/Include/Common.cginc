// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

#if UNITY_VERSION >= 540
    #define O7_ObjectToWorld unity_ObjectToWorld
#else
    #define O7_ObjectToWorld unity_ObjectToWorld
#endif

#define ConvertToGamma(_v_) pow(_v_, 0.454545);

fixed3 getWorldSpaceNormal(half3 n) {
    return normalize(mul((float3x3)O7_ObjectToWorld, n));
}

float3 getWorldSpaceVertex(float3 v) {
    return mul(O7_ObjectToWorld, fixed4(v.xyz, 1.0)).xyz;
}

fixed3 getViewDirection(float3 v) {
    return normalize(_WorldSpaceCameraPos.xyz - getWorldSpaceVertex(v));
}