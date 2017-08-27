package com.turncount.zanyzebra.services;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jms.core.JmsTemplate;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.turncount.zanyzebra.entities.Profile;
import com.turncount.zanyzebra.messaging.LogMessage;
import com.turncount.zanyzebra.repositories.ProfileRepository;;

@Service("profileService")
@Component
@Transactional
public class ProfileDataService {
	@Autowired
	private ProfileRepository repo;
	@Autowired
	private JmsTemplate jmsTemplate;

	@Transactional(readOnly = true)
	public Profile getProfileById(int id) {
		try {
			return repo.findOne(id);
		}catch(Exception e) {
			jmsTemplate.convertAndSend("log", new LogMessage(e.getMessage(), this.getClass().getName(), true));	
			return null;
		}
	}

	@Transactional(readOnly = true)
	public List<Profile> getProfilesByUserId(int id) {
		try {
			return repo.findByUserId(id);
		}catch(Exception e) {
			jmsTemplate.convertAndSend("log", new LogMessage(e.getMessage(), this.getClass().getName(), true));	
			return null;
		}
	}

	@Transactional
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

	@Transactional
	public String deleteProfile(int id) {
        try {
        	jmsTemplate.convertAndSend("log", new LogMessage(String.format("Searching for Profile %s", id), this.getClass().getName()));	
            if (repo.exists(id)) {
            	jmsTemplate.convertAndSend("log", new LogMessage(String.format("Deleting Profile %s", id), this.getClass().getName()));	
                repo.delete(id);
                return "1";
            } else {
            	jmsTemplate.convertAndSend("log", new LogMessage(String.format("Could not find Profile %s", id), this.getClass().getName()));	
                return "2";
            }
        } catch (Exception e) {
            return e.getMessage();
        }
	}

	@Transactional(readOnly = true)
    public List<Profile> getAllprofiles() {
		try{
			return (List<Profile>) repo.findAll();
		}catch(Exception e) {
			jmsTemplate.convertAndSend("log", new LogMessage(e.getMessage(), this.getClass().getName(), true));	
			return null;
		}
    }
}
