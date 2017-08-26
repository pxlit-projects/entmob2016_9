package com.pxl.emotionjava.services.api;

import com.pxl.emotionjava.entities.User;

import java.util.List;

public interface UserDataService {
    User getUserById(int id);

    List<User> getUserByName(String name);

    List<User> getAllUsers();

    String addOrUpdateUser(User user);

    String deleteUser(int id);

    String checkPass(User user);
}
