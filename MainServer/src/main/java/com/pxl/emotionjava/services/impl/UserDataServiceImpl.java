package com.pxl.emotionjava.services.impl;

import com.pxl.emotionjava.entities.User;
import com.pxl.emotionjava.repositories.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service("userService")
public class UserDataServiceImpl implements com.pxl.emotionjava.services.api.UserDataService {
	
	@Autowired
	private UserRepository repo;

	@Override
	public User getUserById(Long id) {
		return repo.findOne(id);
	}

    @Override
	public List<User> getUserByName(String name) {
		return repo.findByUserName(name);
	}

    @Override
	public List<User> getAllUsers() {
		return (List<User>) repo.findAll();
	}

    @Override
	public String addOrUpdateUser(User user) {
        try {
            if (user.getUserId() == 0) {
                user.setUserId(null);
            }

            User u = repo.save(user);
            if (repo.findByUserName(u.getUserName()).get(0).getUserId() != 0){
                return "1";
            } else {
                return "2";
            }
        } catch (Exception e) {
            return e.getClass().getCanonicalName();
        }
	}

    @Override
	public String deleteUser(Long id) {
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
