package be.pxl.emotion.services;

import java.util.List;

import org.springframework.stereotype.Service;

import be.pxl.emotion.beans.Profile;

@Service("profileService")
public class ProfileDataService {

	public Profile getProfileById(int id) {
		return null;
	}

	public List<Profile> getProfilesByUserId(int id) {
		return null;
	}

	public Boolean addProfile(Profile profile) {
		return null;
	}

	public Profile updateProfile(Profile profile) {
		return null;
	}

	public void deleteProfile(int id) {
		return;
	}
}
