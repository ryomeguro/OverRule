Shader "CustomSkybox/Sun"
{
    Properties {
        _BGColor ("Sky Color", Color) = (0.7, 0.7, 1, 1)
        _GroundColor ("Ground Color", Color) = (0.7, 0.7, 1, 1)
        _SunColor ("SunColor", Color) = (1, 0.8, 0.5, 1)
        _SunDir ("Sun Direction", Vector) = (0, 0.5, 1, 0)
        _SunStrength("Sun Strengh", Range(0, 30)) = 12
        _GroundHeight("Ground Height", Range(-1,1)) = 0
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Background"
            "Queue"="Background"
            "PreviewType"="SkyBox"
        }

        Pass
        {
            ZWrite Off
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            fixed3 _BGColor;
            fixed3 _GroundColor;
            fixed3 _SunColor;
            float3 _SunDir;
            float _SunStrength;
            float _GroundHeight;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 texcoord : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 dir = normalize(_SunDir);
                float angle = dot(dir, i.texcoord);
                float width = 0.01;
                fixed3 col = lerp(_GroundColor, _BGColor, smoothstep(_GroundHeight - width, _GroundHeight + width, i.texcoord.y));
                fixed3 c = col + _SunColor * pow(max(0.0, angle), _SunStrength);
                return fixed4(c, 1.0);
            }
            ENDCG
        }
    }
}