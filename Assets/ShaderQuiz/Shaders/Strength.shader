Shader "Unlit/Strength"
{
    Properties
    {
        _Pow("power", Range(-5.0, 5.0)) = 4.0
        _Intensity("intensity", Range(-10.0, 10.0)) = 10.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Packages/jp.keijiro.noiseshader/Shader/SimplexNoise2D.hlsl"

            #define RED float4(1.0, 0.0, 0.0, 1.0)
            #define BLUE float4(0.0, 0.0, 1.0, 1.0)

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _Pow;
            float _Intensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float c = SimplexNoise(i.uv * 10.0);
                c = pow(c, _Pow) * _Intensity;
                float4 col = float4(c, c, c, 1.0);
                col = lerp(RED, BLUE, col);
                return col;
            }
            ENDCG
        }
    }
}
