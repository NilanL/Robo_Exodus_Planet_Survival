Shader "Custom/FogOfWarShader" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _FogOfWarTex("Fog of War Texture", 2D) = "white" {}
        _FogColor("Fog Color", Color) = (0.5, 0.5, 0.5, 1)
        _FogStart("Fog Start Distance", Range(0.0, 100.0)) = 10.0
        _FogEnd("Fog End Distance", Range(0.0, 1000.0)) = 100.0
    }

        SubShader{
            Tags {"Queue" = "Transparent" "RenderType" = "Opaque"}
            LOD 100

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    UNITY_FOG_COORDS(1)
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                sampler2D _FogOfWarTex;
                float4 _FogColor;
                float _FogStart;
                float _FogEnd;

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    UNITY_TRANSFER_FOG(o,o.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    // Sample the main texture
                    fixed4 col = tex2D(_MainTex, i.uv);

                // Sample the fog of war texture
                fixed4 fog = tex2D(_FogOfWarTex, i.uv);

                // Calculate the fog distance
                float fogDistance = (1.0 - fog.r) * (_FogEnd - _FogStart) + _FogStart;

                // Calculate the fog factor
                float fogFactor = saturate((i.vertex.z - _FogStart) / (fogDistance - _FogStart));

                // Apply the fog color
                col.rgb = lerp(col.rgb, _FogColor.rgb, fogFactor);

                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
        }
            FallBack "Diffuse"
}