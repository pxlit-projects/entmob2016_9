package com.turncount.zanyzebra.services;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jms.core.JmsTemplate;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.turncount.zanyzebra.entities.User;
import com.turncount.zanyzebra.messaging.LogMessage;
import com.turncount.zanyzebra.repositories.UserRepository;

@Service("userService")
@Component
@Transactional
public class UserDataService {	
	@Autowired
	private UserRepository repo;
	
	@Autowired
	private JmsTemplate jmsTemplate;

	@Transactional(readOnly = true)
	public User getUserById(int id) {
		try {
			return repo.findOne(id);
		}catch (Exception e) {
			jmsTemplate.convertAndSend("log", new LogMessage(e.getMessage(), this.getClass().getName(), true));	
			return null;
		}
	}

	@Transactional(readOnly = true)
	public List<User> getUserByName(String name) {
		try {
			return repo.findByUserName(name);
		}
		catch(Exception e) {
			jmsTemplate.convertAndSend("log", new LogMessage(e.getMessage(), this.getClass().getName(), true));	
			return null;
		}
	}
	
	@Transactional(readOnly = true)
	public List<User> getAllUsers() {
		try {
			return (List<User>) repo.findAll();
		}catch (Exception e) {
			jmsTemplate.convertAndSend("log", new LogMessage(e.getMessage(), this.getClass().getName(), true));	
			return null;
		}
	}

	@Transactional
	public String addOrUpdateUser(User user) {
        try {
        	jmsTemplate.convertAndSend("log", new LogMessage(String.format("Saving changes for User %s", user.getUserId()), this.getClass().getName()));	
            User u = repo.save(user);
        	jmsTemplate.convertAndSend("log", new LogMessage(String.format("Checking for User %s", user.getUserId()), this.getClass().getName()));	
            if (repo.findByUserName(u.getUserName()).get(0).getUserId() != 0){
                return "1";
            } else {
            	jmsTemplate.convertAndSend("log", new LogMessage(String.format("Something went wrong. Could not find User %s", user.getUserId()), this.getClass().getName()));	
                return "2";
            }
        } catch (Exception e) {
        	jmsTemplate.convertAndSend("log", new LogMessage(e.getMessage(), this.getClass().getName(), true));	
            return e.getMessage();
        }
	}

	@Transactional
	public String deleteUser(int id) {
        try {
        	jmsTemplate.convertAndSend("log", new LogMessage(String.format("Searching for User %s", id), this.getClass().getName()));	
            if (repo.exists(id)) {
            	jmsTemplate.convertAndSend("log", new LogMessage(String.format("Deleting User %s", id), this.getClass().getName()));	
                repo.delete(id);
                return "1";
            } else {
            	jmsTemplate.convertAndSend("log", new LogMessage(String.format("Could not find User %s", id), this.getClass().getName()));	
                return "2";
            }
        } catch (Exception e) {
        	jmsTemplate.convertAndSend("log", new LogMessage(e.getMessage(), this.getClass().getName(), true));	
            return e.getMessage();
        }
    }

	@Transactional(readOnly = true)
	public String checkPass(User user) {
        try {
        	jmsTemplate.convertAndSend("log", new LogMessage(String.format("Searching for User %s", user.getUserId()), this.getClass().getName()));	
            User u = getUserByName(user.getUserName()).get(0);
        	jmsTemplate.convertAndSend("log", new LogMessage(String.format("Checking password for User %s", user.getUserId()), this.getClass().getName()));	
            if (u.getPassword().equals(user.getPassword())) {
                return "1";
            } else {
            	jmsTemplate.convertAndSend("log", new LogMessage("Passwords did not match for User %s", this.getClass().getName()));	
                return "2";
            }
        } catch (Exception e) {
        	jmsTemplate.convertAndSend("log", new LogMessage(e.getMessage(), this.getClass().getName(), true));	
            return e.getMessage();
        }
	}
}
