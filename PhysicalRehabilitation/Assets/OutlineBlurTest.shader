Shader "Custom/OutlineBlurTest"
{
Properties
{
	_MainTex("Texture", 2D) = "white" {}
	_Color("Main Color", Color) = (1,1,1,1)
	_BumpAmt("Distortion", Range(0,128)) = 10
	//_MainTex("Tint Color (RGB)", 2D) = "white" {}
	_BumpMap("Normalmap", 2D) = "bump" {}
	_Size("Size", Range(0, 20)) = 1

	_BlurTex("BlurTexture", 2D) = "white" {}
	_OutlineCol("OutlineColour", Color) = (0.0,0.0,1.0,1.0)
	[Toggle] _Solid("Solid Outline", Float) = 0
	_GradientStrengthModifier("Strength Modifier", Float) = 1.0
}

//Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Opaque" }

SubShader
	{
		Cull Off ZWrite Off ZTest Always

		/*GrabPass {
		Tags { "LightMode" = "Always" }
		}*/

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				//float2 uv : TEXCOORD0;
				float4 uvgrab : TEXCOORD0;
			};

			struct v2f
			{
				//float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;

				float4 uvgrab : TEXCOORD0;

			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uvgrab = v.uvgrab;

				#if UNITY_UV_STARTS_AT_TOP
				float scale = -1.0;
				#else
				float scale = 1.0;
				#endif
				o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y*scale) + o.vertex.w) * 0.5;
				o.uvgrab.zw = o.vertex.zw;

				return o;
			}

			///blur
			sampler2D _GrabTexture;
			float4 _GrabTexture_TexelSize;
			float _Size;

			//outline
			sampler2D _MainTex;

			struct fragOutput {
				half4 sum : COLOR;
				fixed4 col : SV_Target;
			};

			fragOutput frag(v2f i) : SV_Target{

				fragOutput test;

			test.sum = half4(0,0,0,0);
			#define GRABPIXEL(weight,kernelx) tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(float4(i.uvgrab.x + _GrabTexture_TexelSize.x * kernelx*_Size, i.uvgrab.y, i.uvgrab.z, i.uvgrab.w))) * weight
			test.sum += GRABPIXEL(0.05, -4.0);
			test.sum += GRABPIXEL(0.09, -3.0);
			test.sum += GRABPIXEL(0.12, -2.0);
			test.sum += GRABPIXEL(0.15, -1.0);
			test.sum += GRABPIXEL(0.18,  0.0);
			test.sum += GRABPIXEL(0.15, +1.0);
			test.sum += GRABPIXEL(0.12, +2.0);
			test.sum += GRABPIXEL(0.09, +3.0);
			test.sum += GRABPIXEL(0.05, +4.0);

			test.col = tex2D(_MainTex, i.uvgrab);
			//if (col.r == 1.0 && col.g == 0.0 && col.b == 1.0) {
				//col = (1.0, 0.0, 0.0, 1.0);
			//}
			//else {
				test.col.r = 0.0;
				test.col.g = 0.0;
				test.col.b = 1;;

				return test;
			}
			ENDCG
	}


		Pass
		{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					//float2 uv : TEXCOORD0;
					float4 uvgrab : TEXCOORD0;
					//float2 texcoord: TEXCOORD0;
				};

				struct v2f
				{
					//float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
					float4 uvgrab : TEXCOORD0;
				};

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uvgrab = v.uvgrab;

					#if UNITY_UV_STARTS_AT_TOP
					float scale = -1.0;
					#else
					float scale = 1.0;
					#endif
					o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y*scale) + o.vertex.w) * 0.5;
					o.uvgrab.zw = o.vertex.zw;
					return o;
				}

				struct fragOutput {
					half4 sum : COLOR;
					fixed4 col : SV_Target;

				};

				//blur

				sampler2D _GrabTexture;
				float4 _GrabTexture_TexelSize;
				float _Size;

				fragOutput frag(v2f i) : SV_Target{

					fragOutput test;

					half4 sum = half4(0,0,0,0);
					#define GRABPIXEL(weight,kernely) tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(float4(i.uvgrab.x, i.uvgrab.y + _GrabTexture_TexelSize.y * kernely*_Size, i.uvgrab.z, i.uvgrab.w))) * weight

					sum += GRABPIXEL(0.05, -4.0);
					sum += GRABPIXEL(0.09, -3.0);
					sum += GRABPIXEL(0.12, -2.0);
					sum += GRABPIXEL(0.15, -1.0);
					sum += GRABPIXEL(0.18,  0.0);
					sum += GRABPIXEL(0.15, +1.0);
					sum += GRABPIXEL(0.12, +2.0);
					sum += GRABPIXEL(0.09, +3.0);
					sum += GRABPIXEL(0.05, +4.0);

					//outline
					sampler2D _MainTex;
					sampler2D _BlurTex ;

					float _Solid;
					fixed4 _OutlineCol;
					float _GradientStrengthModifier = 1.0;

					fixed4 blurCol = tex2D(_BlurTex, i.uvgrab);
					fixed4 unBlurCol = tex2D(_MainTex, i.uvgrab);
					test.col = _OutlineCol;
					if (_Solid) {
						if (blurCol.r == 1.0) {
							test.col.a = 0.0;
						}
					}
					else {
						test.col.a *= 1.0 - blurCol.r;
					}

					test.col.a *= _GradientStrengthModifier;

					if (unBlurCol.r == 0.0 && unBlurCol.g == 0.0 && unBlurCol.b == 1.0) {
						test.col.a = -1;
					}
					return test;
				}
		ENDCG
		}


		//blur pass
		Pass{
				Tags { "LightMode" = "Always" }

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"

				struct appdata_t {
					float4 vertex : POSITION;
					float2 texcoord: TEXCOORD0;
				};

				struct v2f {
					float4 vertex : POSITION;
					float4 uvgrab : TEXCOORD0;
					float2 uvbump : TEXCOORD1;
					float2 uvmain : TEXCOORD2;
				};

				float _BumpAmt;
				float4 _BumpMap_ST;
				float4 _MainTex_ST;

				v2f vert(appdata_t v) {
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					#if UNITY_UV_STARTS_AT_TOP
					float scale = -1.0;
					#else
					float scale = 1.0;
					#endif
					o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y*scale) + o.vertex.w) * 0.5;
					o.uvgrab.zw = o.vertex.zw;
					o.uvbump = TRANSFORM_TEX(v.texcoord, _BumpMap);
					o.uvmain = TRANSFORM_TEX(v.texcoord, _MainTex);
					return o;
				}

				fixed4 _Color;
				sampler2D _GrabTexture;
				float4 _GrabTexture_TexelSize;
				sampler2D _BumpMap;
				sampler2D _MainTex;

				half4 frag(v2f i) : COLOR {

					half2 bump = UnpackNormal(tex2D(_BumpMap, i.uvbump)).rg;
					float2 offset = bump * _BumpAmt * _GrabTexture_TexelSize.xy;
					i.uvgrab.xy = offset * i.uvgrab.z + i.uvgrab.xy;

					half4 col = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.uvgrab));
					half4 tint = tex2D(_MainTex, i.uvmain) * _Color;

					return col * tint;
				}
		ENDCG
		}

	}
}