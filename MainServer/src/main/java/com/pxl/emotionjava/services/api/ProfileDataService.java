package com.pxl.emotionjava.services.api;

import com.pxl.emotionjava.entities.Profile;

import java.util.List;

public interface ProfileDataService {
    Profile getProfileById(Long id);

    List<Profile> getProfilesByUserId(Long id);

    String addOrUpdateProfile(Profile profile);

    String deleteProfile(Long id);

    List<Profile> getAllProfiles();
}
