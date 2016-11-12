package be.pxl.emotion.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import be.pxl.emotion.beans.Command;
import be.pxl.emotion.services.CommandDataService;

import java.util.List;

@RestController
@RequestMapping(value="/command", produces="application/json")
@Component
public class CommandController {
	@Autowired
	CommandDataService commandService;

	//Lijst van alle profiles opvragen
	@RequestMapping(value = "/all", method = RequestMethod.GET, headers = "Accept=application/json")
	public List<Command> getProfiles() {
		List<Command> listOfCommands = commandService.getAllCommands();
		return listOfCommands;
	}
	
	// Een commandlist opvragen aan de hand van id (json profile)
		@RequestMapping(value = "/id/{id}", method = RequestMethod.GET, headers = "Accept=application/json")
		public Command getCommandById(@PathVariable int id) {
			return commandService.getCommandById(id);
		}
		
		// command toevoegen (json command)
		@RequestMapping(value = "/add", method = RequestMethod.POST, headers = "Accept=application/json")
		public Command addProfile(@RequestBody Command command) {
			return commandService.addCommand(command);
		}
		
		// verwijder command adhv id
		@RequestMapping(value = "/delete/{id}", method = RequestMethod.DELETE, headers = "Accept=application/json")
		public String deleteCommand(@PathVariable("id") int id) {
			return commandService.deleteCommand(id);
		}
		
		// Commandolijst in profile aanpassen adhv id (json command, action)
		@RequestMapping(value = "/update/{id}", method = RequestMethod.PUT, headers = "Accept=application/json")
		public Command updateCommand(@RequestBody Command command) {
			return commandService.updateCommand(command);
		}
		
}
