Shader "Unlit/VertexAnimTex"
{
    Properties
    {
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
                uint vid : SV_VertexID;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : TEXCOORD1;
            };

            sampler1D _VertexAnimTex;
            half4 _VertexAnimTex_TexelSize;
            int _FrameCount;

            v2f vert (appdata v)
            {
                v2f o;
                float4 uv;
                uv.x = (_FrameCount * 4 + v.vid) * _VertexAnimTex_TexelSize.x;
                uv.yzw = 0.0;
                v.vertex.xy = tex1Dlod(_VertexAnimTex, uv).xy;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = float4(1.0, 0.0, 0.0, 1.0);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 col = i.color;
                return col;
            }
            ENDCG
        }
    }
}
