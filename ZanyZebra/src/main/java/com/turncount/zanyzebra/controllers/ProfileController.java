package com.turncount.zanyzebra.controllers;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import com.turncount.zanyzebra.entities.Profile;
import com.turncount.zanyzebra.services.ProfileDataService;

@Controller
@RequestMapping(value="/profile", produces="application/json")
public class ProfileController {

	@Autowired
	ProfileDataService profileService;

	//Lijst van alle profiles opvragen
	@RequestMapping(value = "/all", method = RequestMethod.GET, headers = "Accept=application/json")
	@ResponseBody
	public List<Profile> getProfiles() {
		List<Profile> listOfProfiles = profileService.getAllprofiles();
		return listOfProfiles;
	}

	// Lijst van alle profiles opvragen adhv userId(json profile list)
	@RequestMapping(value = "/userid/{id}", method = RequestMethod.GET, headers = "Accept=application/json")
	@ResponseBody
	public List<Profile> getProfiles(@PathVariable int id) {
		List<Profile> listOfProfiles = profileService.getProfilesByUserId(id);
		return listOfProfiles;
	}

	// Een profile opvragen aan de hand van id (json profile)
	@RequestMapping(value = "/id/{id}", method = RequestMethod.GET, headers = "Accept=application/json")
	@ResponseBody
	public Profile getProfileById(@PathVariable int id) {
		return profileService.getProfileById(id);
	}

	// profile toevoegen (json profile)
	@RequestMapping(value = "/add", method = RequestMethod.POST, headers = "Accept=application/json")
	@ResponseBody
	public String addProfile(@RequestBody Profile profile) {
		return profileService.addOrUpdateProfile(profile);
	}

	// Commandolijst in profile aanpassen adhv command en action object (json command, action)
	@RequestMapping(value = "/update", method = RequestMethod.PUT, headers = "Accept=application/json")
	@ResponseBody
	public String updateProfile(@RequestBody Profile profile) {
		return profileService.addOrUpdateProfile(profile);
	}

	// verwijder profile adhv id
	@RequestMapping(value = "/delete/{id}", method = RequestMethod.DELETE, headers = "Accept=application/json")
	@ResponseBody
	public String deleteProfile(@PathVariable("id") int id) {
		return profileService.deleteProfile(id);

	}
}
