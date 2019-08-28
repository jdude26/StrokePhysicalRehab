Shader "Custom/OutlineBlurTest"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Main Color", Color) = (1,1,1,1)
		_BumpAmt("Distortion", Range(0,128)) = 10
		_MainTex("Tint Color (RGB)", 2D) = "white" {}
		_BumpMap("Normalmap", 2D) = "bump" {}
		_Size("Size", Range(0, 20)) = 1
	}
		
	//Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Opaque" }

	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		//unity tutorials say this isnt ever read?
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
			float2 uv : TEXCOORD0;
		};

		struct v2f
		{
			float2 uv : TEXCOORD0;
			float4 vertex : SV_POSITION;

			float4 uvgrab : TEXCOORD0;

		};

		v2f vert(appdata v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			o.uv = v.uv;

			//addition of this section breaks something, posssibly uvgrab
			#if UNITY_UV_STARTS_AT_TOP
			float scale = -1.0;
			#else
			float scale = 1.0;
			#endif
			o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y*scale) + o.vertex.w) * 0.5;
			o.uvgrab.zw = o.vertex.zw;
			return o;
		}

		sampler2D _MainTex;

		fixed4 frag(v2f i) : SV_Target
		{
			fixed4 col = tex2D(_MainTex, i.uv);
		if (col.r == 1.0 && col.g == 0.0 && col.b == 1.0) {
			//col = (1.0, 0.0, 0.0, 1.0);
		}
		else {
			col.r = 0.0;
			col.g = 0.0;
			col.b = 1;
		}
		return col;
		}
			ENDCG
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

		v2f vert(appdata v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			o.uv = v.uv;
			return o;
		}

		sampler2D _MainTex;
		sampler2D _BlurTex;
		float _Solid;
		fixed4 _OutlineCol;
		float _GradientStrengthModifier;
		fixed4 frag(v2f i) : SV_Target
		{
			fixed4 col;
		fixed4 blurCol = tex2D(_BlurTex, i.uv);
		fixed4 unBlurCol = tex2D(_MainTex, i.uv);
		col = _OutlineCol;
		if (_Solid) {
			if (blurCol.r == 1.0) {
				col.a = 0.0;
			}
		}
		else {
			col.a *= 1.0 - blurCol.r;
		}

		col.a *= _GradientStrengthModifier;

		if (unBlurCol.r == 0.0 && unBlurCol.g == 0.0 && unBlurCol.b == 1.0) {
			col.a = -1;
		}
		return col;
		}
			ENDCG
		}
	
	}
}