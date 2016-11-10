package be.pxl.emotion.service;

import java.util.List;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.EntityTransaction;
import javax.persistence.Persistence;
import javax.persistence.PersistenceContext;
import javax.persistence.PersistenceUnit;
import org.springframework.stereotype.Service;

import be.pxl.emotion.bean.Action;
import be.pxl.emotion.bean.Command;
import be.pxl.emotion.bean.Profile;

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
