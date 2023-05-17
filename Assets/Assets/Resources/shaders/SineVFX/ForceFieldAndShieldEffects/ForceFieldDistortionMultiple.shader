Shader "SineVFX/ForceFieldAndShieldEffects/ForceFieldDistortionMultiple" {
	Properties {
		_MaskFresnelExp ("Mask Fresnel Exp", Range(0.2, 8)) = 4
		_MaskDepthFadeDistance ("Mask Depth Fade Distance", Float) = 6
		_MaskDepthFadeExp ("Mask Depth Fade Exp", Range(0.2, 10)) = 4
		[Toggle] _MaskSoftBorders ("Mask Soft Borders", Float) = 0
		_MaskSoftBordersMultiply ("Mask Soft Borders Multiply", Float) = 1.01
		_MaskSoftBordersExp ("Mask Soft Borders Exp", Float) = 20
		_NormalVertexOffset ("Normal Vertex Offset", Float) = 0
		[Toggle] _LocalNoisePosition ("Local Noise Position", Float) = 0
		_NoiseMaskPower ("Noise Mask Power", Range(0, 10)) = 1
		_Noise01 ("Noise 01", 2D) = "white" {}
		_Noise01Tiling ("Noise 01 Tiling", Float) = 1
		_Noise01ScrollSpeed ("Noise 01 Scroll Speed", Float) = 0.25
		[Toggle] _Noise02Enabled ("Noise 02 Enabled", Float) = 0
		_Noise02 ("Noise 02", 2D) = "white" {}
		_Noise02Tiling ("Noise 02 Tiling", Float) = 1
		_Noise02ScrollSpeed ("Noise 02 Scroll Speed", Float) = 0.25
		[Toggle] _NoiseDistortionEnabled ("Noise Distortion Enabled", Float) = 1
		_NoiseDistortion ("Noise Distortion", 2D) = "white" {}
		_NoiseDistortionPower ("Noise Distortion Power", Range(0, 2)) = 0.5
		_NoiseDistortionTiling ("Noise Distortion Tiling", Float) = 0.5
		_ScreenDistortionPower ("Screen Distortion Power", Range(0, 0.2)) = 0.025
		_MaskAppearLocalYRemap ("Mask Appear Local Y Remap", Float) = 0.5
		_MaskAppearLocalYAdd ("Mask Appear Local Y Add", Float) = 0
		[Toggle] _MaskAppearInvert ("Mask Appear Invert", Float) = 0
		_MaskAppearProgress ("Mask Appear Progress", Range(-2, 2)) = 0
		_MaskAppearNoise ("Mask Appear Noise", 2D) = "white" {}
		_MaskAppearRamp ("Mask Appear Ramp", 2D) = "white" {}
		[Toggle] _MaskAppearNoiseTriplanar ("Mask Appear Noise Triplanar", Float) = 0
		_MaskAppearNoiseTriplanarTiling ("Mask Appear Noise Triplanar Tiling", Float) = 0.2
		_InterceptionPower ("Interception Power", Float) = 1
		_InterceptionOffset ("Interception Offset", Float) = 1
		_ThresholdForInterception ("Threshold For Interception", Float) = 1.001
		_ThresholdForSpheres ("Threshold For Spheres", Float) = 0.99
		[Toggle] _VisualizeMaskDebug ("Visualize Mask Debug", Float) = 0
		[HideInInspector] _texcoord ("", 2D) = "white" {}
		[HideInInspector] __dirty ("", Float) = 1
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
	//CustomEditor "ASEMaterialInspector"
}