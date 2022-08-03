Shader "Unlit/ColoredTexture"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _Color("Main Color", Color) = (0,0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off

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
            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 mainCol = _Color; 
                half4 secondCol = tex2D(_MainTex, i.uv);
                half4 normalizeCol = secondCol.aaaa;
                
                half4 retCol = (mainCol * (1 - normalizeCol)) + (secondCol * normalizeCol);
                retCol.a = mainCol.a;

                return retCol;
            }
            ENDCG
        }
    }
}
