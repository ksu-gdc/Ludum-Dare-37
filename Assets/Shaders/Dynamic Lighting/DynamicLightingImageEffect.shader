Shader "Hidden/DynamicLightingImageEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Alpha("2D Light Map Texture", 2D) = "white" {}
	}
		SubShader
		{
			//Tags{"Queue" = "AlphaTest" "IgnoreProjector" = "True" "RenderType" = "TransparentCutout"}
			// No culling or depth
			//Cull Off ZWrite Off ZTest Always

			//Blend SrcAlpha OneMinusSrcAlpha

			//LOD 100

			//Lighting Off

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
			sampler2D _Alpha;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;

				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				/*
				float4 disp = tex2D(_DisplaceTex, i.uv);
				disp.y = disp.y - 1;
				disp = ((disp * 2) - 1) * _Magnitude;
				*/

				float4 light = tex2D(_Alpha, i.uv);
				float4 main_col = tex2D(_MainTex, i.uv);
				float4 light_val = dot(light, light);

				//main_col = main_col + (main_col * light_val); //interesting inverted shadow effect
				main_col *= light_val;
				return main_col;
			}
			ENDCG
		}
	}
}
