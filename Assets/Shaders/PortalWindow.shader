Shader "Custom/PortalWindow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [Enum(Equal,3,NotEqual,6)] _StencilFilter ("StencilFilter", int) = 6
    }
    
    SubShader
    {
        ZWrite off
        ColorMask 0
        Cull off
        
        Tags { "RenderType"="Opaque" }
                     LOD 200
        
        Stencil
        {
            Ref 1
            Pass replace 
        }
        
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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
