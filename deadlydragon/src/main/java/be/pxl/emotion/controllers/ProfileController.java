package be.pxl.emotion.controllers;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import org.springframework.web.bind.annotation.*;

import be.pxl.emotion.beans.Profile;
import be.pxl.emotion.services.ProfileDataService;

@RestController
@RequestMapping(value="/profile", produces="application/json")
@Component
public class ProfileController {

	@Autowired
	ProfileDataService profileService;

	// Lijst van alle profiles opvragen adhv userId(json profile list)
	@RequestMapping(value = "/userid/{id}", method = RequestMethod.GET, headers = "Accept=application/json")
	public List<Profile> getProfiles(@PathVariable int id) {
		List<Profile> listOfProfiles = profileService.getProfilesByUserId(id);
		return listOfProfiles;
	}

	// Een profile opvragen aan de hand van id (json profile)
	@RequestMapping(value = "/id/{id}", method = RequestMethod.GET, headers = "Accept=application/json")
	public Profile getProfileById(@PathVariable int id) {
		return profileService.getProfileById(id);
	}

	// profile toevoegen (json profile)
	@RequestMapping(value = "/add", method = RequestMethod.POST, headers = "Accept=application/json")
	public Profile addProfile(@RequestBody Profile profile) {
		return profileService.addProfile(profile);
	}

	// Commandolijst in profile aanpassen adhv command en action object (json command, action)
	@RequestMapping(value = "/update", method = RequestMethod.PUT, headers = "Accept=application/json")
	public Profile updateProfile(@RequestBody Profile profile) {
		return profileService.updateProfile(profile);
	}

	// verwijder profile adhv id
	@RequestMapping(value = "/delete/{id}", method = RequestMethod.DELETE, headers = "Accept=application/json")
	public String deleteProfile(@PathVariable("id") int id) {
		return profileService.deleteProfile(id);

	}
}
