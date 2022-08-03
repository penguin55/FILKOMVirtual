Shader "Unlit/MixTexture"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _SecondTex("Second Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha

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
                float2 uv1 : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _SecondTex;
            float4 _SecondTex_ST;
            sampler2D _SplashTex;
            float4 _SplashTex_ST;
            float4 _ColorMain;
            float4 _ColorAdd;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv1 = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv2 = TRANSFORM_TEX(v.uv, _SecondTex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 mainCol = tex2D(_MainTex, i.uv1);
                half4 secondCol = tex2D(_SecondTex, i.uv2);
                half4 normalizeCol = secondCol.aaaa;
                
                half4 retCol = (mainCol * (1 - normalizeCol)) + (secondCol * normalizeCol);
                retCol.a = mainCol.a;

                return retCol;
            }
            ENDCG
        }
    }
}
