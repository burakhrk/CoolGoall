Shader "Unlit/GradientBar"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _StartColor ("Start Color", Color) = (1,0,0,1) // Kırmızı
        _MiddleColor ("Middle Color", Color) = (0,1,0,1) // Yeşil
        _EndColor ("End Color", Color) = (1,0,0,1) // Kırmızı
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
            fixed4 _MiddleColor;
            fixed4 _EndColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                // UV koordinatlarını eğimli hale getirmek için kaydırma uygula
                float curveAmount = 0.2; // Eğriliğin miktarı
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv.y += sin(o.uv.x * 3.14159) * curveAmount; // X koordinatına göre eğim uygula

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                // UV koordinatını kullanarak gradyanı hesapla (x ekseni boyunca)
                float midStart = 0.4; // Yeşil alan başlangıcı
                float midEnd = 0.6;   // Yeşil alan bitişi

                if (i.uv.x < midStart)
                {
                    col.rgb = lerp(_StartColor.rgb, _MiddleColor.rgb, i.uv.x / midStart);
                }
                else if (i.uv.x > midEnd)
                {
                    col.rgb = lerp(_MiddleColor.rgb, _EndColor.rgb, (i.uv.x - midEnd) / (1.0 - midEnd));
                }
                else
                {
                    col.rgb = _MiddleColor.rgb;
                }

                // Her 20 birimde bir siyah çizgi ekle
                float lineInterval = 0.2; // 1/5 = 0.2
                if (frac(i.uv.x / lineInterval) < 0.005) // Daha ince siyah çizgi
                {
                    col.rgb = 0;
                }

                return col;
            }
            ENDCG
        }
    }
}
