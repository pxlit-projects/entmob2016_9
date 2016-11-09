package be.pxl.emotion.bean;

import javax.persistence.*;

@Entity
public class Profile {
	@Id
	private int profileId;
	private int userId;
	private String pairings;
	private String profileName;
	
	public int getProfileId() {
		return profileId;
	}
	public void setProfileId(int profileId) {
		this.profileId = profileId;
	}
	public int getUserId() {
		return userId;
	}
	public void setUserId(int userId) {
		this.userId = userId;
	}
	public String getProfileName() {
		return profileName;
	}
	public void setProfileName(String profileName) {
		this.profileName = profileName;
	}
	public String getPairings(){
		return pairings;
	}
	public void setPairings(String pairings){
		this.pairings = pairings;
	}
}
