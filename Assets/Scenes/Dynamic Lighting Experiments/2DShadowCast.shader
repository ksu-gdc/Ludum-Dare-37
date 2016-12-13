Shader "Hidden/CastingShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Intensity("Light Intensity", Float) = 0
		_MarchDist("March Step Distance", Float) = 0.01
		_MaxIterations("Max Number of Times to March", Int) = 1
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
				float4 lightPos : TEXCOORD1;
				float3 coordinate : TEXCOORD2;
			};

			float4 _MainTex_TexelSize;

			float3 _WorldSpaceLightPos;
			sampler2D _MainTex;
			float _Intensity;
			float _AspectRatio;
			float _MarchDist;
			int _MaxIterations;
			static const float4 _white = float4(1, 1, 1, 0);
			static const float4 _black = float4(0, 0, 0, 0);

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;

				o.coordinate = float4(_WorldSpaceLightPos, 0);
				o.lightPos = ComputeScreenPos(UnityObjectToClipPos(o.coordinate));

				#if UNITY_UV_STARTS_AT_TOP
				if (_MainTex_TexelSize.y < 0) {
					o.uv.y = 1 - o.uv.y;
				}
				#endif

				return o;
			}

			bool is_color(float4 val1, float4 val2) {
				return length(val1 - val2) == 0.0;
			}

			bool is_not_color(float4 val1, float4 val2) {
				return length(val1 - val2) != 0.0;
			}

			float4 march(v2f i, float2 aspect, float2 src, float2 dst, float step_dist) {
				float4 frag_col, tex_col;
				frag_col = tex_col = tex2D(_MainTex, i.uv);

				float2 step = normalize(dst - src) * step_dist;
				float dst_dist = distance(dst, src);
				int steps = (dst_dist / step) + 1;

				uint bounds = (uint)_MaxIterations % 1023;
				for (uint x = 0; x < bounds; x++) {
					uint iter = min(x, steps);
					float2 calc_pos = (src + (iter * step));
					float calc_dist = distance(src, calc_pos);
					frag_col *= (dst_dist - calc_dist >= 0.0) ? tex2D(_MainTex, calc_pos) : _white;
				}

				float4 result = tex_col;

				result = (is_not_color(result, _white)) ? _white : frag_col;
				return result;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float2  lightUV = i.lightPos.xy / i.lightPos.w;
				float4 tex_col = tex2D(_MainTex, i.uv);

				float2 aspect = float2(1, _AspectRatio);
				float dist = distance(aspect * lightUV, aspect * i.uv);
				float4 col = march(i, aspect, i.uv, lightUV, _MarchDist);

				col = (dist >= _Intensity) ? _black : col * lerp(_white, _black, dist / _Intensity);
				return col;
			}
			ENDCG
		}
	}
}
