package be.pxl.emotion.service;

import java.util.List;

import org.apache.logging.log4j.Logger;
import org.apache.logging.log4j.LogManager;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Service;

import be.pxl.emotion.bean.User;
import be.pxl.emotion.repositories.UserRepository;

@Service("userService")
@Component
public class UserDataService {
	@Autowired
	private UserRepository repo;
	private static final Logger logger = LogManager.getLogger("UserService");

	public User getUserById(int id) {
		return repo.findById(id);
	}

	public User getUserByName(String name) {
		return null;
	}

	public List<User> getAllUsers() {
		
		logger.error("current repo state: " + repo.toString());
		return (List<User>) repo.findAll();
	}

	public User addUser(User user) {
		return null;
	}

	public User updateUser(User user) {
		return null;
	}

	public void deleteUser(int id) {
		return;
	}
}
