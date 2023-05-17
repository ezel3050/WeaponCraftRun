Shader "SineVFX/ForceFieldAndShieldEffects/ForceFieldBasicTriplanarMultipleLWRP" {
	Properties {
		[HideInInspector] _EmissionColor ("Emission Color", Vector) = (1,1,1,1)
		[HideInInspector] _AlphaCutoff ("Alpha Cutoff ", Range(0, 1)) = 0.5
		[ASEBegin] _FinalPower ("Final Power", Range(0, 20)) = 4
		_FinalPowerAdjust ("Final Power Adjust", Range(-1, 1)) = -1
		_OpacityPower ("Opacity Power", Range(0, 4)) = 1
		[Toggle(_LOCALNOISEPOSITION_ON)] _LocalNoisePosition ("Local Noise Position", Float) = 0
		_NormalVertexOffset ("Normal Vertex Offset", Float) = 0
		[Toggle(_CUBEMAPREFLECTIONENABLED_ON)] _CubemapReflectionEnabled ("Cubemap Reflection Enabled", Float) = 0
		_CubemapReflection ("Cubemap Reflection", Cube) = "white" {}
		_Ramp ("Ramp", 2D) = "white" {}
		_RampColorTint ("Ramp Color Tint", Vector) = (1,1,1,1)
		_RampMultiplyTiling ("Ramp Multiply Tiling", Float) = 1
		[Toggle(_RAMPFLIP_ON)] _RampFlip ("Ramp Flip", Float) = 0
		_MaskFresnelExp ("Mask Fresnel Exp", Range(0.2, 8)) = 4
		[Toggle(_MASKDEPTHFADEENABLED_ON)] _MaskDepthFadeEnabled ("Mask Depth Fade Enabled", Float) = 1
		_MaskDepthFadeDistance ("Mask Depth Fade Distance", Float) = 0.25
		_MaskDepthFadeExp ("Mask Depth Fade Exp", Range(0.2, 10)) = 4
		_NoiseMaskPower ("Noise Mask Power", Range(0, 10)) = 1
		_NoiseMaskAdd ("Noise Mask Add", Range(0, 1)) = 0.25
		_Noise01 ("Noise 01", 2D) = "white" {}
		_Noise01Tiling ("Noise 01 Tiling", Float) = 1
		_Noise01ScrollSpeed ("Noise 01 Scroll Speed", Float) = 0.25
		[Toggle(_NOISE02ENABLED_ON)] _Noise02Enabled ("Noise 02 Enabled", Float) = 0
		_Noise02 ("Noise 02", 2D) = "white" {}
		_Noise02Tiling ("Noise 02 Tiling", Float) = 1
		_Noise02ScrollSpeed ("Noise 02 Scroll Speed", Float) = 0.25
		[Toggle(_NOISEDISTORTIONENABLED_ON)] _NoiseDistortionEnabled ("Noise Distortion Enabled", Float) = 1
		_NoiseDistortion ("Noise Distortion", 2D) = "white" {}
		_NoiseDistortionPower ("Noise Distortion Power", Range(0, 2)) = 0.5
		_NoiseDistortionTiling ("Noise Distortion Tiling", Float) = 0.5
		_MaskAppearLocalYRamap ("Mask Appear Local Y Ramap", Float) = 0.5
		_MaskAppearLocalYAdd ("Mask Appear Local Y Add", Float) = 0
		[Toggle(_MASKAPPEARINVERT_ON)] _MaskAppearInvert ("Mask Appear Invert", Float) = 0
		_MaskAppearProgress ("Mask Appear Progress", Range(-2, 2)) = 0
		_MaskAppearNoise ("Mask Appear Noise", 2D) = "white" {}
		_MaskAppearRamp ("Mask Appear Ramp", 2D) = "white" {}
		[Toggle(_MASKAPPEARNOISETRIPLANAR_ON)] _MaskAppearNoiseTriplanar ("Mask Appear Noise Triplanar", Float) = 0
		_MaskAppearNoiseTriplanarTiling ("Mask Appear Noise Triplanar Tiling", Float) = 0.2
		_HitWaveNoiseNegate ("Hit Wave Noise Negate", Range(0, 1)) = 1
		_HitWaveLength ("Hit Wave Length", Float) = 0.5
		_HitWaveFadeDistance ("Hit Wave Fade Distance", Float) = 6
		_HitWaveFadeDistancePower ("Hit Wave Fade Distance Power", Float) = 1
		_HitWaveRampMask ("Hit Wave Ramp Mask", 2D) = "white" {}
		_HitWaveDistortionPower ("Hit Wave Distortion Power", Float) = 0
		_InterceptionPower ("Interception Power", Range(0, 4)) = 1
		_InterceptionOffset ("Interception Offset", Float) = 0.66
		_InterceptionNoiseNegate ("Interception Noise Negate", Range(0, 1)) = 1
		_ThresholdForInterception ("Threshold For Interception", Float) = 1.001
		[ASEEnd] _ThresholdForSpheres ("Threshold For Spheres", Float) = 0.99
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "Hidden/InternalErrorShader"
	//CustomEditor "UnityEditor.ShaderGraph.PBRMasterGUI"
}