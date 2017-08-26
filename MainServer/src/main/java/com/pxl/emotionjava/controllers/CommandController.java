package com.pxl.emotionjava.controllers;

import com.pxl.emotionjava.entities.Command;
import com.pxl.emotionjava.services.api.CommandDataService;
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
@RequestMapping(value="/command", produces="application/json")
public class CommandController {
	@Autowired
	CommandDataService commandService;

	//Lijst van alle profiles opvragen
	@RequestMapping(value = "/all", method = RequestMethod.GET, headers = "Accept=application/json")
	@ResponseBody
	public List<Command> getCommands() {
		List<Command> listOfCommands = commandService.getAllCommands();
		return listOfCommands;
	}
	
	// Een commandlist opvragen aan de hand van id (json profile)
		@RequestMapping(value = "/id/{id}", method = RequestMethod.GET, headers = "Accept=application/json")
		@ResponseBody
		public Command getCommandById(@PathVariable int id) {
			return commandService.getCommandById(id);
		}
		
		// command toevoegen (json command)
		@RequestMapping(value = "/add", method = RequestMethod.POST, headers = "Accept=application/json")
		@ResponseBody
		@ResponseStatus(HttpStatus.CREATED)
		public Command addProfile(@RequestBody Command command) {
			return commandService.addCommand(command);
		}
		
		// verwijder command adhv id
		@RequestMapping(value = "/delete/{id}", method = RequestMethod.DELETE, headers = "Accept=application/json")
		@ResponseBody
		public String deleteCommand(@PathVariable("id") int id) {
			return commandService.deleteCommand(id);
		}
		
		// Commandolijst in profile aanpassen adhv id (json command, action)
		@RequestMapping(value = "/update/{id}", method = RequestMethod.PUT, headers = "Accept=application/json")
		@ResponseBody
		public Command updateCommand(@RequestBody Command command) {
			return commandService.updateCommand(command);
		}
		
}
