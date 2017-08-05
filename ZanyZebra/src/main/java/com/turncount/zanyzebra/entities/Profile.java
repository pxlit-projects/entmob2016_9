package com.turncount.zanyzebra.entities;

import javax.persistence.*;

@Entity
public class Profile {
	@Id
	@Column(name = "Id")
	private int profileId;
    @Column(name = "userId")
	private int userId;
	@Column(name="pairings")
    private String pairings;
    @Column(name="profileName")
	private String profileName;

    public Profile(){}
    
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

    public String getPairings() {
        return pairings;
    }

    public void setPairings(String pairings) {
        this.pairings = pairings;
    }

    public String getProfileName() {
        return profileName;
    }

    public void setProfileName(String profileName) {
        this.profileName = profileName;
    }
}
