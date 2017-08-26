package com.pxl.emotionjava.services.api;

import com.pxl.emotionjava.entities.Profile;

import java.util.List;

public interface ProfileDataService {
    Profile getProfileById(int id);

    List<Profile> getProfilesByUserId(int id);

    String addOrUpdateProfile(Profile profile);

    String deleteProfile(int id);

    List<Profile> getAllProfiles();
}
