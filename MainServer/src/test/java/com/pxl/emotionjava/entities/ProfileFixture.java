package com.pxl.emotionjava.entities;

public class ProfileFixture {
    public static Profile aProfile() {
        return Profile.ProfileBuilder.aProfile()
                .withProfileName("my profile")
                .withPairings("pairings")
                .build();
    }

    public static Profile aProfile(Long id) {
        Profile profile = aProfile();
        profile.setProfileId(id);
        return profile;
    }
}
