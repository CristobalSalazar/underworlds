Shader "Unlit/Ground"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Pass
        {
            Lighting On
            Tags { "LightMode"="ForwardAdd" }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdadd_fullshadows

            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                LIGHTING_COORDS(1, 2)
            };

            sampler2D _MainTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                TRANSFER_VERTEX_TO_FRAGMENT(o);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float light = LIGHT_ATTENUATION(i);
                fixed shadow = SHADOW_ATTENUATION(i);
                fixed4 col = tex2D(_MainTex, i.uv) * light * _LightColor0 * shadow;
                col += (abs(sin(_Time.y * 5))/200 * ceil(_LightColor0));
                return col;
            }
            ENDCG
        }
    }
}
