Shader "SineVFX/PrototypeGrid" {
	Properties {
		_Albedo ("Albedo", 2D) = "white" {}
		_ColorTint ("Color Tint", Vector) = (1,1,1,1)
		_Tiling ("Tiling", Float) = 10
		_Metallic ("Metallic", Range(0, 1)) = 0
		_Smoothness ("Smoothness", Range(0, 1)) = 0
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
	Fallback "Diffuse"
	//CustomEditor "ASEMaterialInspector"
}