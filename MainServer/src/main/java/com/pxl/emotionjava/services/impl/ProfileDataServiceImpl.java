package com.pxl.emotionjava.services.impl;

import com.pxl.emotionjava.entities.Profile;
import com.pxl.emotionjava.repositories.ProfileRepository;
import com.pxl.emotionjava.services.api.ProfileDataService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

;

@Service("profileService")
public class ProfileDataServiceImpl implements ProfileDataService {
	@Autowired
	private ProfileRepository repo;

	@Override
	public Profile getProfileById(int id) {
		return repo.findOne(id);
	}

	@Override
	public List<Profile> getProfilesByUserId(int id) {
		return repo.findByUserId(id);
	}

	@Override
	public String addOrUpdateProfile(Profile profile) {
		try {
			Profile p = repo.save(profile);
			if (repo.findByUserId(p.getUserId()).get(0).getProfileId() != 0){				
				return "1";
			} else {
				return "2";
			}
		} catch (Exception e) {
			return e.getMessage();
		}
	}

	@Override
	public String deleteProfile(int id) {
        try {
            if (repo.exists(id)) {
                repo.delete(id);
                return "1";
            } else {
                return "2";
            }
        } catch (Exception e) {
            return e.getMessage();
        }
	}

	@Override
    public List<Profile> getAllProfiles() {
		return (List<Profile>) repo.findAll();
    }
}
