package com.pxl.emotionjava.services.api;

import com.pxl.emotionjava.entities.User;

import java.util.List;

public interface UserDataService {
    User getUserById(Long id);

    List<User> getUserByName(String name);

    List<User> getAllUsers();

    String addOrUpdateUser(User user);

    String deleteUser(Long id);

    String checkPass(User user);
}
