using System.Collections.Generic;

public class Region: CustomBoundary{
	public Boundary masterBoundary;
	
	public Region(List<Coordinate> coordinates, Boundary master_boundary): base(coordinates){
		this.masterBoundary = master_boundary;		
	}
}

public class MovableRegion: Region{
	public bool isMovable; //whether this region can be moved
	public bool isMalliable; // can change overall shape, only possible if movable
	public float dampening; //resistance to movement, only used if movable
		
	public MovableRegion(List<Coordinate> coordinates, Boundary master_boundary,
			bool isMovable, bool isMalliable, float dampening):
			base(coordinates, master_boundary)
	{
		this.isMovable = isMovable;
		this.isMalliable = isMalliable;
		this.dampening = dampening;
	}
		
	public void Move(Direction dir, int magnitude, Boundary bounds){	
		foreach (Coordinate coord in this.coordinates){
			coord.GetNeighbor(dir);
		}
	}
}

public class RegionManager{
	public List<Region> regions;
	
	public RegionManager(List<Region> regions){
			this.regions = regions;
	}
		
	public void AddRegion(string region_name){
			
	}
}