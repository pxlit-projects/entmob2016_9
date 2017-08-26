package com.pxl.emotionjava.repositories;

import com.pxl.emotionjava.entities.Profile;
import org.junit.Before;
import org.junit.Test;
import org.springframework.beans.factory.annotation.Autowired;

import java.util.List;

import static com.pxl.emotionjava.entities.ProfileFixture.aProfile;
import static org.assertj.core.api.Assertions.assertThat;

public class ProfileRepositoryTest extends RepositoryTest{

    @Autowired
    private ProfileRepository profileRepository;
    private Profile profile;

    @Before
    public void setup() {
        profile = aProfile();
        profileRepository.save(profile);

        flushAndClear();
    }

    @Test
    public void profilePersistsCorrectly() throws Exception {
        List<Profile> profiles = (List<Profile>) profileRepository.findAll();

        assertThat(profiles).hasAtLeastOneElementOfType(Profile.class);

        Profile actualProfile = profiles.get(profiles.size()-1);

        assertThat(actualProfile).isNotNull();
        assertThat(actualProfile.getProfileName()).isEqualTo(profile.getProfileName());
        assertThat(actualProfile.getPairings()).isEqualTo(profile.getPairings());
        assertThat(actualProfile.getUserId()).isEqualTo(profile.getUserId());
    }

    @Test
    public void findByUserId() {
        List<Profile> profiles = profileRepository.findByUserId(profile.getUserId());

        assertThat(profiles).hasAtLeastOneElementOfType(Profile.class);
        Profile actualProfile = profiles.get(profiles.size()-1);
        assertThat(actualProfile.getProfileName()).isEqualTo(profile.getProfileName());
        assertThat(actualProfile.getPairings()).isEqualTo(profile.getPairings());
        assertThat(actualProfile.getUserId()).isEqualTo(profile.getUserId());
    }
}