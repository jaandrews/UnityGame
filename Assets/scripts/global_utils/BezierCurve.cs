using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

[Serializable]
public class BezierCurve{
	public Vector3 p0;
	public Vector3 p1;
	public Vector3 p2;
	public Vector3 p3;
	
	public BezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3){
		this.p0 = p0;
		this.p1 = p1;
		this.p2 = p2;
		this.p3 = p3;
	}
	
	public Vector3 GetPoint(float time){
		float u = 1.0f - time;
		float tt = time*time;
		float uu = u*u;
		float uuu = uu * u;
		float ttt = tt * time;
		
		Vector3 result = uuu * this.p0; //first term
		result += 3 * uu * time * this.p1; //second term
		result += 3 * u * tt * this.p2; //third term
		result += ttt * this.p3; //fourth term
		return result;
	}

	public List<Vector3> GetDrawPoints(float startTime, float endTime, float dotThreshold, float minSquaredDistance){
		return this.GetDrawPointTimes(startTime, endTime, dotThreshold, minSquaredDistance).Select(t => this.GetPoint(t)).ToList();
	}
	
	public List<float> GetDrawPointTimes(float startTime, float endTime, float dotThreshold, float minSquaredDistance){
		startTime = Utilities.ClampFloat(startTime, 0,1);
		endTime = Utilities.ClampFloat(endTime, startTime,1);
		List<float> result = new List<float>();
		result.Add(startTime);
		result.Add(endTime);
		int count = 2;
		count += this.GetDrawPointTimesHelper(startTime, endTime, 1,dotThreshold, minSquaredDistance, result);
		if(count < 15){//some times shapes are tricky.
			result = new List<float>();
			for(int i = 0; i <= 15; i++){
				result.Add((i/(float)15)*(endTime -startTime) + startTime);
			}
		}
		return result;
	}
	
	private int GetDrawPointTimesHelper(float t0, float t1, int insertIndex, float dotThreshold, float minSquaredDistance, List<float> times){
		float tMid = (t0 + t1) / 2;
	  	Vector3 pnt0 = this.GetPoint(t0);
	  	Vector3 pnt1 = this.GetPoint(t1);
		
		if((pnt0-pnt1).sqrMagnitude < minSquaredDistance){
			return 0;
		}
	  	
		Vector3 pntMid = this.GetPoint(tMid);
		Vector3 leftDirection = (pnt0 - pntMid).normalized;
		Vector3 rightDirection = (pnt1 - pntMid).normalized;
		
		if(Vector3.Dot(leftDirection, rightDirection) > dotThreshold)
		{
			int pointsAdded = 0;
	 
	    	pointsAdded += this.GetDrawPointTimesHelper(t0, tMid, insertIndex,dotThreshold, minSquaredDistance,  times);
	 
	    	times.Insert(insertIndex + pointsAdded, tMid);
	    	pointsAdded++;
	 
	    	pointsAdded += this.GetDrawPointTimesHelper(tMid, t1, insertIndex + pointsAdded,dotThreshold, minSquaredDistance, times);
	    	return pointsAdded;
	  }
	  return 0;
	}		
}
