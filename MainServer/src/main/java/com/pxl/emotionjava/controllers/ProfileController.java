package com.pxl.emotionjava.controllers;

import com.pxl.emotionjava.entities.Profile;
import com.pxl.emotionjava.services.api.ProfileDataService;
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

@RestController
@RequestMapping(value="/profile", produces="application/json")
public class ProfileController {

	@Autowired
	ProfileDataService profileService;

	//Lijst van alle profiles opvragen
	@RequestMapping(value = "/all", method = RequestMethod.GET, headers = "Accept=application/json")
	@ResponseBody
	public List<Profile> getProfiles() {
		List<Profile> listOfProfiles = profileService.getAllProfiles();
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
	@ResponseStatus(HttpStatus.CREATED)
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
