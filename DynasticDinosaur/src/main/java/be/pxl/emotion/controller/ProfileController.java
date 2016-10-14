package be.pxl.emotion.controller;

import java.util.List;

import org.springframework.web.bind.annotation.*;

import be.pxl.emotion.bean.Action;
import be.pxl.emotion.bean.Command;
import be.pxl.emotion.bean.Profile;
import be.pxl.emotion.service.ProfileDataService;

@RestController
public class ProfileController {
	ProfileDataService profileService = new ProfileDataService();

	// Lijst van alle profiles opvragen adhv userId(json profile list)
	@RequestMapping(value = "/profiles/{id}", method = RequestMethod.GET, headers = "Accept=application/json")
	public List<Profile> getProfiles(@PathVariable int id) {
		List<Profile> listOfProfiles = profileService.getProfilesByUserId(id);
		return listOfProfiles;
	}

	// Een profile opvragen aan de hand van id (json profile)
	@RequestMapping(value = "/profile/{id}", method = RequestMethod.GET, headers = "Accept=application/json")
	public Profile getProfileById(@PathVariable int id) {
		return profileService.getProfileById(id);
	}

	// profile toevoegen (json profile, command, action)
	@RequestMapping(value = "/profile", method = RequestMethod.POST, headers = "Accept=application/json")
	public Profile addProfile(@RequestBody Profile profile, @RequestBody Command command, @RequestBody Action action) {
		return profileService.addProfile(profile, command, action);
	}

	// Commandolijst in profile aanpassen adhv command en action object (json command, action)
	@RequestMapping(value = "/profiles", method = RequestMethod.PUT, headers = "Accept=application/json")
	public void updateProfile(@RequestBody Command command, Action action) {
		profileService.updateProfile(command, action);
	}

	// verwijder profile adhv id
	@RequestMapping(value = "/profile/{id}", method = RequestMethod.DELETE, headers = "Accept=application/json")
	public void deleteProfile(@PathVariable("id") int id) {
		profileService.deleteProfile(id);

	}
}
