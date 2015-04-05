using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BezierCurveRenderer : MonoBehaviour {
	public BezierCurve curve;
	public bool draw = true;
	public int numCurves = 1;
	public float dotThreshold = -.99f;
	public float minSquaredLength = .5f;
	public float startTime = 0.0f;
	public float endTime = 1.0f;
	public float startWidth = .1f;
	public float endWidth = .1f;
	public bool maintainUV = false;
	public Vector2 textureOffset;
	public Vector2 textureScale;
	public Vector2 textureScrollSpeed;
	public bool debugWireframe;
	public Material material;

	public void Awake(){
		this.curve = new BezierCurve(this.curve.p0, this.curve.p1, this.curve.p2, this.curve.p3);
	}
	
	public void Start(){
		
	}
	
	public void OnPostRender(){
		LineRenderer lr = this.GetComponent<LineRenderer>();
		int lri = 0;
		List<Vector3> lrPoints =this.curve.GetDrawPoints(this.startTime, this.endTime, this.dotThreshold, this.minSquaredLength);
		lr.SetVertexCount(lrPoints.Count);
		foreach(Vector3 v in lrPoints){
			lr.SetPosition(lri, v);
			lri++;
		}
		
		if(this.draw && this.numCurves > 0){
			this.minSquaredLength = this.minSquaredLength < 0? .01f: this.minSquaredLength;
			this.startTime = Utilities.ClampFloat(this.startTime, 0f,1.0f);
			this.endTime = Utilities.ClampFloat(this.endTime, this.startTime,1.0f); 
			List<Vector3> line_points = new List<Vector3>();
			//start draw curve
			GL.PushMatrix();
			GL.Begin(GL.TRIANGLE_STRIP);
			GL.wireframe=this.debugWireframe;
			this.material.SetPass(0);
			List<float> times = this.curve.GetDrawPointTimes(this.startTime, this.endTime, this.dotThreshold, this.minSquaredLength);
			Vector3 topPoint = Vector3.zero;
			Vector3 bottomPoint = Vector3.zero;
			float maxWidth = this.startWidth > this.endWidth?this.startWidth:this.endWidth;
			for(int i = 0; i < times.Count-1; i++){
				float st = times[i];
				float et = times[i+1];
				Vector3 startPoint = this.curve.GetPoint(st);
				//line_points.Add(startPoint);
				Vector3 endPoint = this.curve.GetPoint(et);
				Vector3 normal = Vector3.Cross(endPoint - startPoint, Vector3.up);
				if(normal == Vector3.zero){
					normal = Vector3.forward;
				}
				Vector3 side = -Vector3.Cross(normal, endPoint-startPoint).normalized;
				float sw = (startWidth + ((endWidth - startWidth)*st))/2;
				float ew = (startWidth + ((endWidth - startWidth)*et))/2;
				Vector3 startTop = startPoint + (side*sw);
				Vector3 startBottom = startPoint - (side*sw);
				//float vChange = sw/maxWidth;
				if(i > 0){
					startTop = (startTop + topPoint)/2f;
					startBottom = (startBottom + bottomPoint)/2f;
				}
				GL.TexCoord2(st, 1f);
				GL.Vertex3(startTop.x, startTop.y, startTop.z);
				line_points.Add(startTop);
				GL.TexCoord2(st, 0f);
				//Debug.Log("" + i + "<>" + st);
				GL.Vertex3(startBottom.x, startBottom.y, startBottom.z);
				
				topPoint = endPoint + (side*ew);
				bottomPoint = endPoint - (side*ew);
			}
			//finish last two points
			GL.TexCoord2(1.0f, 1.0f);
			//Debug.Log("" + (times.Count-1) + "<>" + 1.0f);
			GL.Vertex3(topPoint.x, topPoint.y, topPoint.z);
			GL.TexCoord2(1.0f, 0.0f);
			GL.Vertex3(bottomPoint.x, bottomPoint.y, bottomPoint.z);
			
			line_points.Add(topPoint);
			GL.End();
			
			
			GL.Begin(GL.LINES);
			Debug.Log(line_points.Count);
			GL.Color(new Color(0,0,0,1));
			foreach(Vector3 lp in line_points){
				GL.Vertex3(lp.x,lp.y,.1f);
			}
			GL.End();
			GL.PopMatrix();
			
			//change render stats
			Vector2 totalOffset = this.textureOffset;
			Vector2 totalScale = this.textureScale;
			totalOffset += this.textureScrollSpeed * Time.time;
			if(maintainUV){
				totalOffset.x += this.startTime;
				totalScale.x *= (this.endTime - this.startTime);
			}
			this.material.SetTextureOffset("_MainTex", totalOffset);
			this.material.SetTextureScale("_MainTex", totalScale);
		}
	}
}
