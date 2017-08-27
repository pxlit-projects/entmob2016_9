package com.turncount.zanyzebra.controllers;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jms.core.JmsTemplate;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import com.turncount.zanyzebra.entities.Profile;
import com.turncount.zanyzebra.messaging.LogMessage;
import com.turncount.zanyzebra.services.ProfileDataService;

@Controller
@RequestMapping(value="/profile", produces="application/json")
public class ProfileController {

	@Autowired
	ProfileDataService profileService;
	@Autowired
	private JmsTemplate jmsTemplate;

	//Lijst van alle profiles opvragen
	@RequestMapping(value = "/all", method = RequestMethod.GET, headers = "Accept=application/json")
	@ResponseBody
	public List<Profile> getProfiles() {
        jmsTemplate.convertAndSend("log", new LogMessage("Request all profiles", this.getClass().getName()));		
		List<Profile> listOfProfiles = profileService.getAllprofiles();
		jmsTemplate.convertAndSend("log", new LogMessage("Success!", this.getClass().getName()));	
		return listOfProfiles;
	}

	// Lijst van alle profiles opvragen adhv userId(json profile list)
	@RequestMapping(value = "/userid/{id}", method = RequestMethod.GET, headers = "Accept=application/json")
	@ResponseBody
	public List<Profile> getProfiles(@PathVariable int id) {
        jmsTemplate.convertAndSend("log", new LogMessage(String.format("Request Profiles for User %s", id), this.getClass().getName()));		
		List<Profile> listOfProfiles = profileService.getProfilesByUserId(id);
		jmsTemplate.convertAndSend("log", new LogMessage("Success!", this.getClass().getName()));	
		return listOfProfiles;
	}

	// Een profile opvragen aan de hand van id (json profile)
	@RequestMapping(value = "/id/{id}", method = RequestMethod.GET, headers = "Accept=application/json")
	@ResponseBody
	public Profile getProfileById(@PathVariable int id) {
        jmsTemplate.convertAndSend("log", new LogMessage(String.format("Request Profile %s", id), this.getClass().getName()));		
		Profile profile = profileService.getProfileById(id);
		jmsTemplate.convertAndSend("log", new LogMessage("Success!", this.getClass().getName()));	
		return profile;
	}

	// profile toevoegen (json profile)
	@RequestMapping(value = "/add", method = RequestMethod.POST, headers = "Accept=application/json")
	@ResponseBody
	public String addProfile(@RequestBody Profile profile) {
	    jmsTemplate.convertAndSend("log", new LogMessage(String.format("Request Adding Profile %s", profile.getProfileId()), this.getClass().getName()));		
		String response = profileService.addOrUpdateProfile(profile);
		  jmsTemplate.convertAndSend("log", new LogMessage("Success!", this.getClass().getName()));
		  return response;
	}

	// Commandolijst in profile aanpassen adhv command en action object (json command, action)
	@RequestMapping(value = "/update", method = RequestMethod.PUT, headers = "Accept=application/json")
	@ResponseBody
	public String updateProfile(@RequestBody Profile profile) {
		  jmsTemplate.convertAndSend("log", new LogMessage(String.format("Request Update for Profile %s", profile.getProfileId()), this.getClass().getName()));		
		  String response = profileService.addOrUpdateProfile(profile);
		  jmsTemplate.convertAndSend("log", new LogMessage("Success!", this.getClass().getName()));
		  return response;
	}

	// verwijder profile adhv id
	@RequestMapping(value = "/delete/{id}", method = RequestMethod.DELETE, headers = "Accept=application/json")
	@ResponseBody
	public String deleteProfile(@PathVariable("id") int id) {
		  jmsTemplate.convertAndSend("log", new LogMessage(String.format("Request Delete for Profile %s", id), this.getClass().getName()));		
		  String response = profileService.deleteProfile(id);
		  jmsTemplate.convertAndSend("log", new LogMessage("Success!", this.getClass().getName()));
		  return response;
	}
}
