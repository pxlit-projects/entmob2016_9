package com.pxl.emotionjava.controllers;

import com.pxl.emotionjava.entities.User;
import com.pxl.emotionjava.services.api.UserDataService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController    // This means that this class is a Controller
@RequestMapping("/user") // This means URL's start with /user (after Application path)
public class UserController {
	@Autowired
	private UserDataService userService;
	
	//Lijst van alle users opvragen (json user)
	@RequestMapping(value = "/all", method = RequestMethod.GET, headers = "Accept=application/json")
	@ResponseBody
	public List<User> getUsers() {
		List<User> listOfUsers = userService.getAllUsers();
        return listOfUsers;
	}
		
	//Een user opvragen aan de hand van id (json user)
	@RequestMapping(value = "/id/{id}", method = RequestMethod.GET, headers = "Accept=application/json")
	@ResponseBody
	public User getUserById(@PathVariable int id) {
		return userService.getUserById(id);
	}
	
	@RequestMapping(value = "/name/{name}", method = RequestMethod.GET, headers = "Accept=application/json")
	@ResponseBody
	public List<User> getUserByName(@PathVariable String name) {
		return userService.getUserByName(name);
	}
	
	//user toevoegen (json user)
	@RequestMapping(value = "/add", method = RequestMethod.POST, headers = "Accept=application/json")
	@ResponseBody
	@ResponseStatus(HttpStatus.CREATED)
	 public String addUser(@RequestBody User user) {
	  return userService.addOrUpdateUser(user);
	}
	
	//user updaten adhv volledig user object (json user)
	 @RequestMapping(value = "/update", method = RequestMethod.PUT, headers = "Accept=application/json")  
	 @ResponseBody
	 public String updateUser(@RequestBody User user) {
	  return userService.addOrUpdateUser(user);
	 }
	 
	 //verwijder user adhv id
	 @RequestMapping(value = "/delete/{id}", method = RequestMethod.DELETE, headers = "Accept=application/json")
	 @ResponseBody
	 public String deleteUser(@PathVariable("id") int id) {
	  return userService.deleteUser(id); 
	 }

	 //check password
     @RequestMapping(value = "/pass", method=RequestMethod.POST, headers="Accept=application/json")
     @ResponseBody
     public String CheckPass(@RequestBody User user) {
    	 return userService.checkPass(user);
     }
}
