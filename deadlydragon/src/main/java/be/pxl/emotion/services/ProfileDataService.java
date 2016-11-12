package be.pxl.emotion.services;

import java.util.List;

import be.pxl.emotion.beans.User;
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

    public List<Profile> getAllprofiles() {
		return (List<Profile>) repo.findAll();
    }
}
