package be.pxl.emotion.controllers;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import org.springframework.web.bind.annotation.*;

import be.pxl.emotion.beans.User;
import be.pxl.emotion.services.UserDataService;

@RestController
@RequestMapping(value="/user", produces="application/json")
@Component
public class UserController {
	
	@Autowired
	UserDataService userService;

	//Lijst van alle users opvragen (json user)
	@RequestMapping(value = "/all", method = RequestMethod.GET, headers = "Accept=application/json")
	public List<User> getUsers() {
		List<User> listOfUsers = userService.getAllUsers();
        return listOfUsers;
	}
	
	//Een user opvragen aan de hand van id (json user)
	@RequestMapping(value = "/id/{id}", method = RequestMethod.GET, headers = "Accept=application/json")
	public User getUserById(@PathVariable int id) {
		return userService.getUserById(id);
	}
	
	@RequestMapping(value = "/name/{name}", method = RequestMethod.GET, headers = "Accept=application/json")
	public List<User> getUserById(@PathVariable String name) {
		return userService.getUserByName(name);
	}
	
	//user toevoegen (json user)
	@RequestMapping(value = "/add", method = RequestMethod.POST, headers = "Accept=application/json")  
	 public String addUser(@RequestBody User user) {
	  return userService.addOrUpdateUser(user);
	}
	
	//user updaten adhv volledig user object (json user)
	 @RequestMapping(value = "/update", method = RequestMethod.PUT, headers = "Accept=application/json")  
	 public String updateUser(@RequestBody User user) {
	  return userService.addOrUpdateUser(user);
	 }
	 
	 //verwijder user adhv id
	 @RequestMapping(value = "/delete/{id}", method = RequestMethod.DELETE, headers = "Accept=application/json")  
	 public String deleteUser(@PathVariable("id") int id, @RequestHeader("X-Auth") String auth) {
	  return userService.deleteUser(id); }

	//check password
    @RequestMapping(value = "/pass", method=RequestMethod.POST, headers="Accept=application/json")
    public String CheckPass(@RequestBody User user, @RequestHeader("X-Auth") String auth) {return userService.checkPass(user);}
}
