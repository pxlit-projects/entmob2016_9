package be.pxl.emotion.controllers;

import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import be.pxl.emotion.beans.Command;
import be.pxl.emotion.services.CommandDataService;

@RestController
public class CommandController {
	CommandDataService commandService = new CommandDataService();
	
	// Een commandlist opvragen aan de hand van id (json profile)
		@RequestMapping(value = "/command/{id}", method = RequestMethod.GET, headers = "Accept=application/json")
		public Command getCommandById(@PathVariable int id) {
			return commandService.getCommandById(id);
		}
		
		// command toevoegen (json command)
		@RequestMapping(value = "/command", method = RequestMethod.POST, headers = "Accept=application/json")
		public Boolean addProfile(@RequestBody Command command) {
			return commandService.addCommand(command);
		}
		
		// verwijder command adhv id
		@RequestMapping(value = "/command/{id}", method = RequestMethod.DELETE, headers = "Accept=application/json")
		public void deleteCommand(@PathVariable("id") int id) {
			commandService.deleteCommand(id);
		}
		
		// Commandolijst in profile aanpassen adhv command en action object (json command, action)
		@RequestMapping(value = "/command/{id}", method = RequestMethod.PUT, headers = "Accept=application/json")
		public void updateCommand(@RequestBody Command command) {
			commandService.updateCommand(command);
		}
		
}
