Shader "Unlit/NormalMap"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Scale("Scale", Range(0.0, 1.0)) = 0.1
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

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _Scale;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 d = _MainTex_TexelSize.xy;
                float h1 = tex2D(_MainTex, i.uv + float2(-d.x, -d.y)).r;
                float h2 = tex2D(_MainTex, i.uv + float2(0.0, -d.y)).r;
                float h3 = tex2D(_MainTex, i.uv + float2(d.x, -d.y)).r;
                float h4 = tex2D(_MainTex, i.uv + float2(-d.x, 0.0)).r;

                float h6 = tex2D(_MainTex, i.uv + float2(d.x, 0.0)).r;
                float h7 = tex2D(_MainTex, i.uv + float2(-d.x, d.y)).r;
                float h8 = tex2D(_MainTex, i.uv + float2(0.0, d.y)).r;
                float h9 = tex2D(_MainTex, i.uv + float2(d.x, d.y)).r;

                float x = -(h3 - h1 + 2.0 * (h6 - h4) + h9 - h7);
                float y = -(h7 - h1 + 2.0 * (h8 - h2) + h9 - h3);
                float3 n = normalize(float3(x, y, _Scale));

                float4 col = float4(n, 1.0);
                return col;
            }
            ENDCG
        }
    }
}
