package com.pxl.emotionjava.services.impl;

import com.pxl.emotionjava.entities.Profile;
import com.pxl.emotionjava.repositories.ProfileRepository;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.springframework.test.context.junit4.SpringRunner;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import static com.pxl.emotionjava.entities.ProfileFixture.aProfile;
import static org.assertj.core.api.Assertions.assertThat;
import static org.mockito.Matchers.eq;
import static org.mockito.Mockito.when;


@RunWith(SpringRunner.class)
public class ProfileDataServiceTest {

    @Mock
    private ProfileRepository repository;

    @InjectMocks
    private ProfileDataServiceImpl profileService;

    @Test
    public void getProfileById() throws Exception {
        Profile profile = aProfile(1);
        when(repository.findOne(profile.getProfileId())).thenReturn(profile);

        Profile actualProfile = profileService.getProfileById(profile.getProfileId());

        assertThat(actualProfile).isEqualTo(profile);
    }

    @Test
    public void getProfilesByUserId() throws Exception {
        List<Profile> profiles = Collections.singletonList(aProfile(1));
        when(repository.findByUserId(1)).thenReturn(profiles);

        List<Profile> actualProfiles = profileService.getProfilesByUserId(1);

        assertThat(actualProfiles).hasSize(1);
        assertThat(actualProfiles.get(0)).isEqualTo(profiles.get(0));
    }

    @Test
    public void addOrUpdateProfile() throws Exception {
        Profile profile = aProfile(1);
        when(repository.save(eq(profile))).thenReturn(profile);
        when(repository.findByUserId(profile.getUserId())).thenReturn(Collections.singletonList(profile));

        String retVal = profileService.addOrUpdateProfile(profile);

        assertThat(retVal).isEqualTo("1");
    }

    @Test
    public void deleteProfile() throws Exception {
        Profile profile = aProfile(1);
        when(repository.exists(profile.getProfileId())).thenReturn(true);

        String retVal = profileService.deleteProfile(profile.getProfileId());

        assertThat(retVal).isEqualTo("1");

        Mockito.verify(repository).delete(profile.getProfileId());
    }

    @Test
    public void getAllProfiles() throws Exception {
        List<Profile> profiles = new ArrayList<>();
        profiles.add(aProfile(1));
        when(repository.findAll()).thenReturn(profiles);

        List <Profile> actualProfiles = profileService.getAllProfiles();

        assertThat(actualProfiles).hasSize(1);
        assertThat(actualProfiles.get(0)).isEqualTo(profiles.get(0));
    }

}