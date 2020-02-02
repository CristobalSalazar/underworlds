Shader "Custom/Screen"
{
    Properties
    {
        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
        _Contrast("Luminance", float ) = 0.5
        _Lerp("Lerp", float) = 0.1
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            fixed _Contrast;
            fixed _Lerp;
            float rand(float val)
            {
                return frac(sin(dot(val,float2(12.9898,78.233)))*43758.5453123);
            }

            fixed4 frag (v2f i) : SV_Target
            {

                fixed4 col = tex2D(_MainTex, i.uv);
                fixed brightness = Luminance(col.rgb);
                col += brightness * _Contrast;
                return col;
            }
            ENDCG
        }
    }
}
