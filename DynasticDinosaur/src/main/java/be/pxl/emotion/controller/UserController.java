package be.pxl.emotion.controller;

import java.util.List;

import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import be.pxl.emotion.bean.User;
import be.pxl.emotion.service.UserDataService;

@RestController
public class UserController {
	UserDataService userService = new UserDataService();

	//Lijst van alle users opvragen (json user)
	@RequestMapping(value = "/users", method = RequestMethod.GET, headers = "Accept=application/json")
	public List<User> getUsers() {
		List<User> listOfUsers = userService.getAllUsers();
		return listOfUsers;
	}
	
	//Een user opvragen aan de hand van id (json user)
	@RequestMapping(value = "/user/{id}", method = RequestMethod.GET, headers = "Accept=application/json")
	public User getUserById(@PathVariable int id) {
		return userService.getUserById(id);
	}
	
	//user toevoegen (json user)
	@RequestMapping(value = "/users", method = RequestMethod.POST, headers = "Accept=application/json")  
	 public User addUser(@RequestBody User user) {  
	  return userService.addUser(user);
	}
	
	//user updaten adhv volledig user object (json user)
	 @RequestMapping(value = "/users", method = RequestMethod.PUT, headers = "Accept=application/json")  
	 public User updateUser(@RequestBody User user) {  
	  return userService.updateUser(user);
	 }
	 
	 //verwijder user adhv id
	 @RequestMapping(value = "/user/{id}", method = RequestMethod.DELETE, headers = "Accept=application/json")  
	 public void deleteUser(@PathVariable("id") int id) {  
	  userService.deleteUser(id);  
	  
	 }
}
