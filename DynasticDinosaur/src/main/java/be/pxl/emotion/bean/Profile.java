package be.pxl.emotion.bean;

public class Profile {
	private int profileId;
	private int userId;
	private int commands;
	private int actions;
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
	public int getCommands() {
		return commands;
	}
	public void setCommands(int commands) {
		this.commands = commands;
	}
	public int getActions() {
		return actions;
	}
	public void setActions(int actions) {
		this.actions = actions;
	}
}
