Shader "Minimalist/Gradient Skybox" {
	Properties {
		_Color1 ("Color 1", Vector) = (1,1,1,0)
		_Color2 ("Color 2", Vector) = (1,1,1,0)
		_Intensity ("Intensity", Float) = 1
		_Exponent ("Exponent", Float) = 1
		_DirX ("Direction X", Range(0, 180)) = 0
		_DirY ("Direction Y", Range(0, 180)) = 0
		[HideInInspector] _UpVector ("Direction", Vector) = (0,1,0,0)
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
	//CustomEditor "Minimalist.MinimalistSkyEditor"
}