Shader "Custom/PieceShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _MetallicTex("MetallicTex", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _NormalMap("NormalMap", 2D) = "bump"{}
        _EmissionTexture("EmissionTexture", 2D) = "black" {}
        [HDR] _EmissionColor("EmissionColor", Color) = (0,0,0)
        _RimColor("RimColor", Color) = (1,0,0,1)
        _RimPower("RimPower", Float) = 2
        [MaterialToggle] _UseRim ("UseRim", Float) = 0 
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _EmissionTexture;
        sampler2D _MetallicTex;
        sampler2D _NormalMap;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldNormal;
            float3 viewDir;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        fixed3 _EmissionColor;
        fixed4 _RimColor;
        float _RimPower;
        float _UseRim;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            fixed4 metalic = tex2D (_MetallicTex, IN.uv_MainTex);
            // Metallic and smoothness come from slider variables
            o.Metallic = metalic.r * _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
            
            fixed4 normTex = tex2D(_NormalMap, IN.uv_MainTex);
            o.Normal = UnpackNormal(normTex);
            
            o.Emission += tex2D (_EmissionTexture, IN.uv_MainTex) * _EmissionColor;
            
            float rim = 1 - saturate(dot(IN.viewDir, o.Normal));
            o.Emission += _RimColor * pow(rim, _RimPower) * _UseRim * (sin(_Time.z * 3) + 1) / 2;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
