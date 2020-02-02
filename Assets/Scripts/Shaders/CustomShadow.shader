Shader "Custom/EnvironmentShadow"
{
    Properties {
        [NoScaleOffset] _MainTex ("Main Texture", 2D) = "white" {}
        _Offset ("Offset", Float) = 0.2
        [MaterialToggle] _HideShadows("Hide in Shadow", Float) = 0
        _Color ("Main Color", Color) = (1,1,1,1)
        _ShadowColor("Shadow Color", Color) = (0, 0, 0, 0)
    }

    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdadd_fullshadows
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;

            v2f vert(appdata i)
            {
                v2f o;
                float4 light_position = mul(unity_WorldToObject, _WorldSpaceLightPos0);
                light_position.z = 0;
                light_position.w = 0;
                float4 light_direction = normalize(i.vertex - light_position);
                if (i.vertex.y < 0)
                {
                    o.vertex = UnityObjectToClipPos(i.vertex);
                }
                else
                {
                    o.vertex = UnityObjectToClipPos(i.vertex - fixed4(-light_direction.x/10, 0, 0, 0));
                }

                o.uv = i.uv;
                return o;
            }

            fixed4 frag(v2f i) : COLOR
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                if (col.a == 0) discard;
                return col * 0.1;
            }


            ENDCG
        }

        Pass
        {
            Lighting On
            Tags {"LightMode"="ForwardAdd"}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdadd_fullshadows
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"

            sampler2D _MainTex;
            fixed4 _Color;
            fixed _HideShadows;

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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                TRANSFER_VERTEX_TO_FRAGMENT(o);
                return o;
            }

            fixed4 frag(v2f i) : SV_TARGET
            {
                float light_attenuation = LIGHT_ATTENUATION(i);
                fixed shadow = SHADOW_ATTENUATION(i);
                fixed4 color = tex2D(_MainTex, i.uv) * normalize(_LightColor0) * _Color;

                if (shadow < 0.6) {
                    if (color.a == 0) discard;
                    else return 0;
                }
                else if (color.a == 0) {
                    discard;
                }
                return color;
            }

            ENDCG
        }

    }
}