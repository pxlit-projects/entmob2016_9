package be.pxl.emotion.services;

import java.util.List;

import be.pxl.emotion.repositories.ProfileRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import be.pxl.emotion.beans.Profile;

@Service("profileService")
public class ProfileDataService {
	@Autowired
	private ProfileRepository repo;

	public Profile getProfileById(int id) {
		return repo.findOne(id);
	}

	public List<Profile> getProfilesByUserId(int id) {
		return repo.findByUserId(id);
	}

	public Profile addProfile(Profile profile) {
		return repo.save(profile);
	}

	public Profile updateProfile(Profile profile) {
		return repo.save(profile);
	}

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
}
