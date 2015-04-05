
Shader "Custom/clip_shader" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_CutHeight("CutHeight", Float) = .65
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		CGPROGRAM
// Upgrade NOTE: excluded shader from OpenGL ES 2.0 because it does not contain a surface program or both vertex and fragment programs.
#pragma exclude_renderers gles
		//#pragma vertex vert
		#pragma surface surf Lambert vertex:vert
		//#pragma surface surf Lambert
		
		sampler2D _MainTex;
		float _CutHeight;

		struct Input {
			float2 uv_MainTex;
			float localYPos;
		};
		
		void vert (inout appdata_full v, out Input o) {
          o.localYPos = v.vertex.y;
      	}

		void surf (Input IN, inout SurfaceOutput o) {
			float cut = 1.0;//cut = false
			if(IN.localYPos > _CutHeight){cut= -1.0;}//cut = true
			clip(cut);
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	//FallBack "Diffuse"

}