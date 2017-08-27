package com.pxl.emotionjava.entities;

import javax.persistence.*;

@Entity
public class Profile {
	@Id
	@Column(name = "id")
    @GeneratedValue(generator = "profile_id_seq", strategy = GenerationType.SEQUENCE)
    @SequenceGenerator(name = "profile_id_seq", sequenceName = "profile_id_seq", allocationSize = 1)
	private Long profileId;
    @Column(name = "user_id")
	private Long userId;
	@Column(name="pairings")
    private String pairings;
    @Column(name="profile_name")
	private String profileName;

    public Profile(){}
    
    public Long getProfileId() {
        return profileId;
    }

    public void setProfileId(Long profileId) {
        this.profileId = profileId;
    }

    public Long getUserId() {
        return userId;
    }

    public void setUserId(Long userId) {
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
        int result = getProfileId().hashCode();
        result = 31 * result + getUserId().hashCode();
        result = 31 * result + (getPairings() != null ? getPairings().hashCode() : 0);
        result = 31 * result + getProfileName().hashCode();
        return result;
    }

    public static final class ProfileBuilder {
        private Long profileId;
        private Long userId;
        private String pairings;
        private String profileName;

        private ProfileBuilder() {
        }

        public static ProfileBuilder aProfile() {
            return new ProfileBuilder();
        }

        public ProfileBuilder withProfileId(Long profileId) {
            this.profileId = profileId;
            return this;
        }

        public ProfileBuilder withUserId(Long userId) {
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
