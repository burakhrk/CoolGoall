Shader "Unlit/GradientBar"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _StartColor ("Start Color", Color) = (0,1,0,1) // Yeşil
        _EndColor ("End Color", Color) = (1,0,0,1)   // Kırmızı
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
            float4 _MainTex_ST;
            fixed4 _StartColor;
            fixed4 _EndColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // UV koordinatını kullanarak gradyanı hesapla (x ekseni boyunca)
                col.rgb = lerp(_StartColor.rgb, _EndColor.rgb, i.uv.x);
                return col;
            }
            ENDCG
        }
    }
}