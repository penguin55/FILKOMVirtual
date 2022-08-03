Shader "Unlit/TransparentColor"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Base Color", Color) = (1,1,1,1)
        _Edge("Edge", Range(0.0, 1.0)) = 0.5
        _Smooth("Smooth", Range(0.0, 1.0)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent+3" }
        LOD 100
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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _Edge;
            float _Smooth;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                half4 texCol = tex2D(_MainTex, i.uv);
                float alphaStep = 0;
                alphaStep = smoothstep((i.uv.y - _Smooth), (i.uv.y + _Smooth), _Edge);
                half4 baseColor = half4(_Color.rgb, alphaStep * _Color.a);

                half4 maskColor = texCol.aaaa;
                half4 returnColor = (baseColor * (1 - maskColor)) + (texCol * maskColor);

                return returnColor;
            }
            ENDCG
        }
    }
}
