package com.tc.maven.controller;

import java.util.List;

import org.springframework.web.bind.annotation.*;

import com.tc.maven.bean.*;
import com.tc.maven.service.*;

public class ProfileController {
	ProfileDataService profileService = new ProfileDataService();

	// Lijst van alle profiles opvragen adhv userId(json profile list)
	@RequestMapping(value = "/profiles/{id}", method = RequestMethod.GET, headers = "Accept=application/json")
	public List<Profile> getProfiles(@PathVariable int id) {
		List<Profile> listOfProfiles = profileService.getProfilesByUserId(id);
		return listOfProfiles;
	}
	
	//Een profile opvragen aan de hand van id (json profile)
		@RequestMapping(value = "/profile/{id}", method = RequestMethod.GET, headers = "Accept=application/json")
		public Profile getProfileById(@PathVariable int id) {
			return profileService.getProfileById(id);
		}
}
