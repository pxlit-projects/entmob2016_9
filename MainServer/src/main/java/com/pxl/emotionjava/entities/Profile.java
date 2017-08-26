package com.pxl.emotionjava.entities;

import javax.persistence.*;

@Entity
public class Profile {
	@Id
	@Column(name = "id")
	private int profileId;
    @Column(name = "user_id")
	private int userId;
	@Column(name="pairings")
    private String pairings;
    @Column(name="profile_name")
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

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof Profile)) return false;

        Profile profile = (Profile) o;

        if (getUserId() != profile.getUserId()) return false;
        if (getPairings() != null ? !getPairings().equals(profile.getPairings()) : profile.getPairings() != null)
            return false;
        return getProfileName().equals(profile.getProfileName());
    }

    @Override
    public int hashCode() {
        int result = getProfileId();
        result = 31 * result + getUserId();
        result = 31 * result + (getPairings() != null ? getPairings().hashCode() : 0);
        result = 31 * result + getProfileName().hashCode();
        return result;
    }

    public static final class ProfileBuilder {
        private int profileId;
        private int userId;
        private String pairings;
        private String profileName;

        private ProfileBuilder() {
        }

        public static ProfileBuilder aProfile() {
            return new ProfileBuilder();
        }

        public ProfileBuilder withProfileId(int profileId) {
            this.profileId = profileId;
            return this;
        }

        public ProfileBuilder withUserId(int userId) {
            this.userId = userId;
            return this;
        }

        public ProfileBuilder withPairings(String pairings) {
            this.pairings = pairings;
            return this;
        }

        public ProfileBuilder withProfileName(String profileName) {
            this.profileName = profileName;
            return this;
        }

        public Profile build() {
            Profile profile = new Profile();
            profile.setProfileId(profileId);
            profile.setUserId(userId);
            profile.setPairings(pairings);
            profile.setProfileName(profileName);
            return profile;
        }
    }
}
