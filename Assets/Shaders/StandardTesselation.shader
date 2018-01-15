// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/StandardTesselation" {
	Properties {
		_Tess ("Tessellation", Range(1,32)) = 4
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_NormalMap ("Normalmap", 2D) = "bump" {}
		_Displacement ("Displacement", Range(0, 1.0)) = 0.3
		_Color ("Color", color) = (1,1,1,0)
		_SpecColor ("Spec color", color) = (0.5,0.5,0.5,0.5)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 300
		
		CGPROGRAM
		#pragma surface surf BlinnPhong addshadow fullforwardshadows vertex:disp tessellate:tessFixed nolightmap
		#pragma target 4.6

		struct appdata {
			float4 vertex : POSITION;
			float4 tangent : TANGENT;
			float3 normal : NORMAL;
			float2 texcoord : TEXCOORD0;
		};

		float _Tess;

		float4 tessFixed()
		{
			return _Tess;
		}

		float _Displacement;

		float hash( float n )
		{
			return frac(sin(n)*43758.5453);
		}

		float noise( float3 x )
			{
				// The noise function returns a value in the range -1.0f -> 1.0f

				float3 p = floor(x);
				float3 f = frac(x);

				f       = f*f*(3.0-2.0*f);
				float n = p.x + p.y*57.0 + 113.0*p.z;

				return lerp(lerp(lerp( hash(n+0.0), hash(n+1.0),f.x),
					   lerp( hash(n+57.0), hash(n+58.0),f.x),f.y),
					   lerp(lerp( hash(n+113.0), hash(n+114.0),f.x),
					   lerp( hash(n+170.0), hash(n+171.0),f.x),f.y),f.z);
			}

		void disp (inout appdata v)
		{
			float d = noise(mul(unity_ObjectToWorld, v.vertex).xyz) * _Displacement - _Displacement / 2; 
			if (v.normal.y == 1)
				v.vertex.y += d;
			else if (v.normal.y == -1)
				v.vertex.y += d;
			else if (v.normal.x == -1)
				v.vertex.y += d;
			else if (v.normal.x == 1)
				v.vertex.y += d;
			else if (v.normal.z != 0)
				v.vertex.y += d;
		}

		struct Input {
			float2 uv_MainTex;
		};

		sampler2D _MainTex;
		sampler2D _NormalMap;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Specular = 0.2;
			o.Gloss = 1.0;
			o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_MainTex));
		}
		ENDCG
	}
	FallBack "Diffuse"
}

