package be.pxl.emotion.services;

import java.util.List;

import org.apache.logging.log4j.Logger;
import org.apache.logging.log4j.LogManager;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Service;

import be.pxl.emotion.beans.User;
import be.pxl.emotion.repositories.UserRepository;

@Service("userService")
@Component
public class UserDataService {
	private static final Logger logger = LogManager.getLogger("UserService");
	@Autowired
	private UserRepository repo;

	public User getUserById(int id) {
		return repo.findOne(id);
	}

	public List<User> getUserByName(String name) {
		return repo.findByUserName(name);
	}

	public List<User> getAllUsers() {
		return (List<User>) repo.findAll();
	}

	public String addOrUpdateUser(User user) {
        try {
        	System.out.println(user.getCountry());
            User u = repo.save(user);
            System.out.println(u.getCountry());
            if (repo.findByUserName(u.getUserName()).get(0).getUserId() != 0){
                return "1";
            } else {
                return "2";
            }
        } catch (Exception e) {
            return e.getMessage();
        }
	}

	public String deleteUser(int id) {
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

	public String checkPass(User user) {
        try {
            User u = getUserByName(user.getUserName()).get(0);
            if (u.getPassword().equals(user.getPassword())) {
                return "1";
            } else {
                return "2";
            }
        } catch (Exception e) {
            return e.getMessage();
        }
	}
}
