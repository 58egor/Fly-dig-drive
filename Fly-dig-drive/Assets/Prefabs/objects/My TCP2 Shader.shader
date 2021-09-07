// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Toony Colors Pro+Mobile 2
// (c) 2014-2021 Jean Moreno


Shader "Toony Colors Pro 2/User/My TCP2 Shader"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo", 2D) = "white" {}

		_Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5

		_Glossiness("Smoothness", Range(0.0, 1.0)) = 0.5
		_GlossMapScale("Smoothness Factor", Range(0.0, 1.0)) = 1.0

		_SpecColor("Specular", Color) = (0.2,0.2,0.2)

		// [ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
		// [ToggleOff] _GlossyReflections("Glossy Reflections", Float) = 1.0


		// Blending state
		[HideInInspector] _Mode("__mode", Float) = 0.0
		[HideInInspector] _SrcBlend("__src", Float) = 1.0
		[HideInInspector] _DstBlend("__dst", Float) = 0.0
		[HideInInspector] _ZWrite("__zw", Float) = 1.0

		//TOONY COLORS PRO 2 ----------------------------------------------------------------
		_HColor("Highlight Color", Color) = (0.785,0.785,0.785,1.0)
		_SColor("Shadow Color", Color) = (0.195,0.195,0.195,1.0)

	[Header(Ramp Shading)]
		_RampThreshold("Threshold", Range(0,1)) = 0.5
		_RampSmooth("Main Light Smoothing", Range(0,1)) = 0.2
		_RampSmoothAdd("Other Lights Smoothing", Range(0,1)) = 0.75

	[Header(Threshold Texture)]
		[NoScaleOffset]
		_ThresholdTex ("Texture (Alpha)", 2D) = "gray" {}
		_ThresholdStrength ("Strength", Range(0,1)) = 1
		_ThresholdScale ("Scale", Float) = 4

	[Header(Sketch)]
		//SKETCH
		_SketchTex ("Sketch (Alpha)", 2D) = "white" {}
		_SketchSpeed ("Sketch Anim Speed", Range(1.1, 10)) = 6
		_SketchStrength ("Sketch Strength", Range(0,1)) = 1

		//Avoid compile error if the properties are ending with a drawer
		[HideInInspector] __dummy__ ("__unused__", Float) = 0
	}

	SubShader
	{
		Blend [_SrcBlend] [_DstBlend]
		ZWrite [_ZWrite]
		Tags { "RenderType"="Opaque" "PerformanceChecks"="False" }

		CGPROGRAM

		#pragma surface surf StandardTCP2 noshadow vertex:vert keepalpha exclude_path:deferred exclude_path:prepass
		#pragma target 3.0

		#pragma shader_feature _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON

		//================================================================================================================================
		// STRUCTS

		struct Input
		{
			float2 uv_MainTex;
			#define uv_TexturedThreshold uv_MainTex
			float4 screenCoordsCustom;
		};

		//================================================================================================================================
		// VERTEX FUNCTION

		//Vertex input
		struct appdata_tcp2
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float4 texcoord : TEXCOORD0;
			float4 texcoord1 : TEXCOORD1;
			float4 texcoord2 : TEXCOORD2;
			float4 tangent : TANGENT;
		#if defined(LIGHTMAP_ON) && defined(DIRLIGHTMAP_COMBINED)
			float4 tangent : TANGENT;
		#endif
			UNITY_VERTEX_INPUT_INSTANCE_ID
		};
	
		//Vertex function
		void vert (inout appdata_tcp2 v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
			
			float4 pos = UnityObjectToClipPos(v.vertex);
			float4 screenCoords = ComputeScreenPos(pos);
			o.screenCoordsCustom = screenCoords;
			
			//Sketch
		}

		//================================================================================================================================
		// LIGHTING FUNCTION

		inline half4 LightingStandardTCP2(SurfaceOutputStandardTCP2 s, half3 viewDir, UnityGI gi)
		{
			s.Normal = normalize(s.Normal);

			// energy conservation
			half oneMinusReflectivity;
			s.Albedo = EnergyConservationBetweenDiffuseAndSpecular (s.Albedo, s.Specular, /*out*/ oneMinusReflectivity);

			// shader relies on pre-multiply alpha-blend (_SrcBlend = One, _DstBlend = OneMinusSrcAlpha)
			// this is necessary to handle transparency in physically correct way - only diffuse component gets affected by alpha
			half outputAlpha;
			s.Albedo = PreMultiplyAlpha(s.Albedo, s.Alpha, oneMinusReflectivity, /*out*/ outputAlpha);

		#if defined(UNITY_PASS_FORWARDBASE)
			fixed atten = s.atten;
		#else
			fixed atten = 1;
		#endif

			half4 c = TCP2_BRDF_PBS(s.Albedo, s.Specular, oneMinusReflectivity, s.Smoothness, s.Normal, viewDir, gi.light, gi.indirect, /* TCP2 */ atten, s

				,s.texThresholdTexcoords
				,s.screenCoords
				);
			c.a = outputAlpha;
			return c;
		}

		inline void LightingStandardTCP2_GI(inout SurfaceOutputStandardTCP2 s, UnityGIInput data, inout UnityGI gi)
		{
			Unity_GlossyEnvironmentData g = UnityGlossyEnvironmentSetup(s.Smoothness, data.worldViewDir, s.Normal, s.Specular);
			gi = UnityGlobalIllumination(data, s.Occlusion, s.Normal, g);

			s.atten = data.atten;				//transfer attenuation to lighting function
			gi.light.color = _LightColor0.rgb;	//remove attenuation
		}

		//================================================================================================================================
		// SURFACE FUNCTION

		void surf (Input IN, inout SurfaceOutputStandardTCP2 o)
		{

			fixed4 mainTex = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = mainTex.rgb;
			o.Alpha = mainTex.a;

		#if _ALPHATEST_ON
			clip(o.Alpha - _Cutoff);
		#endif

			//Specular workflow
			fixed4 specGlossMap = fixed4(0,0,0,0);
			half4 specGloss = SpecularGloss(mainTex.a, specGlossMap);
			half3 specColor = specGloss.rgb;
			half smoothness = specGloss.a;
			o.Specular = specColor;
			o.Smoothness = smoothness;


			o.texThresholdTexcoords = IN.uv_TexturedThreshold;
			o.screenCoords = IN.screenCoordsCustom;
		#ifdef _ALPHABLEND_ON
			o.Albedo *= o.Alpha;
		#endif
		}
		ENDCG

	}

	CGINCLUDE

	#if !defined(EXCLUDE_TCP2_MAIN_PASS)
		#include "Lighting.cginc"

		//================================================================================================================================
		// STRUCT

		struct SurfaceOutputStandardTCP2
		{
			fixed3 Albedo;      // base (diffuse or specular) color
			fixed3 Normal;      // tangent space normal, if written
			half3 Emission;

			fixed3 Specular;    // specular color

			//Smoothness is the user facing name, it should be perceptual smoothness but user should not have to deal with it.
			// Everywhere in the code you meet smoothness it is perceptual smoothness
			half Smoothness;    // 0=rough, 1=smooth
			half Occlusion;     // occlusion (default 1)
			fixed Alpha;        // alpha for transparencies

			fixed atten;
			float2 texThresholdTexcoords;
			float4 screenCoords;
		};

		//================================================================================================================================
		// VARIABLES

		sampler2D _MainTex;
		fixed4 _Color;
		half _Cutoff;
		half _Glossiness;
		half _GlossMapScale;

		//-------------------------------------------------------------------------------------
		//TCP2 Params

		fixed4 _HColor;
		fixed4 _SColor;
		sampler2D _Ramp;
		fixed _RampThreshold;
		fixed _RampSmooth;
		fixed _RampSmoothAdd;
		sampler2D _ThresholdTex;
		fixed _ThresholdScale;
		fixed _ThresholdStrength;
		sampler2D _SketchTex;
		float4 _SketchTex_ST;
		fixed _SketchStrength;
		fixed _SketchSpeed;

		//================================================================================================================================
		// LIGHTING / BRDF

		//-------------------------------------------------------------------------------------
		// TCP2 Tools

		inline half WrapRampNL(half nl, fixed threshold, fixed smoothness)
		{
			nl = saturate(nl);
			nl = smoothstep(threshold - smoothness*0.5, threshold + smoothness*0.5, nl);
			return nl;
		}

		//Adjust screen UVs relative to object to prevent screen door effect
		inline void ObjSpaceUVOffset(inout float2 screenUV, in float screenRatio)
		{
			// UNITY_MATRIX_P._m11 = Camera FOV
			float4 objPos = float4(-UNITY_MATRIX_T_MV[3].x * screenRatio * UNITY_MATRIX_P._m11, -UNITY_MATRIX_T_MV[3].y * UNITY_MATRIX_P._m11, UNITY_MATRIX_T_MV[3].z, UNITY_MATRIX_T_MV[3].w);

			float offsetFactorX = 0.5;
			float offsetFactorY = offsetFactorX * screenRatio;
			offsetFactorX *= _SketchTex_ST.x;
			offsetFactorY *= _SketchTex_ST.y;

			if (unity_OrthoParams.w < 1)	//don't scale with orthographic camera
			{
				//adjust uv scale
				screenUV -= float2(offsetFactorX, offsetFactorY);
				screenUV *= objPos.z;	//scale with cam distance
				screenUV += float2(offsetFactorX, offsetFactorY);

				// sign(UNITY_MATRIX_P[1].y) is different in Scene and Game views
				screenUV.x -= objPos.x * offsetFactorX * sign(UNITY_MATRIX_P[1].y);
				screenUV.y -= objPos.y * offsetFactorY * sign(UNITY_MATRIX_P[1].y);
			}
			else
			{
				// sign(UNITY_MATRIX_P[1].y) is different in Scene and Game views
				screenUV.x += objPos.x * offsetFactorX * sign(UNITY_MATRIX_P[1].y);
				screenUV.y += objPos.y * offsetFactorY * sign(UNITY_MATRIX_P[1].y);
			}
		}

		//-------------------------------------------------------------------------------------
		// Standard Shader inputs


		half4 SpecularGloss(float mainTexAlpha, fixed4 specGlossMap)
		{
			half4 sg;
			sg.rgb = _SpecColor.rgb;
			sg.a = mainTexAlpha * _Glossiness;
			return sg;
		}

		//-------------------------------------------------------------------------------------

		// Note: BRDF entry points use oneMinusRoughness (aka "smoothness") and oneMinusReflectivity for optimization
		// purposes, mostly for DX9 SM2.0 level. Most of the math is being done on these (1-x) values, and that saves
		// a few precious ALU slots.

		// Main Physically Based BRDF
		// Derived from Disney work and based on Torrance-Sparrow micro-facet model
		//
		//   BRDF = kD / pi + kS * (D * V * F) / 4
		//   I = BRDF * NdotL
		//
		// * NDF (depending on UNITY_BRDF_GGX):
		//  a) Normalized BlinnPhong
		//  b) GGX
		// * Smith for Visiblity term
		// * Schlick approximation for Fresnel
		half4 TCP2_BRDF_PBS(half3 diffColor, half3 specColor, half oneMinusReflectivity, half smoothness, half3 normal, half3 viewDir, UnityLight light, UnityIndirect gi,
			/* TCP2 */ half atten, SurfaceOutputStandardTCP2 s
			,half2 texThresholdTexcoords
			,half4 screenCoords
			)
		{
			half perceptualRoughness = SmoothnessToPerceptualRoughness (smoothness);
			half3 halfDir = Unity_SafeNormalize (light.dir + viewDir);

			// NdotV should not be negative for visible pixels, but it can happen due to perspective projection and normal mapping
			// In this case normal should be modified to become valid (i.e facing camera) and not cause weird artifacts.
			// but this operation adds few ALU and users may not want it. Alternative is to simply take the abs of NdotV (less correct but works too).
			// Following define allow to control this. Set it to 0 if ALU is critical on your platform.
			// This correction is interesting for GGX with SmithJoint visibility function because artifacts are more visible in this case due to highlight edge of rough surface
			// Edit: Disable this code by default for now as it is not compatible with two sided lighting used in SpeedTree.
			#define TCP2_HANDLE_CORRECTLY_NEGATIVE_NDOTV 0 

	#if TCP2_HANDLE_CORRECTLY_NEGATIVE_NDOTV
			// The amount we shift the normal toward the view vector is defined by the dot product.
			half shiftAmount = dot(normal, viewDir);
			normal = shiftAmount < 0.0f ? normal + viewDir * (-shiftAmount + 1e-5f) : normal;
			// A re-normalization should be applied here but as the shift is small we don't do it to save ALU.
			//normal = normalize(normal);

			half nv = saturate(dot(normal, viewDir)); // TODO: this saturate should no be necessary here
	#else
			half nv = abs(dot(normal, viewDir));	// This abs allow to limit artifact
	#endif

			half nl = dot(normal, light.dir);

		#if !defined(UNITY_PASS_FORWARDADD)
	
			half2 thresholdUv = texThresholdTexcoords.xy * _ThresholdScale;
			half texThreshold = tex2D(_ThresholdTex, thresholdUv).a - 0.5;
			nl += texThreshold * _ThresholdStrength;
		#endif

		#if defined(UNITY_PASS_FORWARDADD)
			#define RAMP_SMOOTH _RampSmoothAdd
		#else
			#define RAMP_SMOOTH _RampSmooth
		#endif

			//TCP2 Ramp N.L
			nl = WrapRampNL(nl, _RampThreshold, RAMP_SMOOTH);

			half nh = saturate(dot(normal, halfDir));

			half lv = saturate(dot(light.dir, viewDir));
			half lh = saturate(dot(light.dir, halfDir));

			// Diffuse term
			half diffuseTerm = DisneyDiffuse(nv, nl, lh, perceptualRoughness) * nl;

			// Specular term
			// HACK: theoretically we should divide diffuseTerm by Pi and not multiply specularTerm!
			// BUT 1) that will make shader look significantly darker than Legacy ones
			// and 2) on engine side "Non-important" lights have to be divided by Pi too in cases when they are injected into ambient SH
			half roughness = PerceptualRoughnessToRoughness(perceptualRoughness);
	#if UNITY_BRDF_GGX
			half V = SmithJointGGXVisibilityTerm (nl, nv, roughness);
			half D = GGXTerm (nh, roughness);
	#else
			// Legacy
			half V = SmithBeckmannVisibilityTerm (nl, nv, roughness);
			half D = NDFBlinnPhongNormalizedTerm (nh, PerceptualRoughnessToSpecPower(perceptualRoughness));
	#endif
			half specularTerm = V*D * UNITY_PI; // Torrance-Sparrow model, Fresnel is applied later
	#ifdef UNITY_COLORSPACE_GAMMA
			specularTerm = sqrt(max(1e-4h, specularTerm));
	#endif
			// specularTerm * nl can be NaN on Metal in some cases, use max() to make sure it's a sane value
			specularTerm = max(0, specularTerm * nl);

			// surfaceReduction = Int D(NdotH) * NdotH * Id(NdotL>0) dH = 1/(roughness^2+1)
			half surfaceReduction;
	#ifdef UNITY_COLORSPACE_GAMMA
			surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;		// 1-0.28*x^3 as approximation for (1/(x^4+1))^(1/2.2) on the domain [0;1]
	#else
			surfaceReduction = 1.0 / (roughness*roughness + 1.0);			// fade \in [0.5;1]
	#endif

			// To provide true Lambert lighting, we need to be able to kill specular completely.
			specularTerm *= any(specColor) ? 1.0 : 0.0;

	//TCP2 Colored Highlight/Shadows
			_SColor = lerp(_HColor, _SColor, _SColor.a);	//Shadows intensity through alpha

	//light attenuation already included in light.color for point lights
	#if !defined(UNITY_PASS_FORWARDADD)
			diffuseTerm *= atten;
	#endif
			half3 diffuseTermRGB = lerp(_SColor.rgb, _HColor.rgb, diffuseTerm);
			half3 diffuseTCP2 = diffColor * (gi.diffuse + light.color * diffuseTermRGB);
			//original: diffColor * (gi.diffuse + light.color * diffuseTerm)

	//light attenuation already included in light.color for point lights
	#if !defined(UNITY_PASS_FORWARDADD)
			//TCP2: atten contribution to specular since it was removed from light calculation
			specularTerm *= atten;
	#endif

			half grazingTerm = saturate(smoothness + (1-oneMinusReflectivity));
			half3 color =	diffuseTCP2
							+ specularTerm * light.color
							* FresnelTerm (specColor, lh)
							+ surfaceReduction * gi.specular
							* FresnelLerp (specColor, grazingTerm, nv);

			//Sketch
			float2 screenUV = screenCoords.xy / screenCoords.w;
			screenUV = TRANSFORM_TEX(screenUV, _SketchTex);
			float screenRatio = _ScreenParams.y / _ScreenParams.x;
			screenUV.y *= screenRatio;
			ObjSpaceUVOffset(screenUV, screenRatio);
			float2 random = round(float2(_Time.z,-_Time.z) * _SketchSpeed) / _SketchSpeed;
			screenUV.xy += frac(random.xy);
			float2 sketchUV = screenUV;

			fixed sketch = tex2D(_SketchTex, sketchUV).a;
			sketch = lerp(sketch, 1, nl * atten);	//Regular sketch overlay
			color.rgb = lerp(color.rgb, sketch * color.rgb, _SketchStrength);
			return half4(color, 1);
		}

		//================================================================================================================================

	#endif
	ENDCG

	FallBack "VertexLit"
	CustomEditor "TCP2_MaterialInspector_SurfacePBS_SG"
}

