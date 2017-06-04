// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Waves" {
	Properties{

         _Speed("Speed",Range(0,100))=2
          _SpeedV("SpeedV",Range(0,1))=1
	     _MainTex("MainTex",2D)="white"{}
	     _merage("merage",Range(0,1))=0.5
	}

	Subshader{

	    Pass{

	    CGPROGRAM

	      
	       #pragma vertex vert
	       #pragma fragment frag

	       float4 _MainTex_ST;
	       sampler2D  _MainTex;
	       float _Speed;
	       float _SpeedV;
		   float _merage;
	       
	       struct a2v{

               float4 vertex:POSITION;
               float4 texcoord:TEXCOORD;

	       };

	       struct v2f{

	           float4 pos:SV_POSITION;
	           float2 texcoord:TEXCOORD0;

	       };

	        v2f vert(a2v v)
	        {
	            v2f o;
	            v.vertex.y+=sin(_Time.y+(v.vertex.x+v.vertex.z))*_Speed;
	            o.pos=mul(UNITY_MATRIX_MVP,v.vertex);
	            o.texcoord.xy=_MainTex_ST.xy*v.texcoord.xy+_MainTex_ST;
	          return o;
	        }

	        fixed4 frag(v2f i):SV_Target{
	             i.texcoord.y+=sin(_Time.y*_SpeedV*2);
			     fixed4 col = tex2D(_MainTex, i.texcoord.xy);
				 i.texcoord.y += sin(_Time.y*_SpeedV);
				 fixed4 col2 = tex2D(_MainTex, i.texcoord.xy);

				 return col*_merage+col2*(1-_merage);
	        }


	    ENDCG

	    }
	}
}
