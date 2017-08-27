package com.turncount.zanyzebra.controllers;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jms.core.JmsTemplate;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestHeader;

import com.turncount.zanyzebra.entities.User;
import com.turncount.zanyzebra.messaging.LogMessage;
import com.turncount.zanyzebra.services.UserDataService;

@Controller    // This means that this class is a Controller
@RequestMapping("/user") // This means URL's start with /user (after Application path)
public class UserController {
	@Autowired
	private UserDataService userService;
	
	@Autowired
	private JmsTemplate jmsTemplate;
	
	//Lijst van alle users opvragen (json user)
	@RequestMapping(value = "/all", method = RequestMethod.GET, headers = "Accept=application/json")
	@ResponseBody
	public List<User> getUsers() {		
        jmsTemplate.convertAndSend("log", new LogMessage("Request all users", this.getClass().getName()));		
		List<User> listOfUsers = userService.getAllUsers();
		jmsTemplate.convertAndSend("log", new LogMessage("Success!", this.getClass().getName()));	
        return listOfUsers;
	}
		
	//Een user opvragen aan de hand van id (json user)
	@RequestMapping(value = "/id/{id}", method = RequestMethod.GET, headers = "Accept=application/json")
	@ResponseBody
	public User getUserById(@PathVariable int id) {
        jmsTemplate.convertAndSend("log", new LogMessage(String.format("Request User %s", id), this.getClass().getName()));		
		User user = userService.getUserById(id);
		jmsTemplate.convertAndSend("log", new LogMessage("Success!", this.getClass().getName()));	
		return user;
	}
	
	@RequestMapping(value = "/name/{name}", method = RequestMethod.GET, headers = "Accept=application/json")
	@ResponseBody
	public List<User> getUserById(@PathVariable String name) {
        jmsTemplate.convertAndSend("log", new LogMessage(String.format("Request Users %s", name), this.getClass().getName()));		
		List<User> users = userService.getUserByName(name);
		jmsTemplate.convertAndSend("log", new LogMessage("Success!", this.getClass().getName()));
		return users;
	}
	
	//user toevoegen (json user)
	@RequestMapping(value = "/add", method = RequestMethod.POST, headers = "Accept=application/json")
	@ResponseBody
	 public String addUser(@RequestBody User user) {
      jmsTemplate.convertAndSend("log", new LogMessage(String.format("Request Adding User %s", user.getUserId()), this.getClass().getName()));		
	  String response = userService.addOrUpdateUser(user);
	  jmsTemplate.convertAndSend("log", new LogMessage("Success!", this.getClass().getName()));
	  return response;
	}
	
	//user updaten adhv volledig user object (json user)
	 @RequestMapping(value = "/update", method = RequestMethod.PUT, headers = "Accept=application/json")  
	 @ResponseBody
	 public String updateUser(@RequestBody User user) {
	  jmsTemplate.convertAndSend("log", new LogMessage(String.format("Request Update for User %s", user.getUserId()), this.getClass().getName()));		
	  String response = userService.addOrUpdateUser(user);
	  jmsTemplate.convertAndSend("log", new LogMessage("Success!", this.getClass().getName()));
	  return response;
	 }
	 
	 //verwijder user adhv id
	 @RequestMapping(value = "/delete/{id}", method = RequestMethod.DELETE, headers = "Accept=application/json")
	 @ResponseBody
	 public String deleteUser(@PathVariable("id") int id, @RequestHeader("X-Auth") String auth) {
	  jmsTemplate.convertAndSend("log", new LogMessage(String.format("Request Delete for User %s", id), this.getClass().getName()));		
	  String response = userService.deleteUser(id);
	  jmsTemplate.convertAndSend("log", new LogMessage("Success!", this.getClass().getName()));
	  return response;
	 }

	 //check password
     @RequestMapping(value = "/pass", method=RequestMethod.POST, headers="Accept=application/json")
     @ResponseBody
     public String CheckPass(@RequestBody User user) {
    	 jmsTemplate.convertAndSend("log", new LogMessage(String.format("Request Check Password for User %s", user.getUserId()), this.getClass().getName()));		
   	  	String response = userService.checkPass(user);
   	  	jmsTemplate.convertAndSend("log", new LogMessage("Success!", this.getClass().getName()));
   	  	return response;
     }
}
