package com.pxl.emotionjava.services.impl;

import com.pxl.emotionjava.entities.Profile;
import com.pxl.emotionjava.entities.User;
import com.pxl.emotionjava.repositories.ProfileRepository;
import com.pxl.emotionjava.repositories.UserRepository;
import com.pxl.emotionjava.services.api.ProfileDataService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

;

@Service("profileService")
public class ProfileDataServiceImpl implements ProfileDataService {
	@Autowired
	private ProfileRepository repo;

	@Autowired
	private UserRepository userRepository;

	@Override
	public Profile getProfileById(Long id) {
		return repo.findOne(id);
	}

	@Override
	public List<Profile> getProfilesByUserId(Long id) {
		return repo.findByUserId(id);
	}

	@Override
	public String addOrUpdateProfile(Profile profile) {
		if (profile.getProfileId() == 0) {
			profile.setProfileId(null);
		}
		try {
			Profile p = repo.save(profile);
			User user = userRepository.findOne(profile.getUserId());
			if (user.getDefaultProfileId() == 0) {
				user.setDefaultProfileId(profile.getProfileId());
				userRepository.save(user);
			}
			if (repo.findOne(p.getProfileId()).getProfileId() != 0){
				return "1";
			} else {
				return "2";
			}
		} catch (Exception e) {
			return e.getMessage();
		}
	}

	@Override
	public String deleteProfile(Long id) {
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
